using Sitecore.Safe.Security.SafeValidation;

namespace Sitecore.Safe.Models.Test
{
    public class TestForm
    {
        [SafeRegexValidator("alpha-numeric")]
        public string FirstName { get; set; }

        [SafeRegexValidator("scriptInjection")]
        public string LastName { get; set; }
        public string RecaptchaResponse { get; set; }
    }
}