module PersonalFinance.Modules.MessageDisplay {

    //Template @ http://stackoverflow.com/questions/26920055/define-angularjs-directive-using-typescript-and-inject-mechanism

    export class MessagesDirective implements ng.IDirective {
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
         
        constructor(private messageService: IMessageService) {

        }

        link = (scope: IMessageScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, ctrl: any) => {
            scope.errors = this.messageService.errors;
            scope.infos = this.messageService.infos;
            scope.warnings = this.messageService.warnings;

            scope.$on("$stateChangeStart", () => {
                this.messageService.clear();
            });
        }

        static factory(): ng.IDirectiveFactory {
            var directive = (messageService: IMessageService) => new MessagesDirective(messageService);
            directive.$inject = ['messageService'];
            return directive;
        }
    }

    export interface IMessageScope extends ng.IScope {
        errors: string[];
        warnings: string[];
        infos: string[];
        isLoading:Function;
    }

    export interface IMessageService {
        errors: string[];
        warnings: string[];
        infos: string[];

        addError(message: string);
        addWarning(message: string);
        addInfo(message: string);

        addErrors(message: string[]);
        addWarnings(message: string[]);
        addInfos(message: string[]);

        removeErrorAfterSeconds(message: string, seconds: number);
        removeWarningAfterSeconds(message: string, seconds: number);
        removeInfoAfterSeconds(message: string, seconds: number);

        clear();
    }

    export class MessageService implements IMessageService {
        errors: string[] = [];
        warnings: string[] = [];
        infos: string[] = [];

        $inject = ['$timeout'];

        constructor(private $timeout: ng.ITimeoutService) {
            
        }

        scrollTop() {
            $("html, body").animate({ scrollTop: "0px" });
        }

        removeErrorAfterSeconds(message: string, seconds: number) {
            var counter = seconds * 1000;
            this.$timeout(() => {
                var index = this.errors.indexOf(message);
                if (index >= 0) {
                    this.errors.splice(index, 1);
                }
            }, counter);
        }

        removeWarningAfterSeconds(message: string, seconds: number) {
            var counter = seconds * 1000;
            this.$timeout(() => {
                var index = this.warnings.indexOf(message);
                if (index >= 0) {
                    this.warnings.splice(index, 1);
                }
            }, counter);
        }

        removeInfoAfterSeconds(message: string, seconds: number) {
            var counter = seconds * 1000;
            this.$timeout(() => {
                var index = this.infos.indexOf(message);
                if (index >= 0) {
                    this.infos.splice(index, 1);
                }
            }, counter);
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
        .module("message-utils", [])
        .directive("messagesDisplay", PersonalFinance.Modules.MessageDisplay.MessagesDirective.factory())
        .service("messageService", PersonalFinance.Modules.MessageDisplay.MessageService)
        .service("focusService", PersonalFinance.Modules.MessageDisplay.FocusService);
}).call(this);