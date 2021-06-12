using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using System;
using System.Text.RegularExpressions;

namespace Sitecore.Safe.Common.Helper
{
    public static class PatternValidator
    {
        public static bool IsValidString(string source, string patternKey, string patternValue)
        {
            bool result = true;
            try
            {
                switch (patternKey)
                {
                    case "MatchRegex":
                    case "NotMatchRegex":
                        Regex rgx = new Regex(@patternValue,RegexOptions.IgnoreCase);
                        result = (string.Equals("MatchRegex", patternKey))?
                            rgx.Match(source).Success : !rgx.Match(source).Success;
                        break;
                }
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }
    }
}