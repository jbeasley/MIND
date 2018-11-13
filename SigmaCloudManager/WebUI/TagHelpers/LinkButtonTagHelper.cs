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
    /// </summary>
    [HtmlTargetElement("a")]
    public class LinkButtonTagHelper: TagHelper
    {
        private const string GridBtnLinkAttributeName = "mind-grid-btn-link";
        private const string CreateBtnLinkAttributeName = "mind-create-btn-link";

        [HtmlAttributeName(GridBtnLinkAttributeName)]
        public bool GridBtnLink { get; set; }

        [HtmlAttributeName(CreateBtnLinkAttributeName)]
        public bool CreateBtnLink { get; set; }

        public LinkButtonTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (GridBtnLink) output.Attributes.SetAttribute("class", "btn btn-sm btn-secondary");
            if (CreateBtnLink)
            {
                output.Attributes.SetAttribute("class", "btn btn-sm btn-secondary");
                output.PreContent.SetHtmlContent("<i class='fas fa-plus-circle'></i>&nbsp;");
            }
        }
    }
}
