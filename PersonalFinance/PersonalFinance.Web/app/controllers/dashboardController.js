(function () {
    var app = angular.module("PersonalFinance");

    app.controller('dashboardController', [
        '$scope',
        'accountService',
        'transactionService',
        dashboardController
    ]);

    function dashboardController($scope, accountService, transactionService) {
        $scope.message = "";
        $scope.accounts = accountService.accounts;
        $scope.recentTransactions = [];
    }
}())