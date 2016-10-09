
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
    'ncy-angular-breadcrumb',
    'chart.js'
    ])
    .constant("settings", personalFinanceSettings)

    .config([
        '$stateProvider', '$urlRouterProvider', ($stateProvider, $urlRouterProvider) => {

            $urlRouterProvider.otherwise("/login");

            $stateProvider.state('login', {
                url: '/login',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/login.html",
                        controller: "loginController"
                    }
                }
            });

            $stateProvider.state('signup', {
                url: '/signup',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/signup.html",
                        controller: "signupController"
                    }
                }
            });


            $urlRouterProvider.when('/home', '/home/dashboard');
            $stateProvider.state('home', {
                url: '/home',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/home.html",
                        controller: "homeController"
                    }
                },
                ncyBreadcrumb: {
                    label: 'Home',
                    skip: true 
                }
            });

            $stateProvider.state('home.dashboard', {
                url: '/dashboard',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/dashboard.html",
                        controller: "dashboardController"
                    }
                },
                ncyBreadcrumb: {
                    label: 'Dashboard'
                }
            });

            $stateProvider.state('home.groups', {
                url: '/groups',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/groups/groups-list.html",
                        controller: "groupsController"
                    }
                },
                ncyBreadcrumb: {
                    label: 'Groups',
                    parent: 'home.dashboard'
                }
            });

            $stateProvider.state('home.groups.details', {
                url: '/details/{groupId:int}',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/groups/group-detail.html",
                        controller: "groupsDetailController"
                    }
                },
                ncyBreadcrumb: {
                    label: 'Group Details',
                    parent: 'home.groups'
                }
            });

            $stateProvider.state('home.bills', {
                url: '/bills',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/bills/bills-list.html",
                        controller: "billsController"
                    }
                },
                ncyBreadcrumb: {
                    label: 'Bills',
                    parent: 'home.dashboard'
                }
            });

            $stateProvider.state('home.budgets', {
                url: '/budgets',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/budgets/budget-list.html",
                        controller: 'budgetListController'
                    }
                },
                ncyBreadcrumb: {
                    label: 'Budgets',
                    parent: 'home.dashboard'
                }
            });

            $stateProvider.state('home.budgets.details', {
                url: '/details/{budgetId:int}',
                views: {
                    "main-body@": {
                        templateUrl: "App/views/budgets/budget-detail.html",
                        controller: 'budgetDetailsController'
                    }
                },
                ncyBreadcrumb: {
                    label: 'Budget Details',
                    parent: 'home.budgets'
                }
            });
        }
    ])

    .directive('navigationDisplay', PersonalFinance.Directives.NavigationDisplay.factory())
    .directive('stServerPagination', Common.Directives.stServerPagination)

    .service('authService', PersonalFinance.Services.AuthService)
    .service('userAccountService', PersonalFinance.Services.UserAccountService)
    .service('browserStorageService', PersonalFinance.Services.BrowserStorageService)
    .service('transactionDataService', PersonalFinance.Services.TransactionDataService)
    .service('categoryDataService', PersonalFinance.Services.CategoryDataService)
    .service('browserStorageService', PersonalFinance.Services.BrowserStorageService)
    .service('budgetDataService', PersonalFinance.Services.BudgetDataService)
    .service('billDataService', PersonalFinance.Services.BillDataService)
    .service('groupDataService', PersonalFinance.Services.GroupDataService)

    .controller('budgetListController', PersonalFinance.Controllers.BudgetListController)
    .controller('budgetDetailsController', PersonalFinance.Controllers.BudgetDetailsController)

    .controller('groupsController', PersonalFinance.Controllers.GroupsController)
    .controller('groupsDetailController', PersonalFinance.Controllers.GroupDetailController)
    .controller('billsController', PersonalFinance.Controllers.BillsController)

    .controller('dashboardController', PersonalFinance.Controllers.DashboardController)
    .controller('homeController', PersonalFinance.Controllers.HomeController)
    .controller('signupController', PersonalFinance.Controllers.SignupController)
    .controller('loginController', PersonalFinance.Controllers.LoginController)

    .controller('confirmModalController', PersonalFinance.Controllers.ConfirmModalController)

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
    