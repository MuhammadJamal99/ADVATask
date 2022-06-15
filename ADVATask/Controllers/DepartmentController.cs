using ADVATask.Data;
using ADVATask.Services.DepartmentRepository;
using ADVATask.ViewModels.DepartmentVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADVATask.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _Logger;
        private readonly IDepartmentService _DepartmentService;
        public DepartmentController(ILogger<DepartmentController> Logger, IDepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
            _Logger = Logger;
        }
        public IActionResult _DepartmentsList()
        {
            var Model = _DepartmentService.GetAllDepartmentsVM();
            if (Model != null && Model.Count() > 0)
                return PartialView(Model);
            else
                return PartialView(null);
        }
        public IActionResult Departments()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddDepartment(DepartmentVM NewDepartmentVM)
        {
            if (!string.IsNullOrEmpty(NewDepartmentVM.Name))
            {
                var Model = _DepartmentService.GetDepartmentByName(NewDepartmentVM.Name);
                if (Model == null)
                {
                    Department NewDepartment = new()
                    {
                        Name = NewDepartmentVM.Name
                    };
                    _DepartmentService.AddDepartment(NewDepartment);
                    return Json(new { success = true, responseText = "Added Successfully!" }, new JsonSerializerSettings());
                }
                else
                    return Json(new { success = false, responseText = "Already Exists!" }, new JsonSerializerSettings());
            }
            else
                return Json(new { success = false, responseText = "Empty Or Not Valid Data!" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult UpdateDepartment(DepartmentVM UpdatedDepartment)
        {
            if (UpdatedDepartment.DepartmentID > 0 && !string.IsNullOrEmpty(UpdatedDepartment.Name))
            {
                Department Model = _DepartmentService.GetDepartmentByID(UpdatedDepartment.DepartmentID);
                if (Model != null)
                {
                    Model.Name = UpdatedDepartment.Name;
                    _DepartmentService.UpdateDepartment(Model);
                    return Json(new { success = true, responseText = "Updated Successfully!" }, new JsonSerializerSettings());
                }
                else
                    return Json(new { success = false, responseText = "Not Found!" }, new JsonSerializerSettings());
            }
            else
                return Json(new { success = false, responseText = "Empty or Not Vaild Data!" }, new JsonSerializerSettings());
        }
        public JsonResult DeleteDepartment(int DepartmentID)
        {
            if(DepartmentID > 0)
            {
                Department Model = _DepartmentService.GetDepartmentByID(DepartmentID);
                if(Model != null)
                {
                    if(Model.Employees != null && Model.Employees.Count() > 0)
                        return Json(new { success = false, responseText = "Can't Delete Department That Have Employees" }, new JsonSerializerSettings());
                    else
                    {
                        _DepartmentService.DeleteDepartment(Model);
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
