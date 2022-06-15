using System.ComponentModel.DataAnnotations;

namespace ADVATask.Data
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int DepartmentID { get; set; }
        public int MangerID { get; set; }
        public bool IsManger { get; set; }
        public virtual Employee? Manger { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
