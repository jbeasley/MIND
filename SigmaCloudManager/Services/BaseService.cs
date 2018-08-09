using AutoMapper;
using SCM.Data;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public abstract class BaseService: IBaseService
    {
        protected internal IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IValidator Validator { get; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public BaseService(IUnitOfWork unitOfWork, IValidator validator)
        {
            UnitOfWork = unitOfWork;
            Validator = validator;
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IValidator validator)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Validator = validator;
        }
    }
}
