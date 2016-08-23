module PersonalFinance.Controllers {
    export class SignupController {
        static $inject = ['$scope', 'messageService', '$state', '$validation', '$timeout', 'authService', 'userAccountService'];

        registrationSuccessful = false;
        newUserAccount: Models.IUserAccount;

        constructor(
            private $scope: IScope, 
            private messageService: Modules.MessageDisplay.IMessageService,
            private $state: ng.ui.IStateService,
            private $validation,
            private $timeout: ng.ITimeoutService,
            private $authService: Services.IAuthService,
            private userAccountService: Services.IUserAccountService
        ) {
            $scope.vm = this;
        }

        register(form) {
            this.messageService.clear();
            this.$validation.validate(form)
            .success(() => {
                this.userAccountService.register(this.newUserAccount).then(
                (response) => {
                    this.registrationSuccessful = true;
                    this.messageService.addInfo("Registration successful! You will be redirected to login in 2 second...");
                    this.startTimer();
                }, (err) => {
                    this.messageService.addError("Registration Failed");
                });
            })
            .error(() => {
                this.messageService.addWarning("Validation issues found, please review your submission");
            });
        }

        startTimer() {
            this.$timeout(() => {
                this.$state.go('login');
            }, 2000);
        }
    }
}