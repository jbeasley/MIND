using System;
namespace Mind
{
    /// <summary>
    /// Nova client API settings.
    /// </summary>
    public class NovaClientApiSettings
    {
        /// <summary>
        /// Gets or sets the base path, e.g. http://127.0.0.1:8080/restconf
        /// </summary>
        /// <value>The base path.</value>
        public string BasePath { get; set; }

        /// <summary>
        /// Username for authentication with the restconf server
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }


        /// <summary>
        /// Password for authentication with the restconf server
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
