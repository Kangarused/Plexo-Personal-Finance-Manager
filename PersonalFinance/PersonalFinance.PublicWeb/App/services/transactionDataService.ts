module PersonalFinance.Services {

    export interface ITransactionDataService {
        getTransactions(pagingFilter: Models.IPagingFilter): ng.IPromise<Models.IResponseObject<Models.ITransaction[]>>;
        createTransaction(transaction: Models.ITransaction): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        updateTransaction(transaction: Models.ITransaction): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteTransaction(id: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
    } 

    export class TransactionDataService implements ITransactionDataService {
        static $inject = ['$http'];

        constructor(public $http: ng.IHttpService) {
            
        }

        getTransactions(pagingFilter: Models.IPagingFilter): ng.IPromise<Models.IResponseObject<Models.ITransaction[]>> {
            return this.$http.post("/api/Transaction/GetTransactions/", pagingFilter);
        }

        createTransaction(transaction: Models.ITransaction): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Transaction/CreateTransaction/", transaction);
        }

        updateTransaction(transaction: Models.ITransaction): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Transaction/UpdateTransaction/", transaction);
        }

        deleteTransaction(id: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Transaction/DeleteTransaction/", id);
        }
    }
}