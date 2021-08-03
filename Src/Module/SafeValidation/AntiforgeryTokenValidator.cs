using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Models;
using Sitecore.Safe.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Sitecore.Safe.Security.SafeValidation
{
    public class AntiforgeryTokenValidator : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {            
            var cookieToken = filterContext.HttpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            string formToken = filterContext.HttpContext.Request.Headers["RequestVerificationToken"];            
            AntiForgery.Validate(cookieToken!=null?cookieToken.Value:null, formToken);            
        }
    }
}