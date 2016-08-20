module PersonalFinance.Controllers {
    export class LoginController {
        static $inject = ['$scope', '$state', '$validation', 'authService', 'userAccountService', 'errorService'];

        loginData: Models.ILoginRequest;
         
        constructor(
            private $scope: INtaScope,
            private $state: ng.ui.IStateService,
            private $validation,
            private authService: Services.IAuthService,
            private userAccountService: Services.IUserAccountService, 
            private errorService: Modules.ErrorDisplay.IErrorService
        ) {
            $scope.vm = this;
        }

        login(form) {
            this.errorService.clear();
            this.$validation.validate(form)
            .success(() => {
                this.userAccountService.getLocalToken(this.loginData.userName, this.loginData.password).then(
                (response) => {
                    this.authService.setAuthData(response.data);
                    this.redirectIfAuthorised();
                }, () => {
                    this.errorService.addError("Error performing user validation.");
                });
            });
        }

        redirectIfAuthorised(): void {
            if (this.authService.isUserAuth()) {
                this.$state.go("dashboard");
            }
        }
    }
}