module PersonalFinance.Controllers {
    export class BudgetsController {
        static $inject = ['$scope', 'errorService', '$validation', 'budgetDataService'];

        recentBudget: Models.IRecentBudgetResponse;
        budgets: Models.IBudget[];
        newBudget: Models.IBudget;
        delBudget: Models.IBudget;

        budgetTypes = Models.BudgetType;
        modalError: string = null;

        constructor(
            private $scope: IScope,
            private errorService: Modules.ErrorDisplay.ErrorService,
            private $validation,
            private budgetDataService: Services.IBudgetDataService
        ) {
            $scope.vm = this;

            this.loadBudgets();
        }

        loadRecentBudget() {
            this.budgetDataService.getRecentBudget().then(
            (response) => {
                this.recentBudget = response.data;
            });
        }

        loadBudgets() {
            this.budgetDataService.getBudgets().then(
            (response) => {
                this.budgets = response.data;
                this.loadRecentBudget();
            });
        }

        addBudget(form) {
            this.modalError = null;
            this.$validation.validate(form)
            .success(() => {
                this.budgetDataService.addBudget(this.newBudget).then(
                    (response) => {
                        this.errorService.addInfo("Budget created successfully");
                        this.loadBudgets();
                    }, (err) => {
                        this.errorService.addError("Budget creation Failed");
                    });
            })
            .error(() => {
                this.modalError = "Validation issues found, please review your submission";
            });
        }

        setWaitingDelete(budget) {
            this.delBudget = budget;
        }

        deleteBudget() {
            this.budgetDataService.deleteBudget(this.delBudget).then(
                (response) => {
                this.errorService.addInfo(response.data.response);
                this.loadBudgets();
            });
        }
    }
}