(function () {
    var app = angular.module("PersonalFinance");

    app.controller('dashboardController', [
        '$scope',
        'accountService',
        'transactionService',
        dashboardController
    ]);

    function dashboardController($scope, accountService, transactionService) {
        accountService.getAccounts().success(function (data) {
            $scope.accounts = data;
        });

        $scope.message = "";
        $scope.recentTransactions = [];
    }
}())