(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.controller("accountDetailController", [
        '$scope',
        '$routeParams',
        'accountService',
        'transactionService',
        'categoryService',
        accountDetailController
    ]);

    function accountDetailController($scope, $routeParams, accountService, transactionService, categoryService) {

        $scope.pagingInfo = {
            accountId: $routeParams.accountId,
            page: 1,
            itemsPerPage: 5,
            sortBy: 'TransactionDate',
            reverse: true,
            totalItems: 0
        };

        var propertiesToSearchBy = {};

        $scope.refreshAccount = function () {
            accountService.getAccountById($routeParams.accountId).success(function (data) {
                $scope.account = data;
            });
        };
        
        $scope.refreshTransactions = function () {
            transactionService.getTransactions($scope.pagingInfo, propertiesToSearchBy).success(function (response) {
                $scope.transactions = response.data;
                $scope.pagingInfo.totalItems = response.count;
            });
        };

        $scope.sort = function (sortBy) {
            if (sortBy == $scope.pagingInfo.sortBy) {
                $scope.pagingInfo.reverse = !$scope.pagingInfo.reverse;
            }
            else {
                $scope.pagingInfo.sortBy = sortBy;
                $scope.pagingInfo.reverse = true;
            }
            $scope.pagingInfo.page = 1;
            $scope.refreshTransactions();
        };

        $scope.pageChanged = function () {
            $scope.refreshTransactions();
        };

        $scope.refreshAccount();
        $scope.refreshTransactions();

        var newTransactionDefault = {
            accountId: $routeParams.accountId,
            description: "",
            amount: 0.0,
            reconciled: 0.0,
            category: "",
            transactionDate: new Date()
        };
        $scope.transactionData = newTransactionDefault;
        $scope.deposit = true;
        $scope.createIsCollapsed = true;

        $scope.searchTransaction = {
            accountId: {
                value: $routeParams.accountId,
                searchBy: true // automatically search by AccountId
            },
            description: {
                value: null,
                searchBy: false
            },
            minAmount: {
                value: null,
                searchBy: false
            },
            maxAmount: {
                value: null,
                searchBy: false
            },
            isReconciled: {
                value: null,
                searchBy: false
            },
            category: {
                value: null,
                searchBy: false
            },
            minDate: {
                value: null,
                searchBy: false
            },
            maxDate: {
                value: null,
                searchBy: false
            }
        };
        $scope.searchIsCollapsed = true;

        $scope.resetCreateTransactionForm = function () {
            $scope.transactionData.description = "";
            $scope.transactionData.amount = 0.0;
            $scope.transactionData.reconciled = 0.0;
            $scope.transactionData.category = "";
            $scope.transactionData.transactionDate = new Date();
        };

        $scope.createTransaction = function (transactionData) {
            transactionService.createTransaction(transactionData).success(function (response) {
                $scope.resetCreateTransactionForm()
                $scope.refreshTransactions();
                $scope.refreshAccount();
            });
        };

        $scope.updateTransaction = function (transaction) {
            transactionService.updateTransaction(transaction).success(function (response) {
                $scope.refreshTransactions();
                $scope.refreshAccount();
            });
        };

        $scope.deleteTransaction = function (transaction) {
            transactionService.deleteTransaction(transaction).success(function (response) {
                $scope.refreshTransactions();
                $scope.refreshAccount();
            });
        };

        $scope.getCategories = function (val) {
            return categoryService.getCategories(val).then(function (response) {
                var categories = [];
                angular.forEach(response.data, function (item) {
                    categories.push(item);
                });
                return categories;
            });
        };

        $scope.searchForTransaction = function (transactionSearchData) {

            angular.forEach(transactionSearchData, function (val, key) {
                if (val.searchBy) {
                    this[key] = val.value;
                }
            }, propertiesToSearchBy);

            console.log(propertiesToSearchBy);

            $scope.pagingInfo.page = 1;
            transactionService.getTransactions($scope.pagingInfo, propertiesToSearchBy).success(function (response) {
                $scope.transactions = response.data;
                $scope.pagingInfo.totalItems = response.count;
            });
        };

        $scope.clearSearch = function (transactionSearchData) {
            angular.forEach(transactionSearchData, function (val, key) {
                if (key !== 'accountId') {
                    val.value = null;
                    val.searchBy = false;
                }
            });

            propertiesToSearchBy = null;
            propertiesToSearchBy = {};
            $scope.pagingInfo.page = 1;
            $scope.refreshTransactions();
        };

        //datepicker
        $scope.calendar = {
            createTransactionOpened: false,
            minDateSearchOpened: false,
            maxDateSearchOpened: false
        };
        $scope.open = function ($event, opened) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.calendar[opened] = true;
        };

        $scope.sortIcon = function (sortBy) {
            if ($scope.pagingInfo.sortBy == sortBy) {
                if ($scope.pagingInfo.reverse) {
                    return "glyphicon glyphicon-chevron-down";
                }
                else {
                    return "glyphicon glyphicon-chevron-up";
                }
            }
            return "";
        };

        $scope.sortClass = function (sortBy) {
            if ($scope.pagingInfo.sortBy == sortBy) {
                return "grey";
            }
            else {
                return "transactionTableHeader";
            }
        };
    }
}())