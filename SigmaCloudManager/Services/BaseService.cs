using AutoMapper;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public abstract class BaseService: IBaseService
    {
        protected internal IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
