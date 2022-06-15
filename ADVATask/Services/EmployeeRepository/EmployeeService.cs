using ADVATask.Data;
using ADVATask.ViewModels.EmployeeVM;
using Microsoft.EntityFrameworkCore;

namespace ADVATask.Services.EmployeeRepository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _Context;
        public EmployeeService(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public Employee AddEmployee(Employee NewEmployee)
        {
            _Context.Add(NewEmployee);
            _Context.SaveChanges();
            return NewEmployee;
        }
        public Employee UpdateEmployee(Employee UpdatedEmployee)
        {
            var EmployeeEntity = _Context.Employees.Attach(UpdatedEmployee);
            EmployeeEntity.State = EntityState.Modified;
            _Context.SaveChanges();
            return UpdatedEmployee;
        }
        public Employee DeleteEmployee(Employee DeletedEmployee)
        {
            _Context.Remove(DeletedEmployee);
            _Context.SaveChanges();
            return DeletedEmployee;
        }
        public Employee GetEmployeeByID(int EmployeeID)
        {
            Employee Model = _Context.Employees.Find(EmployeeID);
            if (Model != null)
                return Model;
            else
                return null;
        }
        public EmployeeVM GetEmployeeVMByID(int EmployeeID)
        {
            Employee Item = _Context.Employees.Find(EmployeeID);
            if (Item != null)
            {
                EmployeeVM Model = new() {
                    EmployeeID = Item.EmployeeID,
                    MangerID = Item.MangerID,
                    DepartmentID = Item.DepartmentID,
                    Name = Item.Name,
                    Salary = Item.Salary,
                    EmployeeDepartment = Item.Department.Name,
                    EmployeeManger = Item.Manger.Name
                };
                return Model;
            }
            else
                return null;
        }
        public IEnumerable<EmployeeVM> GetAllEmployees()
        {
            IEnumerable<EmployeeVM> Model = _Context.Employees.AsNoTracking().Select(o => new EmployeeVM() {
                EmployeeID = o.EmployeeID,
                DepartmentID = o.DepartmentID,
                MangerID = o.MangerID,
                Name = o.Name,
                Salary = o.Salary,
                EmployeeDepartment = o.Department.Name,
                EmployeeManger = o.Manger.Name
            }).ToList();

            if (Model.Count() > 0)
                return Model;
            else
                return null;
        }
    }
}
