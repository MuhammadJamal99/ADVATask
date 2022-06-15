using System.ComponentModel.DataAnnotations;

namespace ADVATask.Data
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }    
    }
}
