module PersonalFinance.Controllers {
    export class AccountsController {
        static $inject = ['$scope', 'accountDataService'];

        accounts: Models.IAccount[];
        newAccount: Models.IAccount;
         
        constructor(
            private $scope: INtaScope,
            private accountDataService: Services.IAccountDataService
        ) {
            this.loadAccounts();
        }

        loadAccounts() {
            this.accountDataService.getAccounts().then(
            (response) => {
                this.accounts = response.data;
            });
        }

        createAccount() {
            this.accountDataService.createAccount(this.newAccount).then(
            (response) => {
                this.loadAccounts();
            });
        }

        deleteAccount(accountId) {
            this.accountDataService.deleteAccount(accountId).then(
            (reponse) => {
                this.loadAccounts();
            });
        }
    }
}