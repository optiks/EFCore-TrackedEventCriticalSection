using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCore_TrackedEventCriticalSection
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.Tracked += ChangeTracker_Tracked;
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is Employee employee)
            {
                var devices = employee.Devices;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDevice>()
                .HasOne<Employee>(a => a.Employee)
                .WithMany(p => p.Devices)
                .HasForeignKey(a => a.EmployeeId)
                .IsRequired(true);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmployeeDevice> Devices { get; set; }
    }

    public class EmployeeDevice
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Device { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
