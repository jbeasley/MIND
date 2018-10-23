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
    /// The helper looks for an attribute called 'is-save-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-save-button")]
    public class SaveButtonTagHelper : FormActionTagHelper
    {
        public SaveButtonTagHelper(IUrlHelperFactory urlHelperFactory)  : base(urlHelperFactory)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-save'></i>&nbsp;Save");
            output.Attributes.RemoveAll("is-save-button");
        }
    }
}
