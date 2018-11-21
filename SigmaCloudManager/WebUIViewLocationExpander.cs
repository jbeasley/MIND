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
        /// <summary>
        /// Expands the view locations.
        /// </summary>
        /// <returns>The view locations.</returns>
        /// <param name="context">Context.</param>
        /// <param name="viewLocations">View locations.</param>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            viewLocations = viewLocations.Select(s => s.Replace("Views", "WebUI/Views"));
            return viewLocations;
        }

        /// <summary>
        /// Populates the values.
        /// </summary>
        /// <param name="context">Context.</param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
