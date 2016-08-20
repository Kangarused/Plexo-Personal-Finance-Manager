module PersonalFinance.Controllers {
    export class DashboardController {
        static $inject = ['$scope', 'accountDataService', 'transactionDataService'];

        message: string;
        accounts: Models.IAccount[];
        recentTransactions: Models.ITransaction[]; 

        constructor(
            private $scope: IScope,
            private accountDataService: Services.IAccountDataService,
            private transactionDataService: Services.ITransactionDataService
        ) {
            window["dashboard"] = this;
            $scope.vm = this;

            this.loadAccounts();
        }

        setup() {
            this.message = "";
            this.recentTransactions = [];
        }

        loadAccounts() {
            this.accountDataService.getAccounts().then(
            (response) => {
                this.accounts = response.data;
            });
        }
    }
}