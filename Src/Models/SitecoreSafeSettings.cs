using System.Collections.Generic;

namespace Sitecore.Safe.Models
{   

    public class SitecoreSafeSettings
    {
        public Configuration Configuration { get; set; }
        public HeaderSettings HeaderSettings { get; set; }
        public UrlThreatSettings UrlThreatSettings { get; set; }
        public ValidationSettings ValidationSettings { get; set; }
    }

    /// <summary>
    /// Configurations
    /// </summary>
    public class Configuration
    {
        public List<Url> UrlList { get; set; }
        public List<Pattern> PatternList { get; set; }
    }

    public class Pattern
    {
        public string key { get; set; }
        public string value { get; set; }
        public string ErrorMessage { get; set; }
        public string MatchingType { get; set; }
    }   

    public class Url
    {
        public string DomainAndPort { get; set; }
    }

    /// <summary>
    /// Url threat settings
    /// </summary>
    /// 
    public class UrlThreat
    {
        public string ThreatDetails { get; set; }
        public string ThreatPageId { get; set; }
        public string MatchingType { get; set; }
        public List<Url> Urls { get; set; }
        public string Url { get; set; }
    }

    public class UrlThreatSettings
    {
        public List<UrlThreat> GroupSettings { get; set; }
        public List<UrlThreat> DomainSpecificSettings { get; set; }
    }


    /// <summary>
    /// Header
    /// </summary>
    public class HeaderDetail
    {
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
        public bool IsAppend { get; set; }
        public List<Url> Urls { get; set; }
        public string Url { get; set; }
    }

    public class HeaderSettings
    {
        public List<HeaderDetail> GroupSettings { get; set; }
        public List<HeaderDetail> DomainSpecificSettings { get; set; }
    }

   
    /// <summary>
    /// Validation attribute settings
    /// </summary>
    public class ValidationSettings
    {
        public List<Pattern> AllSettings { get; set; }       
    }
}



