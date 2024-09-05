using Microsoft.EntityFrameworkCore;

namespace Ejer__2
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { 
            
            get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
    }

}
