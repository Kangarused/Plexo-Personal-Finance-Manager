module PersonalFinance.Controllers {

    export class BudgetDetailsController {
        static $inject = ['$scope', '$stateParams', 'messageService', '$validation', 'budgetDataService', '$uibModal'];

        budget: Models.IBudget;
        budgetItems: Models.IBudgetItem[];

        newBudgetItem: Models.IBudgetItem;
        delBudgetItem: Models.IBudgetItem;

        //Enums
        budgetItemTypes = Models.BudgetItemType;
        frequencyTypes = Models.PaymentFrequency;

        constructor(
            private $scope: IScope,
            private $stateParams,
            private messageService: Modules.MessageDisplay.IMessageService,
            private $validation,
            private budgetDataService: Services.IBudgetDataService,
            private $uibModal
        ) {
            $scope.vm = this;
            this.loadBudgetDetails($stateParams.budgetId);
        }

        loadBudgetDetails(budgetId) {
            this.budgetDataService.getBudgetDetailsById(budgetId).then(
            (response) => {
                this.budget = response.data.budget;
                this.budgetItems = response.data.budgetItems;
            });
        }

        addBudgetItem(form) {
            this.messageService.clear();
            this.$validation.validate(form)
            .success(() => {
                this.newBudgetItem.budgetId = this.budget.id;
                this.budgetDataService.addBudgetItem(this.newBudgetItem).then(
                (response) => {
                    this.messageService.addInfo(response.data.response);
                    this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                    this.loadBudgetDetails(this.budget.id);
                }, (err) => {
                    var message = "Failed to add transaction";
                    this.messageService.addError(message);
                    this.messageService.removeErrorAfterSeconds(message, 4);
                });
            })
                .error(() => {
                var message = "Validation issues found, please review required fields";
                this.messageService.addError(message);
                this.messageService.removeErrorAfterSeconds(message, 4);
            });
        }

        setWaitingDelete(item) {
            this.delBudgetItem = item;
        }

        deleteBudgetItem(budget) {
            var modalInstance = this.$uibModal.open({
                animation: true,
                templateUrl: '/App/views/modals/confirm-modal.html',
                controller: 'confirmModalController',
                resolve: {
                    modalData: () => {
                        return {
                            modalHead: 'Delete Transaction',
                            modalBody: 'Are you sure you want to delete this transaction?'
                        };
                    }
                }
            });

            modalInstance.result.then(confirm => {
                if (confirm) {
                    this.budgetDataService.deleteBudgetItem(budget).then(
                    (response) => {
                        this.messageService.addInfo(response.data.response);
                        this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                        this.loadBudgetDetails(this.budget.id);
                    }, (err) => {
                        var message = "Failed to delete transaction";
                        this.messageService.addError(message);
                        this.messageService.removeErrorAfterSeconds(message, 4);
                    });
                }
            });
        }
    }
}