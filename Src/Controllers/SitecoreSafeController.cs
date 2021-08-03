using Sitecore.Mvc.Controllers;
using Sitecore.Safe.Models.Test;
using Sitecore.Safe.Security.Recaptcha;
using Sitecore.Safe.Security.SafeValidation;
using System.Web.Mvc;

namespace Sitecore.Safe.Controllers
{
    public class SitecoreSafeController : SitecoreController
    {
        public ActionResult Setup()
        {
            return View();
        }

        [AntiforgeryTokenValidator]
        [HttpPost]
        public bool TestForm(TestForm testForm)
        {
            bool isMOdelvalid = ModelState.IsValid;
            bool recapValidation = GoogleRecaptchaHandler.ValidateGoogleRecaptchaResponse(testForm.RecaptchaResponse, "{Your secert key}", "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}");
            return (isMOdelvalid && recapValidation);
        }
    }
}
