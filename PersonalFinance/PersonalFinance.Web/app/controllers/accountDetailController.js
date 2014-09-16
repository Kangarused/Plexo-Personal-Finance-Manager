(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller("accountDetailController", [
        '$scope',
        '$routeParams',
        'accountService',
        'transactionService',
        accountDetailController
    ]);

    function accountDetailController($scope, $routeParams, accountService, transactionService) {
        var blankTransaction = {
            account: $routeParams.accountName,
            description: "",
            amount: 0.0,
            reconciled: 0.0,
            category: "",
            date: new Date()
        };

        $scope.transactionData = blankTransaction;
        
        $scope.account = "";
        $scope.transactions = transactionService.transactions;
        $scope.deposit = true;
        $scope.editIsCollapsed = true;
        $scope.searchIsCollapsed = true;


        $scope.editTransaction = function (index) {
            $scope.transactionData = transactionService.transactions[index];
            $scope.editIsCollapsed = false;
        };

        $scope.deleteTransaction = function (index) {
            transactionService.destroy(index);
        };

        $scope.resetEditTransactionForm = function () {
            $scope.transactionData = blankTransaction;
        };

        //datepicker
        $scope.opened = false;
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        var accounts = accountService.accounts;

        for (var i = 0; i < accounts.length; i++) {
            if (accounts[i].name == $routeParams.accountName) {
                $scope.account = accounts[i];
            }
        }
    }
}())