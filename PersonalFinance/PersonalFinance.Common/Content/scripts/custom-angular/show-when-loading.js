// a directive that shows or hides an element based on number of pending http requests
angular.module('show.when.loading', [])

.directive('showWhenLoading', ['$http', function ($http) {
    return {
        restrict: 'E',
        template: '<span><i class="fa fa-spin fa-cog"></i></span>',
        link: function (scope, elm, attrs) {

            if (attrs.small != null) {
                scope.height = "17";
            } else {
                scope.height = "30"; 
            }

            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.hide();
                }
            });
        }
    };
}]);