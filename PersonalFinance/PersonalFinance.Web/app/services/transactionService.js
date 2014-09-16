(function () {
    'use strict';

    var app = angular.module("PersonalFinance");

    app.factory("transactionService", [
        transactionService
    ]);

    function transactionService() {
        var serviceBase = "http://localhost:62733/";
        var _transactions = [
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            },
            {
                account: "Checking",
                date: new Date().toDateString(),
                description: "New Shoes",
                category: "Clothing",
                amount: 123.45,
                reconciled: 123.45,
                updatedBy: "Adam Eury"
            }
        ];

        var _destroy = function (index) {
            _transactions.splice(index, 1);
        };

        return {
            transactions: _transactions,
            destroy: _destroy
        };
    }
}())