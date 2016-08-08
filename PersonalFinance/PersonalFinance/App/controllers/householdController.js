(function () {
    var app = angular.module('PersonalFinance');

    app.controller('householdController', [
        '$scope',
        householdController
    ]);

    function householdController($scope) {
        $scope.members = [
            {
                name: "Adam Eury",
                email: "adameury@outlook.com"
            },
            {
                name: "Debora Eury",
                email: "debbie500@gmail.com"
            }
        ]
    }
}())