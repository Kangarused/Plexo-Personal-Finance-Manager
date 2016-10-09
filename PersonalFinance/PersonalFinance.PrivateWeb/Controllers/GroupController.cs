using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PersonalFinance.Common.Dtos;
using PersonalFinance.Common.Model;
using PersonalFinance.PrivateWeb.Database.Repositories;
using PersonalFinance.PrivateWeb.Providers;

namespace PersonalFinance.PrivateWeb.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IUserResolver _userResolver;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IGroupInviteRepository _groupInviteRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBillRepository _billRepository;

        public GroupController(
            IUserResolver userResolver,
            IUserRepository userRepository,
            IGroupRepository groupRepository,
            IGroupMemberRepository groupMemberRepository,
            IGroupInviteRepository groupInviteRepository,
            IBudgetRepository budgetRepository,
            IBillRepository billRepository
            )
        {
            _userResolver = userResolver;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
            _groupInviteRepository = groupInviteRepository;
            _budgetRepository = budgetRepository;
            _billRepository = billRepository;
        }

        [AcceptVerbs("GET")]
        public async Task<Group> LoadGroup(int param)
        {
            var currentUser = _userResolver.GetUser();
            var groupMember = await _groupMemberRepository.GetByUserIdForGroup(currentUser.Id, param);

            if (groupMember != null)
            {
                var group = await _groupRepository.GetByIdAsync(param);
                return group;
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [AcceptVerbs("GET")]
        public async Task<List<Group>> GetUsersGroups()
        {
            var currentUser = _userResolver.GetUser();
            var groupList = await _groupMemberRepository.GetGroupListForUser(currentUser.Id);

            List<Group> groups = new List<Group>();
            foreach (var groupId in groupList)
            {
                Group group = await _groupRepository.GetByIdAsync(groupId.GroupId);
                groups.Add(group);
            }

            return groups;
        }

        [AcceptVerbs("GET")]
        public async Task<List<Budget>> GetGroupBudgets(int param)
        {
            var currentUser = _userResolver.GetUser();
            var groupMembers = await _groupMemberRepository.GetAllMembersForGroup(param);

            if (groupMembers.Any(x => x.UserId == currentUser.Id))
            {
                var budgetList = await _budgetRepository.GetBudgetsForGroup(param);
                return budgetList;
            }
            
            throw new UnauthorizedAccessException("User does not have access to this group");
        }

        [AcceptVerbs("GET")]
        public async Task<List<Bill>> GetGroupBills(int param)
        {
            var currentUser = _userResolver.GetUser();
            var groupMembers = await _groupMemberRepository.GetAllMembersForGroup(param);

            if (groupMembers.Any(x => x.UserId == currentUser.Id))
            {
                var billList = await _billRepository.GetBillsForGroup(param);
                return billList;
            }

            throw new UnauthorizedAccessException("User does not have access to this group");
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> CreateGroup(CreateGroupRequest request)
        {
            var currentUser = _userResolver.GetUser();
            if (request.Name != null)
            {
                string name = request.Name;
                if (request.Name.Length > 255)
                {
                    name = request.Name.Substring(0, 255);
                }

                Group newGroup = new Group(){Name = name};
                var groupId = await _groupRepository.InsertAsync(newGroup);

                GroupMember newMember = new GroupMember()
                {
                    UserId = currentUser.Id,
                    GroupId = Convert.ToInt32(groupId),
                    Role = Role.GroupAdmin.ToString()
                };

                await _groupMemberRepository.InsertAsync(newMember);

                return new ActionResponseGeneric<string>()
                {
                    Succeed = true,
                    Response = "Your new group has been created"
                };
            }

            throw new ArgumentNullException();
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResponseGeneric<string>> LeaveGroup([FromBody] int groupId)
        {
            var currentUser = _userResolver.GetUser();
            var group = await _groupRepository.GetByIdAsync(groupId);

            if (group != null)
            {
                bool deleteGroup = false;
                var groupMembers = await _groupMemberRepository.GetAllMembersForGroup(groupId);
                if (groupMembers.Any(x => x.UserId == currentUser.Id))
                {
                    if (groupMembers.Count <= 1)
                    {
                        deleteGroup = true;
                        await _budgetRepository.DeleteAsync(x => x.GroupId == groupId);
                        await _billRepository.DeleteAsync(x => x.GroupId == groupId);
                        await _groupMemberRepository.DeleteAsync(x => x.GroupId == groupId);
                        await _groupRepository.DeleteAsync(x => x.Id == groupId);
                    }
                    else
                    {
                        var member = await _groupMemberRepository.GetByUserIdForGroup(currentUser.Id, groupId);
                        if (member != null)
                        {
                            if (member.Role == Role.GroupAdmin.ToString())
                            {
                                groupMembers.OrderBy(x => x.Id).First().Role = Role.GroupAdmin.ToString();
                            }
                        }

                        await
                            _groupMemberRepository.DeleteAsync(x => x.GroupId == groupId && x.UserId == currentUser.Id);
                    }
                }
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [AcceptVerbs("GET")]
        public async Task<List<PendingInvites>> LoadPendingInvites(int param)
        {
            var currentUser = _userResolver.GetUser();
            var groupMembers = await _groupMemberRepository.GetAllMembersForGroup(param);

            if (groupMembers.Any(x => x.UserId == currentUser.Id))
            {
                var invites = await _groupInviteRepository.GetRecentInvitesForGroup(param);
                List<PendingInvites> recentInvites = new List<PendingInvites>();

                foreach (var invite in invites)
                {
                    var user = await _userRepository.GetByIdAsync(invite.ToUserId);
                    recentInvites.Add(new PendingInvites
                    {
                        ToEmail = user.Email,
                        DateSent = invite.DateSent
                    });
                }

                return recentInvites;
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}