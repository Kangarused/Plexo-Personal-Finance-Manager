(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller('loginController', [
        '$scope',
        '$location',
        'authService',
        loginController
    ]);

    function loginController ($scope, $location, authService) {
        $scope.message = "";

        $scope.loginData = {
            userName: "",
            password: "",
        }

        $scope.login = function () {
            authService.login($scope.loginData).then(function (response) {
                $location.path("/home");
            },
            function (error) {
                $scope.message = error.error_description;
            });
        };
    }
}())