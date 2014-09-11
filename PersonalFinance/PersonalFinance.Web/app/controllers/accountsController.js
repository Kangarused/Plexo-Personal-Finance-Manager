(function () {
    var app = angular.module("PersonalFinance");

    app.controller("accountsController", [
        '$scope',
        'accountService',
        accountsController
    ]);

    function accountsController($scope, accountService) {
        $scope.accounts = accountService.accounts;
    }
}())