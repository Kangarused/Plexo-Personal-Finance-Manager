(function () {
    var app = angular.module("PersonalFinance");

    app.controller("signupController", [
        '$scope',
        '$location',
        '$timeout',
        'authService',
        signupController
    ]);

    function signupController($scope, $location, $timeout, authService) {
        $scope.message = '';
        $scope.registrationSuccessful = false;

        $scope.registrationData = {
            userName: "",
            email: "",
            firstName: "",
            lastName: "",
            password: "",
            confirmPassword: ""
        }

        $scope.register = function () {
            authService.register($scope.registrationData).then(function (response) {
                $scope.registrationSuccessful = true;
                $scope.message = "Registration successful! You will be redirected to login in 2 seconds...";
                startTimer(); // count down 2 seconds and redirect to login view.
            },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                $scope.message = "Registration failed due to " + errors.join(' ');
            });

            var startTimer = function () {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $location.path("/login");
                }, 2000)
            };
        };
    }
}())