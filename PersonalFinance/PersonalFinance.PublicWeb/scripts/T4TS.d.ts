﻿
/****************************************************************************
  Generated by T4TS.tt - don't make any changes in this file
****************************************************************************/

declare module PersonalFinance.Models {
    /** Generated from PersonalFinance.PublicWeb.Models.RegisterExternalSettings **/
    export interface IRegisterExternalSettings {
        provider: string;
        externalAccessToken: string;
    }
    /** Generated from PersonalFinance.PublicWeb.Models.PublicSettings **/
    export interface IPublicSettings {
        buildVersion: string;
        environment: string;
        authClientId: string;
        recaptchaPublicKey: string;
    }
    /** Generated from PersonalFinance.PublicWeb.Models.UserAccount **/
    export interface IUserAccount {
        provider: string;
        externalAccessToken: string;
        userName: string;
        name: string;
        email: string;
        password: string;
        confirmPassword: string;
        role: any;
    }
    /** Generated from PersonalFinance.PrivateWeb.Models.PrivateSettings **/
    export interface IPrivateSettings {
        currentUser: PersonalFinance.Models.IUserDetails;
        buildVersion: string;
        environment: string;
    }
    /** Generated from PersonalFinance.PrivateWeb.Models.UserDetails **/
    export interface IUserDetails {
        id: number;
        displayName: string;
        email: string;
        roles: any[];
        isAnonymous: boolean;
        isPrivateApiClient: boolean;
        ipAddress: string;
        isSystemAdmin: boolean;
    }
    /** Generated from PersonalFinance.Common.Dtos.ActionErrorReport **/
    export interface IActionErrorReport {
        error: string;
        description: string;
        errorList: string[];
    }
    /** Generated from PersonalFinance.Common.Dtos.ApplicationAssigneeRequest **/
    export interface IApplicationAssigneeRequest {
        name: string;
        userId: number;
        applicantName: string;
        applicantId: number;
        agentName: string;
        agentId: number;
        applicationId: number;
        status: string;
    }
    /** Generated from PersonalFinance.Common.Dtos.ClientsByAgentRequest **/
    export interface IClientsByAgentRequest extends PersonalFinance.Models.IPagingFilter {
        agentUserId: number;
    }
    /** Generated from PersonalFinance.Common.Dtos.LoginRequest **/
    export interface ILoginRequest {
        userName: string;
        password: string;
    }
    /** Generated from PersonalFinance.Common.Dtos.MessagesByAgentClientRequest **/
    export interface IMessagesByAgentClientRequest extends PersonalFinance.Models.IPagingFilter {
        clientId: number;
    }
    /** Generated from PersonalFinance.Common.Dtos.MessagesByApplicationRequest **/
    export interface IMessagesByApplicationRequest extends PersonalFinance.Models.IPagingFilter {
        userId: number;
        applicationId: number;
    }
    /** Generated from PersonalFinance.Common.Dtos.MessagesByUserRequest **/
    export interface IMessagesByUserRequest extends PersonalFinance.Models.IPagingFilter {
        userId: number;
    }
    /** Generated from PersonalFinance.Common.Dtos.SaveClientRequest **/
    export interface ISaveClientRequest {
        id: number;
        agentUserId: number;
        name: string;
        email: string;
    }
    /** Generated from PersonalFinance.Common.Model.Account **/
    export interface IAccount {
    }
    /** Generated from PersonalFinance.Common.Model.Bill **/
    export interface IBill {
    }
    /** Generated from PersonalFinance.Common.Model.Billing **/
    export interface IBilling {
    }
    /** Generated from PersonalFinance.Common.Model.Budget **/
    export interface IBudget {
    }
    /** Generated from PersonalFinance.Common.Model.BudgetItem **/
    export interface IBudgetItem {
    }
    /** Generated from PersonalFinance.Common.Model.Category **/
    export interface ICategory {
    }
    /** Generated from PersonalFinance.Common.Model.Account **/
    export interface IAccount {
        id: number;
        userId: number;
        householdId: number;
        name: string;
        isReconciled: boolean;
        createdBy: string;
        createdTime: string;
        modifiedBy: string;
        modifiedTime: string;
    }
    /** Generated from PersonalFinance.Common.Model.Bill **/
    export interface IBill {
        id: number;
        billingId: number;
        name: string;
        description: string;
        dueDate: string;
        amount: number;
        annualFrequency: number;
        isPaid: boolean;
    }
    /** Generated from PersonalFinance.Common.Model.Billing **/
    export interface IBilling {
        id: number;
        userId: number;
        householdId: number;
    }
    /** Generated from PersonalFinance.Common.Model.BudgetItem **/
    export interface IBudgetItem {
        id: number;
        budgetId: number;
        type: string;
        name: string;
        description: string;
        amount: number;
        annualFrequency: number;
    }
    /** Generated from PersonalFinance.Common.Model.Budget **/
    export interface IBudget {
        id: number;
        userId: number;
        householdId: number;
        name: string;
        createdBy: string;
        createdTime: string;
        modifiedBy: string;
        modifiedTime: string;
    }
    /** Generated from PersonalFinance.Common.Model.Category **/
    export interface ICategory {
        id: number;
        name: string;
        description: string;
    }
    /** Generated from PersonalFinance.Common.Model.HouseholdInvite **/
    export interface IHouseholdInvite {
        id: number;
        fromUserId: number;
        toUserId: number;
        pending: boolean;
        accepted: boolean;
        dateSent: string;
        dateAccepted: string;
    }
    /** Generated from PersonalFinance.Common.Model.HouseholdMember **/
    export interface IHouseholdMember {
        id: number;
        userId: number;
        householdId: number;
    }
    /** Generated from PersonalFinance.Common.Model.Household **/
    export interface IHousehold {
        id: number;
        name: string;
        address: string;
        phoneNumber: string;
    }
    /** Generated from PersonalFinance.Common.Model.Transaction **/
    export interface ITransaction {
        id: number;
        accountId: number;
        name: string;
        description: string;
        amount: number;
        reconciledAmount: number;
        isReconciled: boolean;
        transactionDate: string;
        createdBy: string;
        createdTime: string;
        modifiedBy: string;
        modifiedTime: string;
    }
    /** Generated from PersonalFinance.Common.Model.UserLogin **/
    export interface IUserLogin {
        id: number;
        userId: number;
        loginProvider: string;
        providerKey: string;
    }
    /** Generated from PersonalFinance.Common.Model.UserRole **/
    export interface IUserRole {
        id: number;
        userId: number;
        role: string;
    }
    /** Generated from PersonalFinance.Common.Model.User **/
    export interface IUser {
        id: number;
        userName: string;
        name: string;
        email: string;
        phoneNumber: string;
        passwordHash: string;
        emailConfirmed: boolean;
        createdBy: string;
        createdTime: string;
        modifiedBy: string;
        modifiedTime: string;
    }
    /** Generated from PersonalFinance.Common.Model.Household **/
    export interface IHousehold {
    }
    /** Generated from PersonalFinance.Common.Model.HouseholdInvite **/
    export interface IHouseholdInvite {
    }
    /** Generated from PersonalFinance.Common.Model.HouseholdMember **/
    export interface IHouseholdMember {
    }
    /** Generated from PersonalFinance.Common.Model.PagingFilter **/
    export interface IPagingFilter {
        disablePaging: boolean;
        page: number;
        pageSize: number;
        totalItems: number;
        orderColumn: string;
        orderDirection: any;
    }
    /** Generated from PersonalFinance.Common.Model.Transaction **/
    export interface ITransaction {
    }
    /** Generated from PersonalFinance.Common.Model.User **/
    export interface IUser {
        userRoles: PersonalFinance.Models.IUserRole[];
        userLogins: PersonalFinance.Models.IUserLogin[];
    }
    /** Generated from PersonalFinance.Common.Model.UserLogin **/
    export interface IUserLogin {
    }
    /** Generated from PersonalFinance.Common.Model.UserRole **/
    export interface IUserRole {
    }
}



