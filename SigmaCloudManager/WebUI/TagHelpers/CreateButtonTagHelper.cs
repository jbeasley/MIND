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
    /// The helper looks for an attribute called 'is-create-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-create-button")]
    public class CreateButtonTagHelper: TagHelper
    {
        public CreateButtonTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-plus-circle'></i>&nbsp;Create");
            output.Attributes.RemoveAll("is-create-button");
        }
    }
}
