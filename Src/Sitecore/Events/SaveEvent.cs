using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Safe.Sitecore.Events
{
    public class SaveEvent
    {
        public void StartDialog(object sender, EventArgs args)
        {
            var savedItem = Event.ExtractParameter(args, 0) as Item;

            if (savedItem == null || savedItem.Database.Name.ToLower() != "master")
            {
                return;
            }

            // Start the dialog and pass in an item ID as an argument
            ClientPipelineArgs cpa = new ClientPipelineArgs();
            cpa.Parameters.Add("id", savedItem.ID.ToString());

            // Kick off the processor in the client pipeline
            Context.ClientPage.Start(this, "DialogProcessor", cpa);
        }
    }
}