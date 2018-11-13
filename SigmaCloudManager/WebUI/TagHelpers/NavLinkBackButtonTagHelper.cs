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
    /// Tag helper for a back navigation button
    /// </summary>
    [HtmlTargetElement("a")]
    public class NavLinkBackButtonTagHelper: TagHelper
    {
        private const string NavLinkBackBtnAttributeName = "mind-nav-link-back-btn";

        [HtmlAttributeName(NavLinkBackBtnAttributeName)]
        public bool NavLinkBackBtn { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (NavLinkBackBtn)
            {
                output.Attributes.SetAttribute("class", "btn btn-sm btn-secondary");
                output.PreContent.SetHtmlContent("<i class='fas fa-backward'></i>&nbsp;");
            }
        }
    }
}
