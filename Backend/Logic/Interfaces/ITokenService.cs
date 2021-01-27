using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Logic.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
