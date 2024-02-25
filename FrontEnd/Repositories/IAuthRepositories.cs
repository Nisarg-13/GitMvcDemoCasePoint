using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;

namespace FrontEnd.Repositories
{
    public interface IAuthRepositories
    {
        void Register(AuthModel register);
        AuthModel Login(AuthModel login);
        bool IsEmailExists(string c_email);
    }
}