
module PersonalFinance.Factories {

    PublicHttpInterceptor.$inject = ['$q','browserStorageService'];

    export function PublicHttpInterceptor($q: ng.IQService, browserStorageService: Services.IBrowserStorageService) {
        return {
            request: (config) => {
                config.headers = config.headers || {};
                var authData = browserStorageService.retrieveAuth();
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.access_token;
                }
                return config;
            }
        };
    }
}