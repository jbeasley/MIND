using AutoMapper;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public abstract class BaseService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public INetworkSyncService NetSync { get; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, INetworkSyncService netsync)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            NetSync = netsync;
        }
    }
}
