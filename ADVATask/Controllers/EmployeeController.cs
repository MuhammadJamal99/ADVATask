using ADVATask.Data;
using ADVATask.Services.DepartmentRepository;
using ADVATask.Services.EmployeeRepository;
using ADVATask.ViewModels.EmployeeVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ADVATask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _Logger;
        private readonly IEmployeeService _EmployeeService;
        private readonly IDepartmentService _DepartmentService;
        public EmployeeController(ILogger<EmployeeController> Logger, IEmployeeService EmployeeService, IDepartmentService DepartmentService)
        {
            _EmployeeService = EmployeeService;
            _Logger = Logger;
            _DepartmentService = DepartmentService;
        }
        public IActionResult _Employee(int EmployeeID)
        {
            EmployeeVM Model = _EmployeeService.GetEmployeeVMByID(EmployeeID);
            if (Model != null)
            {
                Model.Departments = _DepartmentService.GetAllDepartmentsVM().Select(o => new SelectListItem()
                {
                    Value = o.DepartmentID.ToString(),
                    Text = o.Name
                }).ToList();
                return PartialView(Model);
            }
            else
                return PartialView(null);
        }
        public IActionResult _EmployeesList()
        {
            var Model = _EmployeeService.GetAllEmployees();
            if (Model != null && Model.Count() > 0)
                return PartialView(Model);
            else
                return PartialView(null);
        }
        public IActionResult Employees()
        {
            var Model = _DepartmentService.GetAllDepartmentsVM().Select(o => new SelectListItem()
            {
                Value = o.DepartmentID.ToString(),
                Text = o.Name
            }).ToList();
            if (Model != null && Model.Count() > 0)
                return View(Model);
            else
                return View();
        }

        [HttpPost]
        public JsonResult AddEmployee(EmployeeVM NewEmployeeVM)
        {
            if (!string.IsNullOrEmpty(NewEmployeeVM.Name) && NewEmployeeVM.Salary > 0 && NewEmployeeVM.DepartmentID > 0)
            {
                Department EmployeeDepartment = _DepartmentService.GetDepartmentByID(NewEmployeeVM.DepartmentID);
                if(EmployeeDepartment != null)
                {
                    Employee NewEmployee = new()
                    {
                        DepartmentID = NewEmployeeVM.DepartmentID,
                        MangerID = EmployeeDepartment.Employees.FirstOrDefault(o => o.IsManger == true) != null ? EmployeeDepartment.Employees.FirstOrDefault(o => o.IsManger == true).MangerID : 1,
                        Name = NewEmployeeVM.Name,
                        Salary = NewEmployeeVM.Salary,
                    };
                    _EmployeeService.AddEmployee(NewEmployee);
                    return Json(new { success = true, responseText = "Added Successfully!" }, new JsonSerializerSettings());
                }
                else
                    return Json(new { success = false, responseText = "Department Not Found!" }, new JsonSerializerSettings());
            }
            else
                return Json(new { success = false, responseText = "Empty Or Not Valid Data!" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult UpdateEmployee(EmployeeVM UpdatedEmployee)
        {
            if (!string.IsNullOrEmpty(UpdatedEmployee.Name) && UpdatedEmployee.Salary > 0 && UpdatedEmployee.DepartmentID > 0)
            {
                Employee Model = _EmployeeService.GetEmployeeByID(UpdatedEmployee.EmployeeID);
                if (Model != null)
                {
                    Department EmployeeDepartment = _DepartmentService.GetDepartmentByID(UpdatedEmployee.DepartmentID);
                    if(EmployeeDepartment != null)
                    {
                        Model.Name = UpdatedEmployee.Name;
                        Model.Salary = UpdatedEmployee.Salary;
                        Model.DepartmentID = UpdatedEmployee.DepartmentID;
                        Model.MangerID = EmployeeDepartment.Employees.FirstOrDefault(o => o.IsManger == true).MangerID;
                        _EmployeeService.UpdateEmployee(Model);
                        return Json(new { success = true, responseText = "Updated Successfully!" }, new JsonSerializerSettings());
                    }
                    else
                        return Json(new { success = true, responseText = "Department Doesn't Exists!" }, new JsonSerializerSettings());
                }
                else
                    return Json(new { success = false, responseText = "Not Found!" }, new JsonSerializerSettings());
            }
            else
                return Json(new { success = false, responseText = "Empty or Not Vaild Data!" }, new JsonSerializerSettings());
        }
        public JsonResult DeleteEmployee(int EmployeeID)
        {
            if (EmployeeID > 0)
            {
                Employee Model = _EmployeeService.GetEmployeeByID(EmployeeID);
                if (Model != null)
                {
                    if(Model.Employees != null && Model.Employees.Count() > 0)
                        return Json(new { success = false, responseText = "Can't Delete Manger That Have Employees!" }, new JsonSerializerSettings());
                    else
                    {
                        _EmployeeService.DeleteEmployee(Model);
                        return Json(new { success = true, responseText = "Deleted Successfully!" }, new JsonSerializerSettings());
                    }
                    
                }
                else
                    return Json(new { success = false, responseText = "Not Found!" }, new JsonSerializerSettings());
            }
            else
                return Json(new { success = false, responseText = "Empty or Not Vaild Data!" }, new JsonSerializerSettings());
        }
    }
}
