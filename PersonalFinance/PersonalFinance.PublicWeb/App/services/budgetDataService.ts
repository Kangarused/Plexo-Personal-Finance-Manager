module PersonalFinance.Services {
    export interface IBudgetDataService {
        getBudgetItems(): ng.IPromise<Models.IResponseObject<Models.IBudgetItem[]>>;
    }

    export class BudgetDataService implements IBudgetDataService {
        static $inject = ['$http'];
         
        constructor(public $http: ng.IHttpService) {
            
        }

        getBudgetItems(): ng.IPromise<PersonalFinance.Models.IResponseObject<PersonalFinance.Models.IBudgetItem[]>> {
            return this.$http.get("/api/Budget/GetBudgetItems/");
        }
    }
}