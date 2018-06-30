using System.Diagnostics;
using HatServer.Data;
using Microsoft.EntityFrameworkCore;

namespace FillerTests
{
    public static class TestUtilities
    {
        public static DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            var methodName = new StackTrace().GetFrame(1).GetMethod().Name;
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: methodName).Options;
            return dbContextOptions;
        }
    }
}
