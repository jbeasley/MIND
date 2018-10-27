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
    /// Extends form action tag helper for a button to add label and image.
    /// The helper looks for an attribute called 'is-clear-alerts-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-clear-alerts-button")]
    public class ClearAlertsButtonTagHelper : FormActionTagHelper
    {
        public ClearAlertsButtonTagHelper(IUrlHelperFactory urlHelperFactory)  : base(urlHelperFactory)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-minus-circle'></i>&nbsp;Clear Alerts");
            output.Attributes.RemoveAll("is-clear-alerts-button");
        }
    }
}
