using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortConnectorAndSfpViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortConnectorAndSfpViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(PortConnectorAndSfpComponentViewModel model)
        {
            var tasks = new List<Task> {
                PopulatePortSfpsDropDownList(model?.PortSfp),
                PopulatePortConnectorsDropDownList(model?.PortConnector)
            };

            await Task.WhenAll(tasks);

            return View(model);
        }
       
        private async Task PopulatePortConnectorsDropDownList(object selectedPortConnector = null)
        {
            var portConnectors = await _unitOfWork.PortConnectorRepository.GetAsync();
            ViewBag.PortConnector = new SelectList(_mapper.Map<List<PortConnectorViewModel>>(portConnectors), "Name", "Name", selectedPortConnector);
        }

        private async Task PopulatePortSfpsDropDownList(object selectedPortSfp = null)
        {
            var portSfps = await _unitOfWork.PortSfpRepository.GetAsync();
            ViewBag.PortSfp = new SelectList(_mapper.Map<List<PortSfpViewModel>>(portSfps), "Name", "Name", selectedPortSfp);
        }
    }
}
