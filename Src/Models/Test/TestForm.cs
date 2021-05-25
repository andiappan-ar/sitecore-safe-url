using Sitecore.Safe.Security.SafeValidation;

namespace Sitecore.Safe.Models.Test
{
    public class TestForm
    {
        [SafeRegexValidator("AlphaNumeric")]
        public string FirstName { get; set; }

        [SafeCharMatchValidator("scriptInjection")]
        public string LastName { get; set; }
    }
}