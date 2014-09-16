(function () {
    var app = angular.module("PersonalFinance");

    app.directive("tabs", function () {
        return {
            restrict: 'E',
            transclude: true,
            controller: function ($scope) {
                $scope.panes = [];

                $scope.select = function (pane) {
                    angular.forEach($scope.panes, function (pane) {
                        pane.selected = false;
                    });

                    pane.selected = true;
                }

                this.addPane = function (pane) {
                    if ($scope.panes.length == 0) {
                        $scope.select(pane);
                    }
                    $scope.panes.push(pane);
                }
            },
            template: 'tabs.html'
        };
    });

    app.directive("pane", function () {
        return {
            require: '^tabs',
            restrict: 'E',
            transclude: true,
            link: function (scope, element, attrs, tabsController) {
                tabsController.addPane(scope);
            }
        }
    });

}())