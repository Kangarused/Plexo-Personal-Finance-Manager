(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller("accountsController", [
        '$scope',
        'accountService',
        accountsController
    ]);

    function accountsController($scope, accountService) {
        $scope.accountName = "";
        $scope.accounts = accountService.accounts;

        $scope.removeAccount = function (accountName) {
            accountService.removeAccount(accountName);
            accountName = "";
        }

        $scope.addAccount = function (accountName) {
            if (accountName) {
                accountService.addAccount(accountName);
                $scope.accountName = "";
            }
            else {
                alert("Must provide an account name!");
            }
        }
    }
}())