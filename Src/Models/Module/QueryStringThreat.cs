using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Safe.Models.Module
{
    public class QueryStringThreat
    {
        public bool IsThreat { get; set; }
        public string ThreatPageId { get; set; }
    }
}