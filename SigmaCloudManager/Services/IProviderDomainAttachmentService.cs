using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using System.Linq.Expressions;

namespace SCM.Services
{
    public interface IProviderDomainAttachmentService : IBaseAttachmentService
    {
        Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request);
    }
}
