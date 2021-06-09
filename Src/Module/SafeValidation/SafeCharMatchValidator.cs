using Sitecore.Safe.Models;
using Sitecore.Safe.Settings;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sitecore.Safe.Security.SafeValidation
{
    public class SafeCharMatchValidator : ValidationAttribute
    {
        private string keyAttribute = string.Empty;
        
        public SafeCharMatchValidator(string key)
        {
            keyAttribute = key;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isInvalid = false;
            string isInvalidErr = string.Empty;

            //if (value != null)
            //{
            //    string valStr = value.ToString();

            //    SafeValidationAttribute invalidCharValidator = SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SafeValidationAttributes.CharacterMatch
            //        .Where(x => string.Equals(x.key, keyAttribute, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            //    if (invalidCharValidator != null)
            //    {
            //        foreach (char @char in valStr)
            //        {
            //            if (invalidCharValidator.value.IndexOf(@char) != -1)
            //            {
            //                isInvalid = true;
            //                isInvalidErr = invalidCharValidator.ErrorMessage;
            //                break;
            //            }
            //        }
            //    }               

            //}

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