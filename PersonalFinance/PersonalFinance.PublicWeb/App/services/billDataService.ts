module PersonalFinance.Services {
    export interface IBillDataService {
        getBills(): ng.IPromise<Models.IResponseObject<Models.IBill[]>>;
        getBillById(billId: number): ng.IPromise<Models.IResponseObject<Models.IBill>>;
        createBill(bill: Models.IBill): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
        deleteBill(billId: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>>;
    }

    export class BillDataService implements IBillDataService {
        static $inject = ['$http']; 

        constructor(public $http: ng.IHttpService) {

        }

        getBills(): ng.IPromise<Models.IResponseObject<Models.IBill[]>> {
            return this.$http.get("/api/Bill/GetBills");
        }

        getBillById(billId: number): ng.IPromise<Models.IResponseObject<Models.IBill>> {
            return this.$http.get("/api/Bill/GetBillById" + billId);
        }

        createBill(bill: Models.IBill): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Bill/CreateBill", bill);
        }

        deleteBill(billId: number): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<string>>> {
            return this.$http.post("/api/Bill/DeleteBill", billId);
        }
    } 
}