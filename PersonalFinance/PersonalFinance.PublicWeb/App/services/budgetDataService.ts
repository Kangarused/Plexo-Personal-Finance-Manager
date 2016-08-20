module PersonalFinance.Services {

    export interface IBudgetDataService {
        getBudgets(): ng.IPromise<Models.IResponseObject<Models.IBudget[]>>;
        getBudgetItems(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudgetItem[]>>;
        getRecentBudget(): ng.IPromise<Models.IResponseObject<Models.IRecentBudgetResponse>>;
        addBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
    }

    export class BudgetDataService implements IBudgetDataService {
        static $inject = ['$http'];
         
        constructor(public $http: ng.IHttpService) {
            
        }

        getBudgets(): ng.IPromise<Models.IResponseObject<Models.IBudget[]>> {
            return this.$http.get("/api/Budget/GetBudgetsForUser");
        }

        getBudgetItems(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudgetItem[]>> {
            return this.$http.get("/api/Budget/GetBudgetItemsForBudget/" + budgetId);
        }

        getRecentBudget(): ng.IPromise<Models.IResponseObject<Models.IRecentBudgetResponse>> {
            return this.$http.get("/api/Budget/GetRecentBudgetItems");
        }

        addBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Budget/AddBudget", budget);
        }

        deleteBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Budget/DeleteBudgetById", budget);
        }
    }
}