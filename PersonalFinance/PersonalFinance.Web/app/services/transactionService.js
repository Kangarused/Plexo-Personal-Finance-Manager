(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.factory("transactionService", [
        '$http',
        transactionService
    ]);

    function transactionService($http) {
        var serviceBase = "http://localhost:62733/";

        var _getTransactions = function (pagingInfo, searchData) {
            var params = {};
            angular.extend(params, pagingInfo, searchData);

            return $http.get(serviceBase + "api/transactions", { params: params });
        };

        var _createTransaction = function (transactionData) {
            return $http.post(serviceBase + "api/transactions/new", transactionData);
        };

        var _updateTransaction = function (transactionData) {
            return $http.put(serviceBase + "api/transactions/update", transactionData);
        };

        var _deleteTransaction = function (transaction) {
            return $http.delete(serviceBase + "api/transactions/delete", {
                params: {
                    transactionId: transaction.id,
                    categoryName: transaction.category,
                    accountId: transaction.accountId
                }
            });
        };

        return {
            getTransactions: _getTransactions,
            createTransaction: _createTransaction,
            updateTransaction: _updateTransaction,
            deleteTransaction: _deleteTransaction
        };
    }
}())