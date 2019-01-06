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
    public class AttachmentBandwidthViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentBandwidthViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(AttachmentBandwidthComponentViewModel model)
        {
            var query = (from result in await _unitOfWork.AttachmentBandwidthRepository.GetAsync()
                         select result);

            query = model.BundleRequired
                ? query.Where(x => x.SupportedByBundle)
                : model.MultiportRequired
                    ? query.Where(x => x.SupportedByMultiPort)
                    : query.Where(x => !x.MustBeBundleOrMultiPort);

            ViewBag.AttachmentBandwidth = new SelectList(_mapper.Map<List<AttachmentBandwidthViewModel>>(query.ToList().OrderBy(b => b.BandwidthGbps)),
                "BandwidthGbps", "BandwidthGbps", model.AttachmentBandwidthGbps);

            return View(model: null as AttachmentBandwidthViewModel);
        }
    }
}
