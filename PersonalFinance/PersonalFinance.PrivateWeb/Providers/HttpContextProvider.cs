﻿using System;
using System.Web;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.Common.Utils.Extensions;
using Newtonsoft.Json;

namespace PersonalFinance.PrivateWeb.Providers
{
    public interface IHttpContextProvider
    {
        InternalApiCallerIdentity GetInternalApiCallerIdentity();
        bool IsInternalApiCall { get; }
        string GetIpAddress();
    }

    [Singleton]
    public class HttpContextProvider : IHttpContextProvider
    {
        private readonly ICryptoProvider _cryptoProvider;
        public HttpContextProvider(ICryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider;
        }

        private HttpContext Context => HttpContext.Current;


        public InternalApiCallerIdentity GetInternalApiCallerIdentity()
        {   
            byte[] callerEncoded = Convert.FromBase64String(Context.Request.Headers[Constants.InternalApiCallerIdentityHeader]);
            var caller = _cryptoProvider.DecodeMessage(callerEncoded);
            return JsonConvert.DeserializeObject<InternalApiCallerIdentity>(caller);
        }

        public bool IsInternalApiCall => Context.Request.Headers["User-Agent"].Contains(Constants.PrivateApiUserAgent);

        public string GetIpAddress()
        {
            if (IsInternalApiCall)
            {
                throw new ArgumentException(
                    "Not available on Private API calls, use the encoded value in the request headers");
            }
            return Context.Request.GetIpAddress();
        }
    }
}