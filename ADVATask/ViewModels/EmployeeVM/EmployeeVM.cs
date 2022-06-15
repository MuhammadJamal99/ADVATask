using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVATask.ViewModels.EmployeeVM
{
    public class EmployeeVM
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int DepartmentID { get; set; }
        public int MangerID { get; set; }
        public bool IsManger { get; set; }
        public string EmployeeManger { get; set; }
        public string EmployeeDepartment { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set;}
    }
    public class MangerVM
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public IEnumerable<EmployeeVM> Employees { get; set; }
    }
}
