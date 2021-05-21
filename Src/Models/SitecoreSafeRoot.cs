using System;
using System.Collections.Generic;

namespace Sitecore.Safe.Models
{
    public class SitecoreSafeRoot
    {
        public Product Product { get; set; }
        public SitecoreSafeUrl SitecoreSafeUrl { get; set; }
    }

    public partial class Product
    {
        public string SitecoreVersion { get; set; }
    }

    public partial class SitecoreSafeUrl
    {
        public Configurations Configurations { get; set; }
        public Modules Modules { get; set; }
    }

    public partial class Configurations
    {
    }

    public partial class Modules
    {
        public QueryString QueryString { get; set; }
        public SecurityHeader SecurityHeader { get; set; }
        public List<InvalidCharValidator> InvalidValidator { get; set; }
        public List<Recaptcha> Recaptcha { get; set; }
    }

    public partial class QueryString
    {
        public QueryStringCommon Common { get; set; }
        public List<QueryStringAllSite> AllSites { get; set; }
    }

    public partial class QueryStringAllSite : UrlKeyCollection
    {
        public string Url { get; set; }
        public string ThreatCharacters { get; set; }
        public string ThreatPageId { get; set; }
    }

    public partial class QueryStringCommon
    {
        public List<string> Urls { get; set; }
        public string ThreatCharacters { get; set; }
        public string ThreatPageId { get; set; }
    }  

    public partial class Recaptcha
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ClientKey { get; set; }
        public string SecretKey { get; set; }
    }

    public partial class SecurityHeader
    {
        public SecurityHeaderCommon Common { get; set; }
        public List<SecurityHeaderAllSite> AllSites { get; set; }
    }

    public partial class SecurityHeaderAllSite : UrlKeyCollection
    {
        public string Url { get; set; }
        public List<SitecoreSafeSecurityHeader> Headers { get; set; }
    }

    public partial class SitecoreSafeSecurityHeader
    {
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
        public bool IsOverWrite { get; set; }
        public bool IsAppend { get; set; }
    }

    public partial class SecurityHeaderCommon
    {
        public List<string> Urls { get; set; }
        public List<SitecoreSafeSecurityHeader> Headers { get; set; }
    }

    public partial class InvalidCharValidator
    {
        public string key { get; set; }
        public string value { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface UrlKeyCollection
    {
        string Url { get; set; }
    }
}



