using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind
{
    /// <summary>
    /// Change the default location of the razor views for the web UI
    /// </summary>
    public class WebUIViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            viewLocations = viewLocations.Select(s => s.Replace("Views", "/WebUI/Views"));
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
