using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    public class BaseFactory
    {
        internal IMapper Mapper { get; set; }

        public BaseFactory(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
