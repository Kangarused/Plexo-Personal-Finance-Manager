(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller("accountsController", [
        '$scope',
        'accountService',
        accountsController
    ]);

    function accountsController($scope, accountService) {
        // Fetch account data for current user household.
        accountService.getAccounts().success(function (data) {
            $scope.accounts = data;
        });

        $scope.accountData = {
            name: ""
        };


        $scope.createAccount = function (accountData) {
            accountService.createAccount(accountData).success(function (data) {
                // On success, reassign accounts to updated account list returned from server.
                $scope.accounts = data;
                $scope.accountData.name = "";
            });
        };

        $scope.deleteAccount = function (accountId) {
            accountService.deleteAccount(accountId).success(function (data) {
                // On success, reassign accounts to updated account list returned from server.
                $scope.accounts = data;
            });
        }
    }
}())