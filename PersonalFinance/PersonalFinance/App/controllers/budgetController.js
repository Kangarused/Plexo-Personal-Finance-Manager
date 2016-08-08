(function () {
    var app = angular.module('PersonalFinance');

    app.controller('budgetController', [
        '$scope',
        'categoryService',
        budgetController
    ]);

    function budgetController($scope, categoryService) {

        $scope.budgetItemData = {
            type: "",
            category: null,
            description: null,
            amount: null,
            annualFrequency: null
        };

        $scope.incomes = [
            {
                description: "Husband Salary",
                category: "Salary",
                amount: 3500,
                annualFrequency: 12
            },
            {
                description: "Wife Salary",
                category: "Salary",
                amount: 3000,
                annualFrequency: 12
            }
        ];

        $scope.expenses = [
            {
                description: "Mortage",
                category: "Bills",
                amount: 600,
                annualFrequency: 12
            },
            {
                description: "Light Bill",
                category: "Bills",
                amount: 115,
                annualFrequency: 12
            },
            {
                description: "Water Bill",
                category: "Bills",
                amount: 15,
                annualFrequency: 12
            },
            {
                description: "Garbage",
                category: "Bills",
                amount: 45,
                annualFrequency: 4
            }
        ];

        $scope.getCategories = function (val) {
            return categoryService.getCategories(val).then(function (response) {
                var categories = [];
                angular.forEach(response.data, function (item) {
                    categories.push(item);
                });
                return categories;
            });
        };
    }
}())