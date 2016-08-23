module PersonalFinance.Services {

    export interface IBudgetDataService {
        getBudgetById(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudget>>;
        getBudgetDetailsById(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudgetDetailsResponse>>;
        getBudgets(): ng.IPromise<Models.IResponseObject<Models.IBudget[]>>;
        getBudgetItems(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudgetItem[]>>;
        getRecentBudget(): ng.IPromise<Models.IResponseObject<Models.IRecentBudgetResponse>>;
        addBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteBudget(budget: Models.IBudget): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        addBudgetItem(budgetItem: Models.IBudgetItem): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteBudgetItem(budgetItem: Models.IBudgetItem): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
    }

    export class BudgetDataService implements IBudgetDataService {
        static $inject = ['$http'];
         
        constructor(public $http: ng.IHttpService) {
            
        }

        getBudgetById(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudget>> {
            return this.$http.get("/api/Budget/GetBudgetById/" + budgetId);
        }

        getBudgetDetailsById(budgetId: number): ng.IPromise<Models.IResponseObject<Models.IBudgetDetailsResponse>> {
            return this.$http.get("/api/Budget/GetBudgetDetailsById/" + budgetId);
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

        addBudgetItem(budgetItem: Models.IBudgetItem): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Budget/AddBudgetItem", budgetItem);
        }

        deleteBudgetItem(budgetItem: Models.IBudgetItem): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Budget/DeleteBudgetItem", budgetItem);
        }
    }
}