using System;

namespace Mind.WebUI.Models
{
    public static class ModifiableResourceExtensions
    {
        public static string GetConcurrencyToken(this IModifiableResource resource) =>
            "\"" + Convert.ToBase64String(resource.RowVersion) + "\"";

        public static void UpdateConcurrencyToken(this IModifiableResource resource, byte[] token) => 
            resource.RowVersion = token;

    }
}
