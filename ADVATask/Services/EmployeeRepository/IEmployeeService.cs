using ADVATask.Data;
using ADVATask.ViewModels.EmployeeVM;

namespace ADVATask.Services.EmployeeRepository
{
    public interface IEmployeeService
    {
        Employee AddEmployee(Employee NewEmployee);
        Employee UpdateEmployee(Employee UpdatedEmployee);
        Employee DeleteEmployee(Employee DeletedEmployee);
        Employee GetEmployeeByID(int EmployeeID);
        EmployeeVM GetEmployeeVMByID(int EmployeeID);
        IEnumerable<EmployeeVM> GetAllEmployees();
    }
}
