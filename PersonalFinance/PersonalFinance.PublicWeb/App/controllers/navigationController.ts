module PersonalFinance.Controllers {
    export class NavigationController {
        static $inject = ['$scope', 'authService'];

        public userConfirmed: boolean = false;

        constructor(
            private $scope: INtaScope,
            private authService: Services.IAuthService
        ) {
            $scope.vm = this;
            this.userConfirmed = authService.isUserAuth();
        }
    }
}