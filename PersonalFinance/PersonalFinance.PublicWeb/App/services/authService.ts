module PersonalFinance.Services {
    export interface IAuthService {
        authExternalProvider(provider: string, controllerReference): void;
        setAuthData(authData: Models.ILocalAccessToken): void;
        getCurrentUser(): Models.ICurrentUser;
        isUserAuth(): boolean;
        logOut();
    }
     
    export class AuthService implements IAuthService {
        static $inject = ['settings', 'browserStorageService'];

        private userInfo: Models.ICurrentUser = null;

        constructor(
            public settings: Models.IPublicSettings,
            private browserStorageService: Services.IBrowserStorageService
        ) {
            this.initAuth();
        }

        public isUserAuth() {
            return this.userInfo != null;
        }

        initAuth() {
            this.userInfo = null;
            var auth = this.browserStorageService.retrieveAuth();
            if (auth != null) {
                this.setAuthData(auth);
            }
        }

        setAuthData(authData: Models.ILocalAccessToken): void {
            if (authData.access_token && authData.role && authData.name) {
                this.userInfo = {
                    role: authData.role,
                    name: authData.name,
                    id: authData.id
                };

                this.browserStorageService.saveAuth(authData);

            } else {
                throw "Some of the required auth components are not being passed to the auth service";
            }
        }

        authExternalProvider = (provider: string, controllerReference) => {
            var redirectUri = location.protocol + '//' + location.host + '/Authcompleted';

            var providerUrl = "/api/Account/ExternalLogin?provider=" + provider
                + "&response_type=token&controllerReference=" + controllerReference + "&client_id=" + this.settings.authClientId + "&redirect_uri=" + redirectUri;

            window.open(providerUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        }

        logOut() {
            this.browserStorageService.clear();
            this.userInfo = null;
        }

        getCurrentUser(): Models.ICurrentUser {
            return this.userInfo;
        }
    }
}