using Microsoft.EntityFrameworkCore;
using System.Data.SqlServerCe;
using System.Linq;

namespace EFCore_TrackedEventCriticalSection
{
    class Program
    {
        static void Main(string[] args)
        {
            var ceConnectionString = "Data Source=TestDb.sdf; Persist Security Info = False; ";
            var ceConnection = new SqlCeConnection(ceConnectionString);
            ceConnection.Open();

            var options = new DbContextOptionsBuilder<TestDataContext>()
                .UseSqlCe(ceConnection)
                .UseLazyLoadingProxies()
                .Options;

            var context = new TestDataContext(options);
            var employees = context.Set<Employee>().ToList();
        }
    }
}
