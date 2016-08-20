module PersonalFinance.Modules.ErrorDisplay {

    //using template from
    //http://stackoverflow.com/questions/26920055/define-angularjs-directive-using-typescript-and-inject-mechanism

    export class ErrorsDirective implements ng.IDirective {
        restrict = 'E';
        scope = {};
        template = '' +
        '<div ng-show="errors.length > 0" ng-repeat="error in errors track by $index">' + 
            '<p class="alert alert-danger" style="margin-bottom:10px">' + 
                '{{error}}' + 
            '</p>' + 
        '</div>' + 
        '<div ng-show="infos.length > 0" ng-repeat="info in infos track by $index">' +
            '<p class="alert alert-success" style="margin-bottom:10px">' +
                '{{info}}' +
            '</p>' +
        '</div>' + 
        '<div ng-show="warnings.length > 0" ng-repeat="warning in warnings track by $index">' +
            '<p class="alert alert-warning" style="margin-bottom:10px">' +
                '{{warning}}' +
            '</p>' +
        '</div>';

        constructor(private errorService:IErrorService) {

        }

        link = (scope: IErrorScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, ctrl: any) => {
            scope.errors = this.errorService.errors;
            scope.infos = this.errorService.infos;
            scope.warnings = this.errorService.warnings;

            scope.$on("$stateChangeStart", () => {
                this.errorService.clear();
            });
        }

        static factory(): ng.IDirectiveFactory {
            var directive = (errorService: IErrorService) => new ErrorsDirective(errorService);
            directive.$inject = ['errorService'];
            return directive;
        }
    }

    export interface IErrorScope extends ng.IScope {
        errors: string[];
        warnings: string[];
        infos: string[];
        isLoading:Function;
    }

    export interface IErrorService {
        errors: string[];
        warnings: string[];
        infos: string[];

        addError(message: string);
        addWarning(message: string);
        addInfo(message: string);

        addErrors(message: string[]);
        addWarnings(message: string[]);
        addInfos(message: string[]);

        clear();
    }

    export class ErrorService implements IErrorService {
        errors: string[] = [];
        warnings: string[] = [];
        infos: string[] = [];

        constructor() {
            
        }

        scrollTop() {
            $("html, body").animate({ scrollTop: "0px" });
        }

        addError(message: string) {
            this.errors.push(message);
            this.scrollTop();
        }

        addErrors(messages: string[]) {
            for (var n = 0; n < messages.length; n++) {
                this.addError(messages[n]);
            }
            this.scrollTop();
        }

        addWarning(message: string) {
            this.warnings.push(message);
            this.scrollTop();
        }

        addWarnings(messages: string[]) {
            for (var n = 0; n < messages.length; n++) {
                this.addWarning(messages[n]);
            }
            this.scrollTop();
        }

        addInfo(message: string) {
            this.infos.push(message);
            this.scrollTop();
        }

        addInfos(messages: string[]) {
            for (var n = 0; n < messages.length; n++) {
                this.addInfo(messages[n]);
            }
            this.scrollTop();
        }

        clear() {
            this.errors.length = 0;
            this.warnings.length = 0;
            this.infos.length = 0;
        }
    }

    export interface IFocusService {
        focus(elementId:string):void;
    }


    export class FocusService implements IFocusService {
        static $inject = ['$timeout', '$window'];
        constructor(private $timeout: ng.ITimeoutService, private $window: ng.IWindowService) {

        }
        focus = (elementId: string): void => {
            this.$timeout(() => {
                var element = this.$window.document.getElementById(elementId);
                if (element)
                    element.focus();
            });
        }
    }
}

(function () {
    angular
        .module("error-utils", [])
        .directive("errorsDisplay", PersonalFinance.Modules.ErrorDisplay.ErrorsDirective.factory())
        .service("errorService", PersonalFinance.Modules.ErrorDisplay.ErrorService)
        .service("focusService", PersonalFinance.Modules.ErrorDisplay.FocusService);
}).call(this);