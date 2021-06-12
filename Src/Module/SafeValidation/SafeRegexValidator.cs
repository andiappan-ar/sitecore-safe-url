using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Models;
using Sitecore.Safe.Settings;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitecore.Safe.Security.SafeValidation
{
    public class SafeRegexValidator : ValidationAttribute
    {
        private string keyAttribute = string.Empty;

        public SafeRegexValidator(string key)
        {
            keyAttribute = key;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isInvalid = false;
            string isInvalidErr = string.Empty;

            if (value != null)
            {
                string valStr = value.ToString();

                Pattern regexAttr = SitecoreSafeSettingsService.Settings.ValidationSettings.AllSettings
                    .Where(x => string.Equals(x.key, keyAttribute, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                if (regexAttr != null && PatternValidator.IsValidString(valStr, regexAttr.key,regexAttr.value))
                {
                    isInvalid = true;
                    isInvalidErr = regexAttr.ErrorMessage;
                }

            }

            if (!isInvalid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(isInvalidErr);
            }
        }
    }
}