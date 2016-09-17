module PersonalFinance.Controllers {
    export class BudgetListController {
        static $inject = ['$scope', 'messageService', '$validation', 'budgetDataService', '$uibModal'];

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
            private budgetDataService: Services.IBudgetDataService,
            private $uibModal
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

        deleteBudget(budget) {
            var modalInstance = this.$uibModal.open({
                animation: true,
                templateUrl: '/App/views/modals/confirm-modal.html',
                controller: 'confirmModalController',
                resolve: {
                    modalData: () => {
                        return {
                            modalHead: 'Delete Budget',
                            modalBody: 'Are you sure you want to delete this budget?'
                        };
                    }
                }
            });

            modalInstance.result.then(confirm => {
                if (confirm) {
                    this.budgetDataService.deleteBudget(budget).then(
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
            });
        }

        getRemainingForBudget(budget: Models.IBudget) {
            if (budget.type == 'Savings') {
                return budget.allocatedAmount - budget.balance;
            } else {
                return budget.balance;
            }
        }
    }
}