(function () {
    'use strict';

    var app = angular.module("PersonalFinance", ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);

    app.config(function ($routeProvider) {
        $routeProvider
            .when('/home', {
                controller: 'homeController',
                templateUrl: '/app/views/home.html'
            })

            .when('/login', {
                controller: 'loginController',
                templateUrl: '/app/views/login.html'
            })

            .when('/signup', {
                controller: 'signupController',
                templateUrl: '/app/views/signup.html'
            })

            .when('/dashboard', {
                controller: 'dashboardController',
                templateUrl: '/app/views/dashboard.html'
            })

            .when('/accounts', {
                controller: 'accountsController',
                templateUrl: '/app/views/accounts.html'
            })

            .when('/accounts/:accountName/:accountId', {
                controller: 'accountDetailController',
                templateUrl: '/app/views/accountDetail.html'
            })

            .when('/household', {
                controller: 'householdController',
                templateUrl: '/app/views/household.html'
            })

            .when('/budget', {
                controller: 'budgetController',
                templateUrl: '/app/views/budget.html'
            })

            .otherwise({ redirectTo: '/home' });
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(["authService", function (authService) {
        authService.fillAuthData();
    }]);
}())