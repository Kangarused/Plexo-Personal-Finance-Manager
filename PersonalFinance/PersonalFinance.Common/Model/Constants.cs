namespace PersonalFinance.Common.Model
{
    public static class Constants
    {
        public const string AuthClientId = "websiteAuth";

        public const string PrivateApiUserAgent = "PersonalFinancePublicApiClient";
        public const string OriginalExceptionHeader = "PersonalFinancePublicApiClient";
        public const string OriginalExceptionStackHeader = "PersonalFinancePublicApiClient";
        public const string InternalApiCallerIdentityHeader = "InternalApiCallerIdentity";
        public const string AprivateApiMediaType = "personal-finance/binary";
        public const string EmailVerificationRegex =   //Regex for email verification
                                                       @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                                       @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                                       @")+" +
                                                       @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
    }
}