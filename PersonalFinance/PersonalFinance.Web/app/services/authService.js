(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.factory("authService", [
        '$http',
        '$q',
        'localStorageService',
        authService
    ]);

    function authService ($http, $q, localStorageService) {
        var serviceBase = "http://localhost:62733/";

        var _authentication = {
            isAuth: false,
            userName: ''
        };

        var _register = function (registrationData) {
            _logout();

            return $http.post(serviceBase + 'api/useraccount/register', registrationData).then(function (response) {
                return response;
            });
        };

        var _login = function (loginData) {
            var data = 'grant_type=password&username=' + loginData.userName + '&password=' + loginData.password;

            var deferred = $q.defer(); 

            $http.post(serviceBase + 'token', data, {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            })
            .success(function (response) {
                // store OAuth Bearer token in Local Storage
                localStorageService.set('authorizationData', {
                    token: response.access_token,
                    userName: loginData.userName
                });

                // update _authentication info
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;

                deferred.resolve(response); // notify promise of successful completion
            })
            .error(function (error, status) {
                _logout();
                deferred.reject(error); // notify promise of failure
            });

            return deferred.promise;
        };

        var _logout = function () {
            // remove token from local storage
            localStorageService.remove('authorizationData');

            // reset _authentication info
            _authentication.isAuth = false;
            _authentication.userName = '';
        };

        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
            }
        };

        return {
            authentication: _authentication,
            register: _register,
            login: _login,
            logout: _logout,
            fillAuthData: _fillAuthData
        };
    }
}());