using Microsoft.EntityFrameworkCore;

namespace ADVATask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(o => o.DepartmentID);
                entity.HasMany(o => o.Employees)
                    .WithOne(o => o.Department)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employee>(entity => {
                entity.HasKey(o => o.EmployeeID);
                entity.HasOne(o => o.Manger)
                    .WithMany(o => o.Employees)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
