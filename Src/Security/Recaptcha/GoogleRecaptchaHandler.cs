using Newtonsoft.Json;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Sitecore.Safe.Security.Recaptcha
{
    public static class GoogleRecaptchaHandler
    {
        public static bool ValidateGoogleRecaptchaResponse(string recaptchaClientResponse,string secret,string recaptchaValidationUrl)
        {
            bool result = false;

            try
            {
                var client = new WebClient();
                var jsonResult = client.DownloadString(string.Format(recaptchaValidationUrl, secret, recaptchaClientResponse));
                var serverCaptchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
                result = serverCaptchaResponse.Success;
            }
            catch(Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }

       
    }
}