module PersonalFinance.Directives {

    export interface INavigationDisplay extends ng.IScope {
        name: string; 
        logout(): void;
        isAuth:boolean;
    }

    export class NavigationDisplay implements ng.IDirective {
        restrict = 'E';
        scope = {};
        template =
        '<ul class="nav navbar-nav" ng-show="isAuth">' +
            '<li>' +
                '<a style="cursor: pointer;" ui-sref="budget">Budgets </a>' +
            '</li>' +
            '<li>' +
                '<a style="cursor: pointer;" ui-sref="accounts">Accounts </a>' +
            '</li>' +
            '<li>' +
                '<a style="cursor: pointer;" ui-sref="households">Households </a>' +
            '</li>' +
        '</ul>' +
        '<ul class="nav navbar-nav navbar-right">' +
            '<li ng-show="isAuth">' +
                '<p class="navbar-text"><span class="fa fa-user"> </span> {{name}} </p>' +
            '</li>' +
            '<li ng-show="isAuth">' +
                '<a style="cursor:pointer; color:white; font-style:0.8em !important;" ng-click="logout()"><span class="fa fa-sign-out"></span>Log Out</a> ' +
            '</li> ' +
            '<li ng-show="!isAuth"> ' +
                '<a style="cursor:pointer; color:white; font-style:0.8em !important;" ui-sref="signup"> <span class="fa fa-user"></span> Sign Up</a>' +
            '</li>' +
            '<li ng-show="!isAuth"> ' +
                '<a style="cursor:pointer; color:white; font-style:0.8em !important;" ui-sref="login"> <span class="fa fa-sign-in"></span> Login</a>' +
            '</li>' +
        '</ul>'
        ;
        
        constructor(private authService: Services.AuthService, private $state: ng.ui.IStateService) {

        }
         
        link = (scope: INavigationDisplay, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, ctrl: any) => {
            scope.$watch(() => {
                return this.authService.getCurrentUser();
            }, (v:Models.ICurrentUser)  => {
                if (v) {
                    scope.name = v.name;
                    scope.isAuth = true;
                }
            });

            scope.logout = () => {
                this.authService.logOut();
                scope.isAuth = false;
                this.$state.go("login");
            }
        }

        static factory(): ng.IDirectiveFactory {
            var directive = (authService: Services.AuthService, $state: ng.ui.IStateService) => new NavigationDisplay(authService, $state);
            directive.$inject = ['authService','$state'];
            return directive;
        }
    }
}