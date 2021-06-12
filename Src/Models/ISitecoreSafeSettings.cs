using System.Collections.Generic;

namespace Sitecore.Safe.Models
{

    public interface IUrlKeyCollection
    {
        string Url { get; set; }
    }

    public interface IUrlListKeyCollection
    {
        List<Url> Urls { get; set; }
    }

    public interface IQueryStringThreat
    {
        string ThreatDetails { get; set; }
        string ThreatPageId { get; set; }
        string MatchingType { get; set; }

    }

}
