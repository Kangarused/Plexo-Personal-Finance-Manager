module PersonalFinance.Services {
    export interface IBrowserStorageService {
        saveAuth(authData: Models.ILocalAccessToken): void;
        retrieveAuth(): Models.ILocalAccessToken;
        clear(): void;
    } 

    export class BrowserStorageService implements IBrowserStorageService {
        saveAuth(authData: Models.ILocalAccessToken): void {
            var data = JSON.stringify(authData);
            sessionStorage.setItem('finance-auth', data);
        }

        retrieveAuth(): Models.ILocalAccessToken {
            var data = sessionStorage.getItem('finance-auth');
            if (data == null)
                return null;
            var auth = JSON.parse(data);
            return auth;
        }

        clear(): void {
            sessionStorage.removeItem('finance-auth');
        }
    }
}