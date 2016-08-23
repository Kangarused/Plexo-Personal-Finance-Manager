
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
    'message-utils',
    'CaseFilter',
    'ncy-angular-breadcrumb'
    ])
    .constant("settings", personalFinanceSettings)

    .config([
        '$stateProvider', '$urlRouterProvider', ($stateProvider, $urlRouterProvider) => {

            $urlRouterProvider.otherwise("/login");

            $urlRouterProvider.when('/home', '/home/dashboard');
            $stateProvider.state('home', {
                url: '/home',
                templateUrl: "App/views/home.html",
                controller: "homeController",
                ncyBreadcrumb: {
                    label: 'Home',
                    skip: true 
                }
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

            $stateProvider.state('home.dashboard', {
                url: '/dashboard',
                templateUrl: "App/views/dashboard.html",
                controller: "dashboardController",
                ncyBreadcrumb: {
                    label: 'Dashboard'
                }
            });

            $stateProvider.state('home.accounts', {
                url: '/accounts',
                templateUrl: "App/views/accounts/accounts.html",
                controller: "accountsController",
                ncyBreadcrumb: {
                    label: 'Accounts',
                    parent: 'home.dashboard'
                }
            });

            $stateProvider.state('home.accounts.detail', {
                url: '/details/:accountId',
                templateUrl: "App/views/accounts/account-detail.html",
                controller: "accountDetailsController",
                ncyBreadcrumb: {
                    label: 'Account Details'
                }
            });

            $stateProvider.state('home.households', {
                url: '/households',
                templateUrl: "App/views/households/household.html",
                controller: "householdsController",
                ncyBreadcrumb: {
                    label: 'Households',
                    parent: 'home.dashboard'
                }
            });


            $urlRouterProvider.when('/home/budgets', '/home/budgets/list');
            $stateProvider.state('home.budgets', {
                url: '/budgets',
                templateUrl: "App/views/budgets/budget.html",
                controller: "budgetsController",
                ncyBreadcrumb: {
                    label: 'Budgets',
                    parent: 'home.dashboard',
                    skip: true 
                }
            });

            $stateProvider.state('home.budgets.list', {
                url: '/list',
                templateUrl: "App/views/budgets/budget-list.html",
                controller: 'budgetListController',
                ncyBreadcrumb: {
                    label: 'Budgets'
                }
            });

            $stateProvider.state('home.budgets.details', {
                url: '/details/{budgetId:int}',
                templateUrl: "App/views/budgets/budget-detail.html",
                controller: 'budgetDetailsController',
                ncyBreadcrumb: {
                    label: 'Budget Details',
                    parent: 'home.budgets.list'
                }
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
    .controller('budgetListController', PersonalFinance.Controllers.BudgetListController)
    .controller('budgetDetailsController', PersonalFinance.Controllers.BudgetDetailsController)

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

        $rootScope.$on('$stateChangeStart', (e, toState, params) => {
            var allowAnonymnous = [
                "login", "signup"
            ];
            
            if ($.inArray(toState.name, allowAnonymnous) > -1){
                return;
            }

            if (!authService.isUserAuth()) {
                e.preventDefault();
                $state.go('login');
            }
        });
    }]);
    