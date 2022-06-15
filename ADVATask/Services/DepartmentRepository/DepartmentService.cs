using ADVATask.Data;
using ADVATask.ViewModels.DepartmentVM;
using Microsoft.EntityFrameworkCore;

namespace ADVATask.Services.DepartmentRepository
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _Context;
        public DepartmentService(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public Department AddDepartment(Department NewDepartment)
        {
            _Context.Add(NewDepartment);
            _Context.SaveChanges();
            return NewDepartment;
        }
        public Department UpdateDepartment(Department UpdatedDepartment)
        {
            var Department = _Context.Departments.Attach(UpdatedDepartment);
            Department.State = EntityState.Modified;
            _Context.SaveChanges();
            return UpdatedDepartment;
        }
        public Department DeleteDepartment(Department DeletedDepartment)
        {
            _Context.Remove(DeletedDepartment);
            _Context.SaveChanges();
            return DeletedDepartment;
        }
        public Department GetDepartmentByID(int DepartmentID)
        {
            Department Model = _Context.Departments.Find(DepartmentID);
            if (Model != null)
                return Model;
            else
                return null;
        }
        public Department GetDepartmentByName(string DepartmentName)
        {
            Department Model = _Context.Departments.Where(o => o.Name.ToLower().Contains(DepartmentName.ToLower())).FirstOrDefault();
            if (Model != null)
                return Model;
            else
                return null;
        }
        public DepartmentVM GetDepartmentVMByID(int DepartmentID)
        {
            Department Item = _Context.Departments.Find(DepartmentID);
            if (Item != null)
            {
                DepartmentVM Model = new() {
                    DepartmentID = Item.DepartmentID,
                    MangerID = Item.Employees.FirstOrDefault(e => e.IsManger == true).EmployeeID,
                    Name = Item.Name,
                    Manger = Item.Employees.FirstOrDefault(e => e.IsManger == true).Name,
                    EmployeesCount = Item.Employees.Count()
                };
                return Model;
            }
            else
                return null;
        }
        
        public IEnumerable<DepartmentVM> GetAllDepartmentsVM()
        {
            IEnumerable<DepartmentVM> Model = _Context.Departments.AsNoTracking().Select(o => new DepartmentVM() {
                DepartmentID = o.DepartmentID,
                Name = o.Name,
                MangerID = o.Employees.FirstOrDefault(e => e.IsManger == true) != null ? o.Employees.FirstOrDefault(e => e.IsManger == true).EmployeeID : 0,
                Manger = o.Employees.FirstOrDefault(e => e.IsManger == true) != null ? o.Employees.FirstOrDefault(e => e.IsManger == true).Name : "",
                EmployeesCount = o.Employees.Count() > 0 ? o.Employees.Count() : 0,
            }).ToList();
            if (Model != null && Model.Count() > 0)
                return Model;
            else
                return null;
        }
    }
}
