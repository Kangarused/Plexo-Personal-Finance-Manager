module PersonalFinance.Controllers {
    export class GroupsController {
        static $inject = ['$scope', 'messageService',
            '$validation', 'groupDataService',
            '$uibModal', 'budgetDataService', 'billDataService'];

        groups: Models.IGroup[];
        newGroup: Models.ICreateGroupRequest;

        constructor(
            private $scope,
            private messageService: Modules.MessageDisplay.IMessageService,
            private $validation,
            private groupDataService: Services.IGroupDataService,
            private $uibModal,
            private budgetDataService: Services.IBudgetDataService,
            private billDataService: Services.IBillDataService
        ) {
            $scope.vm = this;
            this.loadGroups();
        }

        loadGroups() {
            this.groupDataService.getUsersGroups().then(
            (response) => {
                this.groups = response.data;
            });
        }

        createGroup(form) {
            this.$validation.validate(form).success(() => {
                this.groupDataService.createGroup(this.newGroup).then(
                (response) => {
                    this.messageService.addInfo(response.data.response);
                    this.messageService.removeInfoAfterSeconds(response.data.response, 1);
                    this.newGroup = <Models.ICreateGroupRequest>{};
                    this.loadGroups();
                }, (err) => {
                    this.messageService.addError("An error occured while creating this group, please try again");
                    this.messageService.removeErrorAfterSeconds("An error occured while creating this group, please try again", 2);
                });
            },(err) => {
                this.messageService.addError("Group name is required");
                this.messageService.removeErrorAfterSeconds("Group name is required", 1);
            });
        }
    }
}