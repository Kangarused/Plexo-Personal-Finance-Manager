var Common;
(function (Common) {
    var Directives;
    (function (Directives) {
        stServerPagination.$inject = ['$timeout'];
        function stServerPagination($timeout) {
            return {
                restrict: "E",
                require: '^stTable',
                scope: {
                    stServerPageSize: '=',
                    stServerPipe: '=',
                    stServerPage: '='
                },
                link: function (scope, element, attrs, ctrl) {
                    var stTableController = ctrl;
                    scope.stServerPage = 1;
                    var pageSize = scope.stServerPageSize || 10;
                    function getFilterParams() {
                        return {
                            page: scope.stServerPage,
                            pageSize: pageSize,
                            orderColumn: stTableController.tableState().sort.predicate,
                            orderDirection: stTableController.tableState().sort.reverse ? "desc" : "asc"
                        };
                    }
                    if (angular.isFunction(scope.stServerPipe)) {
                        var isFirstBind = true;
                        //stTableController.preventPipeOnWatch();
                        stTableController.pipe = function () {
                            //set the pipe here to call our custom function
                            var defered = scope.stServerPipe(getFilterParams());
                            if (defered != null) {
                                defered.then(function (result) {
                                    var pagedResult = result.data;
                                    if (pagedResult.numRecords <= pageSize) {
                                    }
                                    else {
                                        $(element).show();
                                    }
                                    if (!isFirstBind) {
                                        $timeout(function () {
                                            //$('body').animate({ scrollTop: 0 }, 400);
                                            //$('body').scrollTo($("[st-table]"), 300);
                                        });
                                    }
                                    isFirstBind = false;
                                    //todo:scroll to top or remove promises, problem is this is run before it is bound so try with $timeout so apply has been done
                                });
                            }
                        };
                    }
                    scope.$watch(function () { return scope.stServerPage; }, function () {
                        stTableController.pipe();
                    });
                }
            };
        }
        Directives.stServerPagination = stServerPagination;
    })(Directives = Common.Directives || (Common.Directives = {}));
})(Common || (Common = {}));
//# sourceMappingURL=st-server-pagination.js.map