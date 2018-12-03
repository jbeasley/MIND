using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models
{
    /// <summary>
    /// Network status enumeration
    /// </summary>
    public enum NetworkStatusEnum
    {
        /// <summary>
        /// Not staged enum
        /// </summary>
        NotStaged = 0,

        /// <summary>
        /// Staged emum
        /// </summary>
        Staged = 1,

        /// <summary>
        /// Active enum
        /// </summary>
        Active = 2,

        /// <summary>
        /// Activation failure enum
        /// </summary>
        ActivationFailure = 3
    }
}
