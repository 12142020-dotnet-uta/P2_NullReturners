using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicClass
    {
        public LogicClass() { }
        private readonly Repo _repo;
        private readonly Mapper _mapper;
        public LogicClass(Repo repo, Mapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    }
}
