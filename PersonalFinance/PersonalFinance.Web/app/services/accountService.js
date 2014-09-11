(function () {
    var app = angular.module("PersonalFinance");

    app.factory("accountService", [
        accountService
    ]);

    function accountService() {
        var serviceBase = "http://localhost:62733/";

        var _accounts = [
            {
                name: "Checking",
                balance: -10,
                reconciled: 23
            },
            {
                name: "Savings",
                balance: 5000,
                reconciled: 4967
            }
        ];

        var _addAccount = function (accountName) {
            _accounts.push({
                name: accountName,
                balance: 0,
                reconciled: 0
            });
        };

        var _removeAccount = function (accountName) {
            var index = -1;

            for (var i = 0; i < _accounts.length; i++) {
                if (_accounts[i].name == accountName) {
                    index = i;
                }
            }

            if (index == -1) {
               console.log("account doesn't exist!")
            }

            _accounts.splice(index, 1);
            console.log("account delete!");
        };


        return {
            accounts: _accounts,
            addAccount: _addAccount,
            removeAccount: _removeAccount
        };
    }
}())