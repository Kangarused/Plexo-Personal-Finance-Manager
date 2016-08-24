module PersonalFinance.Controllers {
    export class AccountsController {
        static $inject = ['$scope'];

        constructor(private $scope: IScope) {
            $scope.vm = this;
        }
    }
}