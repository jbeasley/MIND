﻿using SCM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IVpnAttachmentSetApiValidator : IApiValidator
    {
        Task ValidateNewAsync(VpnAttachmentSetRequestApiModel request);
    }
}
