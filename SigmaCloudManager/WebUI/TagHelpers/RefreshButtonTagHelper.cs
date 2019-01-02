using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mind.WebUI.TagHelpers
{
    /// <summary>
    /// Tag helper for a button to add label and image.
    /// The helper looks for an attribute called 'is-refresh-button' in the button markup
    /// </summary>
    [HtmlTargetElement("button", Attributes = "is-refresh-button")]
    public class RefreshButtonTagHelper: TagHelper
    {
        public RefreshButtonTagHelper()
        {
        }

        public string Caption { get; set; } = "Refresh";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("<i class='fas fa-sync-alt'></i>&nbsp;" + Caption);
            output.Attributes.RemoveAll("is-refresh-button");
        }
    }
}
