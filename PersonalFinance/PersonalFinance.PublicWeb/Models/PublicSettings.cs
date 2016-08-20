using PersonalFinance.Common.Model;
using T4TS;

namespace PersonalFinance.PublicWeb.Models
{
    [TypeScriptInterface]
    public class PublicSettings 
    {
        public string BuildVersion { get; set; }
        public string Environment { get; set; }
        public string AuthClientId { get; set; }
        public string RecaptchaPublicKey { get; set; }
    }
}