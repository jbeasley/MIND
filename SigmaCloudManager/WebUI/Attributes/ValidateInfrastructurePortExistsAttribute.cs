using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Data;

namespace Mind.WebUI.Attributes
{
    /// <summary>
    /// Validates that an infrastructure port exists in the database
    /// </summary>
    public class ValidateInfrastructurePortExistsAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidateInfrastructurePortExistsAttribute() : 
            base(typeof(ValidateInfrastructurePortExistsActionFilter))
        {
        }

        private class ValidateInfrastructurePortExistsActionFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _unitOfWork;

            public ValidateInfrastructurePortExistsActionFilter(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {          
                var portId = context.ActionArguments["portId"] as int?;
               
                if ((from result in await _unitOfWork.PortRepository.GetAsync(q =>
                        q.ID == portId 
                        && q.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false)
                        select result)
                       .SingleOrDefault() == null)
                {

                    context.Result = new ViewResult { ViewName = "ItemNotFound" };
                    return;
                }

                await next();
            }
        }
    }
}
