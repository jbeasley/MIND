using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.TagHelpers
{
    /// <summary>
    /// Tag helper for a pill which indicates network status
    /// </summary>
    [HtmlTargetElement("span")]
    public class NetworkSyncStatusPillTagHelper: TagHelper
    {
        private const string NetworkSyncStatusPillAttributeName = "mind-pill-network-status";

        [HtmlAttributeName(NetworkSyncStatusPillAttributeName)]
        public string NetworkStatus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(NetworkStatus))
            {
                var networkStatusEnum = Enum.Parse<NetworkStatusEnum>(NetworkStatus);
                switch (networkStatusEnum)
                {
                    case NetworkStatusEnum.NotStaged:
                        output.Attributes.SetAttribute("class", "badge badge-pill badge-secondary");
                        break;
                    case NetworkStatusEnum.Staged:
                        output.Attributes.SetAttribute("class", "badge badge-pill badge badge-pill badge-primary");
                        break;
                    case NetworkStatusEnum.Active:
                        output.Attributes.SetAttribute("class", "badge badge-pill badge badge-pill badge-success");
                        break;
                    case NetworkStatusEnum.ActivationFailure:
                        output.Attributes.SetAttribute("class", "badge badge-pill badge badge-pill badge-danger");
                        break;
                    case NetworkStatusEnum.StagedInconsistent:
                        output.Attributes.SetAttribute("class", "badge badge-pill badge badge-pill badge-warning");
                        break;
                }
            }
        }
    }
}
