module PersonalFinance.Controllers {
    export class GroupDetailController {
        static $inject = ['$scope', 'messageService',
            '$validation', 'groupDataService',
            '$uibModal', 'budgetDataService', 'billDataService', '$stateParams'];

        group: Models.IGroup;
        newInvite: Models.IGroupInviteRequest;
        pendingRequests: Models.IPendingInvites[] = [
            {
                toEmail: 'robert.jackson@hotmail.com.au',
                dateSent: 'Fri 7 Oct 2016'
            },
            {
                toEmail: 'billy.joe@hotmail.com',
                dateSent: 'Thurs 6 Oct 2016'
            },
            {
                toEmail: 'eliza.queenn@gmail.com',
                dateSent: 'Thurs 6 Oct 2016'
            },
            {
                toEmail: 'sam.bouvard@hotmail.com',
                dateSent: 'Thurs 6 Oct 2016'
            },
            {
                toEmail: 'kyle.sanders@hotmail.com',
                dateSent: 'Wed 5 Oct 2016'
            }];

        constructor(
            private $scope,
            private messageService: Modules.MessageDisplay.IMessageService,
            private $validation,
            private groupDataService: Services.IGroupDataService,
            private $uibModal,
            private budgetDataService: Services.IBudgetDataService,
            private billDataService: Services.IBillDataService,
            private $stateParams
        ) {
            $scope.vm = this;
            this.loadGroup($stateParams.groupId);
            this.loadPendingInvites();
            window['nigger'] = this;
        }

        loadGroup(groupId: number) {
            this.groupDataService.loadGroup(groupId).then(
            (response) => {
                this.group = response.data;
            });
        }

        loadPendingInvites() {
            this.groupDataService.loadPendingInvites(this.$stateParams.groupId).then(
            (response) => {
                //this.pendingRequests = response.data;
            });
        }
    }
}