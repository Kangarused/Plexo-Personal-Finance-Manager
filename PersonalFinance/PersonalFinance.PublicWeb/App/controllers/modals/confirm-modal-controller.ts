module PersonalFinance.Controllers {
    export class ConfirmModalController {
        static $inject = ['$scope', '$uibModalInstance', 'modalData'];

        constructor(private $scope, private $modalInstance, private modalData) {
            $scope.vm = this;
        }

        confirm() {
            this.$modalInstance.close(true);
        }

        cancel() {
            this.$modalInstance.dismiss('cancel');
        }
    }
}