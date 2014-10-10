(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.factory("accountService", [
        '$http',
        '$q',
        accountService
    ]);

    function accountService($http, $q) {
        var serviceBase = "http://localhost:62733/";

        var _getAccounts = function () {
            return $http.get(serviceBase + "api/accounts");
        };

        var _getAccountById = function (accountId) {
            return $http.get(serviceBase + "api/accounts/" + accountId.toString());
        };

        var _createAccount = function (accountData) {
            return $http.post(serviceBase + "api/accounts/new", accountData);
        };

        var _deleteAccount = function (accountId) {
            return $http.delete(serviceBase + "api/accounts/" + accountId.toString());
        };

        return {
            getAccounts: _getAccounts,
            getAccountById: _getAccountById,
            createAccount: _createAccount,
            deleteAccount: _deleteAccount
        };
    }
}())