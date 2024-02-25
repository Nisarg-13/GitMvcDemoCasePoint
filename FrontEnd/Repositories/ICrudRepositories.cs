using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;

namespace FrontEnd.Repositories
{
    public interface ICrudRepositories
    {
        void Register(EmployeeModel register);
        List<EmployeeModel> GetAllData();
        void DeleteData(int id);
        EmployeeModel ShowData(int id);
        bool UpdateData(EmployeeModel updatedRecord);
        List<EmployeeModel> GetAllDept();
        List<EmployeeModel> GetAllCity();
    }
}