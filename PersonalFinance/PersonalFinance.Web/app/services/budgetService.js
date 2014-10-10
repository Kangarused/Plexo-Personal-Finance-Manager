(function () {
    var app = angular.module("budgetService");

    app.factory("budgetService", [
        '$http',
        budgetService
    ]);

    function budgetService($http) {
        var service = {};
        var serviceBase = "http://localhost:62733/";

        service.getBudgetItems = function () {
            return $http.get(serviceBase + "api/budgetItems");
        };

        return service;
    }
}())