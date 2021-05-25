using Sitecore.Mvc.Controllers;
using Sitecore.Safe.Models.Test;
using System.Web.Mvc;

namespace Sitecore.Safe.Controllers
{
    public class SitecoreSafeController : SitecoreController
    {
        public ActionResult Setup()
        {
            return View();
        }

        [HttpPost]
        public bool TestForm(TestForm testForm)
        {
            return (ModelState.IsValid);
        }
    }
}