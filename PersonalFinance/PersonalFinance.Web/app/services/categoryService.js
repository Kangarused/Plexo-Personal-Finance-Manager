(function () {
    var app = angular.module("PersonalFinance");

    app.factory("categoryService", [
        '$http',
        categoryService
    ]);

    function categoryService($http) {
        var serviceBase = "http://localhost:62733/";

        var _getCategories = function (val) {
            return $http.get(serviceBase + "api/categories", {
                params: {
                    value: val
                }
            });
        }

        return {
            getCategories: _getCategories
        };
    }
}())