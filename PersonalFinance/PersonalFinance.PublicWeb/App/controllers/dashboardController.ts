module PersonalFinance.Controllers {
    export class DashboardController {
        static $inject = ['$scope', 'budgetDataService', 'transactionDataService'];

        budget: Models.IRecentBudgetResponse;
        message: string;
        recentTransactions: Models.ITransaction[]; 

        constructor(
            private $scope: IScope,
            private budgetDataService: Services.IBudgetDataService,
            private transactionDataService: Services.ITransactionDataService
        ) {
            window["dashboard"] = this;
            $scope.vm = this;

            this.loadRecentBudgets();
        }

        setup() {
            this.message = "";
            this.recentTransactions = [];
        }

        loadRecentBudgets() {
            this.budgetDataService.getRecentBudget().then(
            (response) => {
                this.budget = response.data;
            });
        }
    }
}