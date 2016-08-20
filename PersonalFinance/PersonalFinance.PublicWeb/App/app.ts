
var angularApplication = angular.module('personalFinance',
[
    'ui.router',
    'ui.bootstrap', 
    'datatables',
    'datatables.bootstrap',
    'smart-table',
    'datetimepicker',
    'ngSanitize', 
    'validation',
    'validation.rule',
    'validation.schema',
    'PubSub',
    'show.when.loading',
    'ngCookies',
    'error-utils',
    'CaseFilter'
    ])
    .constant("settings", personalFinanceSettings)

    .config([
        '$stateProvider', '$urlRouterProvider', ($stateProvider, $urlRouterProvider) => {

            $urlRouterProvider.otherwise("/login");

            $stateProvider.state('home', {
                url: '/home',
                templateUrl: "App/views/home.html",
                controller: "homeController"
            });

            $stateProvider.state('login', {
                url: '/login',
                templateUrl: "App/views/login.html",
                controller: "loginController"
            });

            $stateProvider.state('signup', {
                url: '/signup',
                templateUrl: "App/views/signup.html",
                controller: "signupController"
            });

            $stateProvider.state('dashboard', {
                url: '/dashboard',
                templateUrl: "App/views/dashboard.html",
                controller: "dashboardController"
            });

            $stateProvider.state('accounts', {
                url: '/accounts',
                templateUrl: "App/views/accounts/accounts.html",
                controller: "accountsController"
            });

            $stateProvider.state('accounts.detail', {
                url: '/accounts/:accountId',
                templateUrl: "App/views/accounts/account-detail.html",
                controller: "accountDetailsController"
            });

            $stateProvider.state('households', {
                url: '/households',
                templateUrl: "App/views/households/household.html",
                controller: "householdsController"
            });

            $stateProvider.state('budgets', {
                url: '/budgets',
                templateUrl: "App/views/budgets/budget.html",
                controller: "budgetsController"
            });
        }
    ])

    .directive('navigationDisplay', PersonalFinance.Directives.NavigationDisplay.factory())
    .directive('stServerPagination', Common.Directives.stServerPagination)

    .service('authService', PersonalFinance.Services.AuthService)
    .service('userAccountService', PersonalFinance.Services.UserAccountService)
    .service('browserStorageService', PersonalFinance.Services.BrowserStorageService)
    .service('accountDataService', PersonalFinance.Services.AccountDataService)
    .service('transactionDataService', PersonalFinance.Services.TransactionDataService)
    .service('categoryDataService', PersonalFinance.Services.CategoryDataService)
    .service('browserStorageService', PersonalFinance.Services.BrowserStorageService)
    .service('budgetDataService', PersonalFinance.Services.BudgetDataService)

    .controller('budgetsController', PersonalFinance.Controllers.BudgetsController)
    .controller('accountDetailsController', PersonalFinance.Controllers.AccountDetailsController)
    .controller('accountsController', PersonalFinance.Controllers.AccountsController)
    .controller('dashboardController', PersonalFinance.Controllers.DashboardController)
    .controller('homeController', PersonalFinance.Controllers.HomeController)
    .controller('signupController', PersonalFinance.Controllers.SignupController)
    .controller('loginController', PersonalFinance.Controllers.LoginController)

    .factory('PublicHttpInterceptor', PersonalFinance.Factories.PublicHttpInterceptor)

    .config(['$httpProvider', ($httpProvider: ng.IHttpProvider) => {
        $httpProvider.interceptors.push('PublicHttpInterceptor');
    }])

    .run(['$rootScope', '$state', 'authService', ($rootScope, $state, authService:PersonalFinance.Services.IAuthService) => {

        $rootScope.enumDescriptions = PersonalFinance.Models.EnumLabelDictionary;

        $rootScope.$on('$stateChangeStart', (e, toState) => {
            var allowAnonymnous = [
                "login", "signup"
            ];
            
            if ($.inArray(toState.name, allowAnonymnous) > -1){
                return; // no need to redirect 
            }

            // now, redirect only not authenticated
            
            if (!authService.isUserAuth()) {
                e.preventDefault(); // stop current execution
                $state.go('login'); // go to login
            }
        });
    }]);
    