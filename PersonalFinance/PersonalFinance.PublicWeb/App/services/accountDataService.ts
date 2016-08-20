module PersonalFinance.Services {

    export interface IAccountDataService {
        getAccounts(): ng.IPromise<Models.IResponseObject<Models.IAccount[]>>;
        getAccountById(id: number): ng.IPromise<Models.IResponseObject<Models.IAccount>>;
        createAccount(account: Models.IAccount): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteAccount(accountId: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
    }

    export class AccountDataService implements IAccountDataService {
        static $inject = ['$http'];

        constructor(public $http: ng.IHttpService) {
            
        }
         
        getAccounts(): ng.IPromise<Models.IResponseObject<Models.IAccount[]>> {
            return this.$http.get("/api/Account/GetAccounts");
        }

        getAccountById(id: number): ng.IPromise<Models.IResponseObject<Models.IAccount>> {
            return this.$http.get("/api/Account/GetAccountById" + id);
        }

        createAccount(account: PersonalFinance.Models.IAccount): ng.IPromise<PersonalFinance.Models.IResponseObject<PersonalFinance.Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Account/CreateAccount/", account);
        }

        deleteAccount(accountId: number): ng.IPromise<PersonalFinance.Models.IResponseObject<PersonalFinance.Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Account/DeleteAccount/", accountId);
        }
    }
}