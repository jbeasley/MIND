using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortStatusViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortStatusViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <IViewComponentResult> InvokeAsync(PortStatusComponentViewModel model)
        {
            await PopulatePortStatusesDropDownList(model.PortStatus);
            return View(model);
        }

        private async Task PopulatePortStatusesDropDownList(PortStatusTypeEnum? selectedPortStatus = null)
        {

            var portStatuses = (from result in await _unitOfWork.PortStatusRepository.GetAsync()
                                select result);

            if (selectedPortStatus != PortStatusTypeEnum.Assigned)
            {

                // Build the list of port status options, but exclude the 'Assigned' option to prevent the user from 
                // manually placing a port in the Assigned state. The Assigned state is automatically set on the port
                // when the port is associated with an Attachment. This should be the only way in which a port becomes
                // 'assigned'.

                portStatuses = portStatuses.Where(portStatus => portStatus.PortStatusType != SCM.Models.PortStatusTypeEnum.Assigned);
            }

            ViewBag.PortStatus = new SelectList(_mapper.Map<List<PortStatusViewModel>>(portStatuses), "Name", "Name", selectedPortStatus);
        }
    }
}
