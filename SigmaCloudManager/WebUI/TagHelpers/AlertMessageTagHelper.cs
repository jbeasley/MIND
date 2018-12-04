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
    /// Tag helper for an alert message
    /// </summary>
    [HtmlTargetElement("div")]
    public class MindAlertTagHelper: TagHelper
    {
        private const string MindAlertAttributeName = "mind-alert";

        [HtmlAttributeName(MindAlertAttributeName)]
        public string MindAlert { get; set; }

        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(MindAlert))
            {
                switch (MindAlert)
                {
                    case "info":
                        output.Content.SetHtmlContent("<div class=\"row\">\n" +
                            "<div class=\"col-sm-12\">\n" +
                            "<div class=\"alert alert-info\"" +
                            "role=\"alert\">\n" +
                            "<div class=\"row vertical-align\">\n" +
                            "<div class=\"col-sm-1 text-center\">\n" +
                            "<i class=\"fa fa-info\"></i>\n" +
                            "</div>\n" +
                            "<div class=\"col-sm-11\">\n" +
                            "" + Message + "\n" +
                            "</div>\n" +
                            "</div>\n" +
                            "</div>\n" +
                            "</div>\n" +
                            "</div>");
                        break;
                    case "warning":
                        output.Content.SetHtmlContent("<div class=\"row\">\n" +
                           "<div class=\"col-sm-12\">\n" +
                           "<div class=\"alert alert-warning\"" +
                           "role=\"alert\">\n" +
                           "<div class=\"row vertical-align\">\n" +
                           "<div class=\"col-sm-1 text-center\">\n" +
                           "<i class=\"fa fa-exclamation-triangle\"></i>\n" +
                           "</div>\n" +
                           "<div class=\"col-sm-11\">\n" +
                           "" + Message + "\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>");
                        break;
                    case "danger":
                        output.Content.SetHtmlContent("<div class=\"row\">\n" +
                           "<div class=\"col-sm-12\">\n" +
                           "<div class=\"alert alert-danger\"" +
                           "role=\"alert\">\n" +
                           "<div class=\"row vertical-align\">\n" +
                           "<div class=\"col-sm-1 text-center\">\n" +
                           "<i class=\"fa fa-exclamation-triangle\"></i>\n" +
                           "</div>\n" +
                           "<div class=\"col-sm-11\">\n" +
                           "" + Message + "\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>");
                        break;
                    case "success":
                        output.Content.SetHtmlContent("<div class=\"row\">\n" + 
                           "<div class=\"col-sm-12\">\n" +
                           "<div class=\"alert alert-success\"" +
                           "role=\"alert\">\n" +
                           "<div class=\"row vertical-align\">\n" +
                           "<div class=\"col-sm-1 text-center\">\n" +
                           "<i class=\"fa fa-check\"></i>\n" +
                           "</div>\n" +
                           "<div class=\"col-sm-11\">\n" +
                           "" + Message + "\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>\n" +
                           "</div>");
                        break;
                }
            }
        }
    }
}
