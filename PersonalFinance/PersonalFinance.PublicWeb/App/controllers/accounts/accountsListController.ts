module PersonalFinance.Controllers {
    export class AccountsListController {
        static $inject = ['$scope', 'messageService', 'accountDataService'];

        accounts: Models.IAccount[];
        newAccount: Models.IAccount;

        constructor(
            private $scope: IScope,
            private messageService: PersonalFinance.Modules.MessageDisplay.MessageService,
            private accountDataService: Services.IAccountDataService
        ) {
            $scope.vm = this;
            this.loadAccounts();
        }

        loadAccounts() {
            this.accountDataService.getAccounts().then(
                (response) => {
                    this.accounts = response.data;
                });
        }

        createAccount() {
            this.newAccount.balance = 0;
            this.newAccount.reconciled = 0;
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