using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Data;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that a provider domain attachment exists in the database
    /// </summary>
    public class ValidateProviderDomainAttachmentExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateProviderDomainAttachmentExistsAttribute() : 
            base(typeof(ValidateProviderDomainAttachmentExistsActionFilter))
        {
        }

        private class ValidateProviderDomainAttachmentExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateProviderDomainAttachmentExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("attachmentId", out object value) && value is int attachmentId)
                {

                    if ((from result in await _unitOfWork.AttachmentRepository.GetAsync(q =>
                        q.AttachmentID == attachmentId
                        && q.AttachmentRole.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false)
                         select result)
                       .SingleOrDefault() != null)
                    {
                        await next();
                    }

                    context.Result = new ViewResult { ViewName = "ItemNotFound" };
                    return;
                }
            }
        }
    }
}
