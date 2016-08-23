module PersonalFinance.Controllers {
    export class BudgetsController {
        static $inject = ['$scope'];
        constructor(private $scope: IScope) {
            $scope.vm = this;
        }
    }
}