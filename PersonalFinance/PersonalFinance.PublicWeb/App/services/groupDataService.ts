module PersonalFinance.Services {
    export interface IGroupDataService {
        loadGroup(groupId: number): ng.IPromise<Models.IResponseObject<Models.IGroup>>;
        getUsersGroups(): ng.IPromise<Models.IResponseObject<Models.IGroup[]>>;
        getGroupBudgets(groupId: number): ng.IPromise<Models.IResponseObject<Models.IBudget[]>>;
        getGroupBills(groupId: number): ng.IPromise<Models.IResponseObject<Models.IBill[]>>;
        createGroup(reqeust: Models.ICreateGroupRequest): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        leaveGroup(groupId: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        loadPendingInvites(groupId: number): ng.IPromise<Models.IResponseObject<Models.IPendingInvites[]>>;
}

    export class GroupDataService implements IGroupDataService {
        static $inject = ['$http'];

        constructor(public $http: ng.IHttpService) { 

        }
        loadGroup(groupId: number): ng.IPromise<Models.IResponseObject<Models.IGroup>> {
            return this.$http.get("/api/Group/LoadGroup/" + groupId);
        }
        getUsersGroups(): ng.IPromise<Models.IResponseObject<Models.IGroup[]>> {
            return this.$http.get("/api/Group/GetUsersGroups/");
        }
        getGroupBudgets(groupId: number): ng.IPromise<Models.IResponseObject<Models.IBudget[]>> {
            return this.$http.get("/api/Group/GetGroupBudgets/" + groupId);
        }
        getGroupBills(groupId: number): ng.IPromise<Models.IResponseObject<Models.IBill[]>> {
            return this.$http.get("/api/Group/GetGroupBills/" + groupId);
        }
        createGroup(reqeust: Models.ICreateGroupRequest): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Group/CreateGroup/", reqeust);
        }
        leaveGroup(groupId: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Group/LeaveGroup/", groupId);
        }
        loadPendingInvites(groupId: number): ng.IPromise<Models.IResponseObject<Models.IPendingInvites[]>> {
            return this.$http.get("/api/Group/LoadPendingInvites/" + groupId);
        }
    }
}