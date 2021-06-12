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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recaptchaClientResponse">Response token recieved in client side.</param>
        /// <param name="secret">Secret key for recaptcha refrered in google dashboad.</param>
        /// <param name="recaptchaValidationUrl">Ex: format https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}</param>
        /// <returns></returns>
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