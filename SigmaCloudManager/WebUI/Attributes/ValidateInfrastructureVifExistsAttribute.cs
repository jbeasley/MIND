using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Data;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that an infrastructure vif exists in the database
    /// </summary>
    public class ValidateInfrastructureVifExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructureVifExistsAttribute() : 
            base(typeof(ValidateInfrastructureVifExistsActionFilter))
        {
        }

        private class ValidateInfrastructureVifExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructureVifExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue("vifId", out object value) && value is int vifId)
                {

                    if ((from result in await _unitOfWork.VifRepository.GetAsync(q =>
                        q.VifID == vifId
                        && q.VifRole.AttachmentRole.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.ProviderInfrastructure,
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
