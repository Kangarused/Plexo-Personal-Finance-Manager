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

        overviewChartDataset = {
            data: [],
            labels: [],
            options: {
                tooltips: {
                    enabled: true,
                    mode: 'single',
                    callbacks: {
                        label: (tooltipItem, data) => {
                            var datasetLabel = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return 'Balance: $' + datasetLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }
                    }
                }
            }
        };

        spendSaveChartDataset = {
            data: [],
            labels: [],
            colors: ['#aaeeaa', '#eeaaaa'],
            options: {
                tooltips: {
                    enabled: true,
                    mode: 'single',
                    callbacks: {
                        label: (tooltipItem, data) => {
                            var datasetLabel = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return 'Balance: $' + datasetLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }
                    }
                }
            }
        };

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
                this.calculateChartData();
            });
        }

        calculateChartData() {
            this.overviewChartDataset.data.length = 0;
            this.overviewChartDataset.labels.length = 0;

            var budget = this.budget.allocatedAmount;
            var data = [];
            if (this.budget.type === 'Savings') {
                budget = 0;
            }

            data.push(budget);
            this.overviewChartDataset.labels.push('Start');
            this.budgetItems.forEach((item, index) => {
                if (item.type === 'Income') {
                    budget += item.amount;
                } else {
                    budget -= item.amount;
                }
                data.push(budget);
                this.overviewChartDataset.labels.push(item.name.substring(0, 30));
            });

            this.overviewChartDataset.data.push(data);
        }

        //calculateSpendSaveChartData() {
        //    var budget = this.budget.allocatedAmount;
        //    var spend = [];
        //    var save = [];
        //    if (this.budget.type === 'Savings') {
        //        budget = 0;
        //    }

        //    this.budgetItems.forEach((item, index) => {
        //        budget += item.amount;
        //        data.push(budget);
        //        this.overviewChartDataset.labels.push(item.name.substring(0, 10));
        //    });

        //    this.overviewChartDataset.data.push(data);
        //}

        addBudgetItem(form) {
            this.messageService.clear();
            this.$validation.validate(form)
            .success(() => {
                this.newBudgetItem.budgetId = this.budget.id;
                this.budgetDataService.addBudgetItem(this.newBudgetItem).then(
                (response) => {
                    this.messageService.addInfo(response.data.response);
                    this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                    this.newBudgetItem = <Models.IBudgetItem>{};
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