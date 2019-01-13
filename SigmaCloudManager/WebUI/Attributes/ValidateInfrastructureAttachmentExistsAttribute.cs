using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Data;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that an infrastructure attachment exists in the database
    /// </summary>
    public class ValidateInfrastructureAttachmentExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureAttachmentExistsAttribute() : 
            base(typeof(ValidateInfrastructureAttachmentExistsActionFilter))
        {
        }

        private class ValidateInfrastructureAttachmentExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructureAttachmentExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("attachmentId", out object value) && value is int attachmentId)
                {

                    if ((from result in await _unitOfWork.AttachmentRepository.GetAsync(q =>
                        q.AttachmentID == attachmentId
                        && q.AttachmentRole.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.ProviderInfrastructure,
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
