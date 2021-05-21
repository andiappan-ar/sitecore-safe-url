using System.Web.Mvc;

namespace Sitecore.Law.Controllers
{
    public class LawController : Controller
    {
        public ActionResult Setup()
        {
            return View();
        }

        [HttpPost]
        public bool ValidateRecaptcha()
        {
            return false;
        }
    }
}