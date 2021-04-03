using MyApp.Db;
using MyApp.Db.DbOperations;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        EmployeeRepository repository = null;
        public HomeController()
        {
            repository = new EmployeeRepository();
        }
        // GET: Home
        [Authorize(Roles ="Admin,Customer")]
        public ActionResult Create()
        {
            return View();
        }
       [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddEmployee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Data successfully added";
                }
            }
            return View();
        }
        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();
            return View(result);
        }
        
        public ActionResult Details(int id)
        {

            var result = repository.GetEmployee(id);
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {

            var employee = repository.GetEmployee(id);
            ViewBag.edited = "Information edited successfully";
            return View(employee);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EmployeeModel emp)
        {
            if(ModelState.IsValid)
            {
                var result = repository.UpdateEmployee(emp.Id, emp);
                return RedirectToAction("GetAllRecords");
            }

            return View();
        }
        public ActionResult Delete(int id)
        {
            repository.DeleteEmployee(id);
            return RedirectToAction("GEtAllRecords");
        }
        
    }
}