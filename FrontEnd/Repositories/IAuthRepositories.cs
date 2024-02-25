using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Repositories
{
    public interface IAuthRepositories
    {
        void Register(AuthModel register);
        List<AuthModel> GetAllData();
        void DeleteData(int id);
        AuthModel ShowData(int id);
        bool UpdateData(AuthModel updatedRecord);
        List<AuthModel> GetAllDept();
        List<AuthModel> GetAllCity();
    }
}