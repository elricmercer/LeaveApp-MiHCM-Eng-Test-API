using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<EmployeeProfile> EmployeeProfile { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocation { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
    }
}
