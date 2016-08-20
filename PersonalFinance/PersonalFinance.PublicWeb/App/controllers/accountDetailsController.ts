module PersonalFinance.Controllers {

    export class AccountDetailsController {
        static $inject = [
            '$scope',
            '$stateParams',
            'accountDataService', 
            'transactionDataService',
            'categoryDataService'
        ];

        account: Models.IAccount;
        transactions: Models.ITransaction[];
        categories: Models.ICategory[];
        newTransaction: Models.ITransaction;
        pagingFilter: Models.IPagingFilter;
        propertiesToSearchBy = {};

        constructor(
            private $scope: INtaScope,
            private $stateParams,
            private accountDataService: Services.IAccountDataService,
            private transactionDataService: Services.ITransactionDataService,
            private categoryDataService: Services.ICategoryDataService
        ) {
            window["account"] = this;
            $scope.vm = this;

            this.refreshAccount();
            this.refreshTransactions(); 
        }

        refreshAccount() {
            this.accountDataService.getAccountById(this.$stateParams.accountId).then(
            (response) => {
                this.account = response.data;
            });
        }

        refreshTransactions() {
            this.transactionDataService.getTransactions(this.pagingFilter).then(
            (response) => {
                this.transactions = response.data;
                this.pagingFilter.totalItems = response.data.length;
            });
        }

        createTransaction(transactionData) {
            this.transactionDataService.createTransaction(transactionData).then(
            (response) => {
                this.resetCreateTransactionForm()
                this.refreshTransactions();
                this.refreshAccount();
            }, (err) => {
                //todo: Display error
            }
        )};

        updateTransaction(transaction) {
            this.transactionDataService.updateTransaction(transaction).then(
                (response) => {
                this.refreshTransactions();
                this.refreshAccount();
            }, (err) => {
                //todo: Display Error       
            }    
        )};

        deleteTransaction(transaction) {
            this.transactionDataService.deleteTransaction(transaction).then(
            (response) => {
                this.refreshTransactions();
                this.refreshAccount();
            }, (err) => {
                //todo: Display Error       
            } 
        )};

        resetCreateTransactionForm() {
            this.newTransaction.name = "";
            this.newTransaction.description = "";
            this.newTransaction.amount = 0.0;
            this.newTransaction.reconciledAmount = 0.0;
            //todo: add category here
            this.newTransaction.transactionDate = "";
        };

        getCategories() {
            this.categoryDataService.getCategories().then(
            (response) => {
                this.categories = response.data;
            });
        }

        searchTransaction = {
            accountId: {
                value: this.$stateParams.accountId,
                searchBy: true
            },
            description: {
                value: null,
                searchBy: false
            },
            minAmount: {
                value: null,
                searchBy: false
            },
            maxAmount: {
                value: null,
                searchBy: false
            },
            isReconciled: {
                value: null,
                searchBy: false
            },
            category: {
                value: null,
                searchBy: false
            },
            minDate: {
                value: null,
                searchBy: false
            },
            maxDate: {
                value: null,
                searchBy: false
            }
        };
    }
}