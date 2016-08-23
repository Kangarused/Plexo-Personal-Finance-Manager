module PersonalFinance.Controllers {
    export class BudgetListController {
        static $inject = ['$scope', 'messageService', '$validation', 'budgetDataService'];

        recentBudget: Models.IRecentBudgetResponse;
        budgets: Models.IBudget[];
        newBudget: Models.IBudget;
        delBudget: Models.IBudget;

        budgetTypes = Models.BudgetType;
        modalError: string = null;

        constructor(
            private $scope: IScope,
            private messageService: Modules.MessageDisplay.IMessageService,
            private $validation,
            private budgetDataService: Services.IBudgetDataService
        ) {
            $scope.vm = this;

            this.loadBudgets();
        }

        loadRecentBudget() {
            this.budgetDataService.getRecentBudget().then(
                (response) => {
                    this.recentBudget = response.data;
                });
        }

        loadBudgets() {
            this.budgetDataService.getBudgets().then(
                (response) => {
                    this.budgets = response.data;
                    this.loadRecentBudget();
                });
        }

        addBudget(form) {
            this.messageService.clear();
            this.modalError = null;
            this.$validation.validate(form)
            .success(() => {
                this.budgetDataService.addBudget(this.newBudget).then(
                    (response) => {
                        this.messageService.addInfo(response.data.response);
                        this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                        this.loadBudgets();
                    }, (err) => {
                    var message = "Failed to create budget";
                    this.messageService.addError(message);
                    this.messageService.removeErrorAfterSeconds(message, 4);
                    });
            })
            .error(() => {
                this.modalError = "Validation issues found, please review your submission";
            });
        }

        setWaitingDelete(budget) {
            this.delBudget = budget;
        }

        deleteBudget() {
            this.budgetDataService.deleteBudget(this.delBudget).then(
            (response) => {
                this.messageService.addInfo(response.data.response);
                this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                this.loadBudgets();
            }, (err) => {
                var message = "Failed to delete budget";
                this.messageService.addError(message);
                this.messageService.removeErrorAfterSeconds(message, 4);
            });
        }
    }
}