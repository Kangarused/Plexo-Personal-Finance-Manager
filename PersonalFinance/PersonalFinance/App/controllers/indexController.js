(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller('indexController', [
        '$scope',
        '$location',
        'authService',
        indexController
    ]);

    function indexController($scope, $location, authService) {
        $scope.navbarExpanded = false;

        $scope.logout = function () {
            authService.logout();
            $location.path('/home');
        }

        $scope.authentication = authService.authentication;
    }
}());