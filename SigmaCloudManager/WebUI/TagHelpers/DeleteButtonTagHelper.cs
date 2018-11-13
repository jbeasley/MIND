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
    /// Tag helper for a button to add label and image.
    /// The helper looks for an attribute called 'is-delete-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-delete-button")]
    public class DeleteButtonTagHelper : TagHelper
    {
        public DeleteButtonTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-minus-circle'></i>&nbsp;Delete");
            output.Attributes.RemoveAll("is-delete-button");
        }
    }
}
