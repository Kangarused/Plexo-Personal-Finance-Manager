module PersonalFinance.Controllers {
    export class BillsController {
        static $inject = ['$scope', 'messageService', '$validation', 'billDataService', '$uibModal'];

        bills: Models.IBill[];
        newBill: Models.IBill;

        modalError: string = null;

        constructor(
            private $scope: IScope,
            private messageService: Modules.MessageDisplay.IMessageService,
            private $validation,
            private billDataService: Services.IBillDataService,
            private $uibModal
        ) {
            $scope.vm = this;

            this.loadBills();
        }

        loadBills() {
            this.billDataService.getBills().then(
                (response) => {
                    this.bills = response.data;
                });
        }

        addBill(form) {
            this.messageService.clear();
            this.modalError = null;
            this.$validation.validate(form)
                .success(() => {
                    this.billDataService.createBill(this.newBill).then(
                        (response) => {
                            this.messageService.addInfo(response.data.response);
                            this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                            this.loadBills();
                        }, (err) => {
                            var message = "Failed to create budget";
                            this.messageService.addError(message);
                            this.messageService.removeErrorAfterSeconds(message, 4);
                        });
                })
                .error(() => {
                    this.modalError = "Validation issues found, please review your submission";
                });
        }

        deleteBill(bill) {
            var modalInstance = this.$uibModal.open({
                animation: true,
                templateUrl: '/App/views/modals/confirm-modal.html',
                controller: 'confirmModalController',
                resolve: {
                    modalData: () => {
                        return {
                            modalHead: 'Delete Bill',
                            modalBody: 'Are you sure you want to delete this bill?'
                        };
                    }
                }
            });

            modalInstance.result.then(confirm => {
                if (confirm) {
                    this.billDataService.deleteBill(bill.id).then(
                        (response) => {
                            this.messageService.addInfo(response.data.response);
                            this.messageService.removeInfoAfterSeconds(response.data.response, 3);
                            this.loadBills();
                        }, (err) => {
                            var message = "Failed to delete budget";
                            this.messageService.addError(message);
                            this.messageService.removeErrorAfterSeconds(message, 4);
                        });
                }
            });
        }
    }
}