(function () {
    'use strict';

    var app = angular.module("PersonalFinance", ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

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

            .otherwise({ redirectTo: '/home' });
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(["authService", function (authService) {
        authService.fillAuthData();
    }]);
}());