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
    /// The helper looks for an attribute called 'is-add-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-add-button")]
    public class AddButtonTagHelper: TagHelper
    {
        public AddButtonTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-plus-circle'></i>&nbsp;Add");
            output.Attributes.RemoveAll("is-add-button");
        }
    }
}
