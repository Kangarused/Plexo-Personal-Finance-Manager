module PersonalFinance.Services {

    export interface IUserAccountService {
        register(userAccount: Models.IUserAccount): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<Models.ILocalAccessToken>>>;
        getTokenExternalProvider(settings: Models.IRegisterExternalSettings): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<Models.ILocalAccessToken>>>;
        getLocalToken(username: string, password: string): ng.IPromise<Models.IResponseObject<Models.ILocalAccessToken>>;
    } 

    export class UserAccountService implements IUserAccountService {
        static $inject = ['$http'];

        constructor(public $http: ng.IHttpService) {

        }

        register(userAccount: Models.IUserAccount): ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<Models.ILocalAccessToken>>> {
            return this.$http.post("/api/UserAccount/Register", userAccount);
        }

        getTokenExternalProvider(registerExternalSettings: Models.IRegisterExternalSettings):
            ng.IPromise<Models.IResponseObject<Models.IActionResponseGeneric<Models.ILocalAccessToken>>> {

            return this.$http.post("/api/UserAccount/ObtainLocalAccessToken", registerExternalSettings);
        }

        getLocalToken(username: string, password: string): ng.IPromise<Models.IResponseObject<Models.ILocalAccessToken>> {
            var data = "grant_type=password&username=" + username + "&password=" + password;
            return this.$http.post('/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
        }
    }
}