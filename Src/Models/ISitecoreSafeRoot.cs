using System.Collections.Generic;

namespace Sitecore.Safe.Models
{

    public interface IUrlKeyCollection
    {
        string Url { get; set; }
    }

    public interface IUrlListKeyCollection
    {
        List<string> Urls { get; set; }
    }

    public interface IQueryStringThreat
    {
        string ThreatCharacters { get; set; }
        string ThreatPageId { get; set; }
    }

    public interface ISecurityHeader
    {
        List<SitecoreSafeSecurityHeader> Headers { get; set; }
    }
}
