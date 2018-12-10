﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models
{
    /// <summary>
    /// Illegal model state exception.
    /// </summary>
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message) : base(message)
        {
        }

        public IllegalStateException(string message,
                            Exception innerException) : base(message, innerException)
        {
        }
    }
}
