/// <reference path="../../PersonalFinance.Common/Content/scripts/common-angular-settings/angularCommonDefinitions.d.ts" />


declare var personalFinanceSettings: PersonalFinance.Models.IPublicSettings;

declare module PersonalFinance.Models {

    export interface ICurrentUser {
        name: string;
        role: string;
        id: number;
    }

    export interface ILocalAccessToken {
        access_token: string;
        name: string;
        role: string;
        id:number;
    }
}


declare module PersonalFinance.Services {
    export interface ILocalStorageService {
        remove(authorizationdata: string);
        set(authorizationdata: string, p: any);
        get(authorizationData:string):any;
    }
}