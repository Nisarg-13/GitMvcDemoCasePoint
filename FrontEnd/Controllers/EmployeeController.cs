using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    // [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly ICrudRepositories _icrudRepositories;

        private readonly IWebHostEnvironment _environment;

        public EmployeeController(ILogger<EmployeeController> logger, ICrudRepositories icrudRepositories,IWebHostEnvironment environment)
        {
            _logger = logger;
            _icrudRepositories = icrudRepositories;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmpInsert()
        {
            List<EmployeeModel> city = _icrudRepositories.GetAllCity();
            ViewBag.Tasks = new SelectList(city, "c_cityid", "c_cityname");
            List<EmployeeModel> dept = _icrudRepositories.GetAllDept();
            ViewBag.Tasks2 = new SelectList(dept, "c_departmentid", "c_deptname");
            return View();
        }
        [HttpPost]
        public IActionResult EmpInsert(EmployeeModel detail,IFormFile? c_photos)
        {     
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(c_photos.FileName);
                var filePath = Path.Combine(_environment.WebRootPath,"photos",filename);
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    c_photos.CopyTo(stream);
                }
                detail.c_photo = filename;
                _icrudRepositories.Register(detail);
                return RedirectToAction("EmpDisplay");
        }
        public IActionResult EmpDisplay()
        {
            var emp = _icrudRepositories.GetAllData();
            return View(emp);
        }
        public IActionResult EmpUpdate(int id)
        {
            List<EmployeeModel> city = _icrudRepositories.GetAllCity();
            ViewBag.Tasks = new SelectList(city, "c_cityid", "c_cityname");
            List<EmployeeModel> dept = _icrudRepositories.GetAllDept();
            ViewBag.Tasks2 = new SelectList(dept, "c_departmentid", "c_deptname");
            var emp = _icrudRepositories.ShowData(id);
            return View("EmpUpdate", emp);
        }
        [HttpPost]
        public IActionResult EmpUpdate(EmployeeModel emp,IFormFile c_photos)
        {
            if(c_photos!= null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(c_photos.FileName);
                var filePath = Path.Combine(_environment.WebRootPath,"photos",filename);
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    c_photos.CopyTo(stream);
                }
                emp.c_photo = filename;
            }
            else
            {
                var exist = _icrudRepositories.ShowData(emp.c_empid);
                emp.c_photo = exist.c_photo;
            }
             _icrudRepositories.UpdateData(emp);
            return RedirectToAction("EmpDisplay");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
             _icrudRepositories.DeleteData(id);
            return RedirectToAction("EmpDisplay");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}