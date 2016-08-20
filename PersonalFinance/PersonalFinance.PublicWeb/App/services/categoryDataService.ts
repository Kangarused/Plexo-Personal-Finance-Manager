module PersonalFinance.Services {

    export interface ICategoryDataService {
        getCategories(): ng.IPromise<Models.IResponseObject<Models.ICategory[]>>;
    }
     
    export class CategoryDataService implements ICategoryDataService {
        static $inject = ['$http'];

        constructor(public $http: ng.IHttpService) {
            
        }

        getCategories(): ng.IPromise<PersonalFinance.Models.IResponseObject<PersonalFinance.Models.ICategory[]>> {
            return this.$http.get("/api/Category/GetCategories/");
        }
    }
}