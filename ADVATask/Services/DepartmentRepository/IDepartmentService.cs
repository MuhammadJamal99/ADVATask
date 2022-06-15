using ADVATask.Data;
using ADVATask.ViewModels.DepartmentVM;

namespace ADVATask.Services.DepartmentRepository
{
    public interface IDepartmentService
    {
        Department AddDepartment(Department NewDepartment);
        Department UpdateDepartment(Department UpdatedDepartment);
        Department DeleteDepartment(Department DeletedDepartment);
        Department GetDepartmentByID(int DepartmentID);
        Department GetDepartmentByName(string DepartmentName);
        DepartmentVM GetDepartmentVMByID(int DepartmentID);
        IEnumerable<DepartmentVM> GetAllDepartmentsVM();
    }
}
