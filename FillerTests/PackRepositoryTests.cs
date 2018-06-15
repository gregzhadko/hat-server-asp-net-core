using System.Linq;
using HatServer.Data;
using HatServer.DAL;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Xunit;

namespace FillerTests
{
    public sealed class PackRepositoryTests
    {
        [Fact]
        public void Insert_WithoutPhrase_Success()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                var list = repo.GetAll();
                var savedPack = list.FirstOrDefault(p => p.Name == pack.Name && p.Description == pack.Description);
                Assert.NotNull(savedPack);
            }
        }
    }
}
