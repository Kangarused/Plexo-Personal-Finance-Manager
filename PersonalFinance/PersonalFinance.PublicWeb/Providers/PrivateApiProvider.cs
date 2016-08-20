using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PersonalFinance.Common.IocAttributes;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.Common.Providers.Logging;
using Newtonsoft.Json;

namespace PersonalFinance.PublicWeb.Providers
{
    public interface IPrivateApiProvider
    {
        Task<T> InvokeGetAsync<T>(string controller, string method, string param = null);
        Task<T> InvokeGetAsync<T>(string controller, string method, int param);
        Task<T> InvokePostAsync<T>(string controller, string method, object request);
    }

    [PerRequest]
    public class PrivateApiProvider : IPrivateApiProvider
    {
        private readonly IVersionProvider _versionProvider;
        private readonly ICryptoProvider _cryptoProvider;
        private readonly ILoggingProvider _loggingProvider;
        private readonly IInternalApiCallerResolverProvider _internalApiCallerResolver;

        private readonly string _privateWebUrl;


        public PrivateApiProvider(
            IConfigurationManagerProvider configurationManagerProvider, 
            IVersionProvider versionProvider,
            ICryptoProvider  cryptoProvider, 
            ILoggingProvider loggingProvider,
            IInternalApiCallerResolverProvider internalApiCallerResolverProvider
            )
        {
            _versionProvider = versionProvider;
            _cryptoProvider = cryptoProvider;
            _loggingProvider = loggingProvider;
            _internalApiCallerResolver = internalApiCallerResolverProvider;

            _privateWebUrl = configurationManagerProvider.GetConfigValue("PrivateWebUrl");
        }


        private string GetApiUrl(string controller, string method, string param = null)
        {
            var output =  $"{_privateWebUrl}/api/{controller}/{method}";
            if (!string.IsNullOrEmpty(param))
            {
                output += $"/{param}";
            }
            return output;
        }


        public async Task<T> InvokeGetAsync<T>(string controller,string method, string param=null)
        {
            Uri uri = new Uri(GetApiUrl(controller, method, param));
            
            HttpResponseMessage response = await DoRequest(HttpMethod.Get, uri);

            byte[] bodyArray = await response.Content.ReadAsByteArrayAsync();

            string responseBody = _cryptoProvider.DecodeMessage(bodyArray);
            
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public Task<T> InvokeGetAsync<T>(string controller, string method, int param)
        {
            return InvokeGetAsync<T>(controller, method, param.ToString());
        }

        public async Task<T> InvokePostAsync<T>(string controller, string method,object request)
        {
            Uri uri = new Uri(GetApiUrl(controller, method));

            HttpResponseMessage response = await DoRequest(HttpMethod.Post, uri,request);
            byte[] bodyArray = await response.Content.ReadAsByteArrayAsync();
            string responseBody = _cryptoProvider.DecodeMessage(bodyArray);

            return JsonConvert.DeserializeObject<T>(responseBody);
        }


        private async Task<HttpResponseMessage> DoRequest(HttpMethod method, Uri uri, object content = null)
        {
            var messageHandler = new HttpClientHandler {UseDefaultCredentials = true};

            var client = new HttpClient(messageHandler);
            
            var request = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = uri
            };

            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(Constants.PrivateApiUserAgent, _versionProvider.BuildVersion));

            var callerId = _internalApiCallerResolver.Caller;
            if (callerId == null)
            {
                throw new PrivateApiException("Caller Id cannot be identified");
            }

            var caller = Convert.ToBase64String(_cryptoProvider.EncodeMessage(JsonConvert.SerializeObject(callerId)));
            request.Headers.Add(Constants.InternalApiCallerIdentityHeader, caller);

            
            if (method == HttpMethod.Put || method == HttpMethod.Post)
            {
                var data = JsonConvert.SerializeObject(content);
                var crypted = _cryptoProvider.EncodeMessage(data);
                ByteArrayContent httpContent = new ByteArrayContent(crypted);
                request.Content = httpContent;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(Constants.AprivateApiMediaType);
            }
            string body="";
            var response = await client.SendAsync(request);
            try
            {
                body = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                string errorMessage="";
                string stackTrace="";

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    if (request.Headers.Contains(Constants.OriginalExceptionHeader) &&
                        request.Headers.Contains(Constants.OriginalExceptionStackHeader))
                    {
                        errorMessage = response.Headers.GetValues(Constants.OriginalExceptionHeader).ElementAt(0);
                        stackTrace = response.Headers.GetValues(Constants.OriginalExceptionStackHeader).ElementAt(0);
                    }
                    else
                    {
                        errorMessage = body;
                    }
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorMessage =
                        "Could not locate service! check the names and signatures of the methods in the controller";
                }

                var message = $"Error on sending request to :'{uri}', status code:{response.StatusCode}, method:{method}, error:{errorMessage}, stack:{stackTrace}";
                _loggingProvider.Logger.Error(message,e);
                var inner = new PrivateApiException(message, e);
                var ex = new PrivateApiException(errorMessage, inner);
                throw ex;
            }
            return response;
        }

        private class PrivateApiException : Exception
        {
            public PrivateApiException(string message) : base(message)
            {
            }
            public PrivateApiException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}