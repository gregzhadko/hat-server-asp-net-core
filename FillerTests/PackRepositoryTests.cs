using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using HatServer.Data;
using HatServer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
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
                .UseInMemoryDatabase(databaseName: "Insert_WithoutPhrase_Success")
                .Options;

            var pack = new Pack {Name = "Test Pack111", Description = "Test Description", Language = "ru"};

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
                var savedPack = repo.GetAll().FirstOrDefault(p => p.Name == pack.Name && p.Description == pack.Description);
                Assert.NotNull(savedPack);
                Assert.InRange(savedPack.Id, 1, int.MaxValue);
            }
        }


        [Fact]
        public void Insert_WithExistingId_Failed()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Insert_WithExistingId_Failed")
                .Options;

            var pack = new Pack {Id = 1, Name = "Test Pack", Description = "Test Description", Language = "ru"};
            var packDuplicated = new Pack {Id = 1, Name = "Test Pack1", Description = "Test Description1", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                Assert.Throws<InvalidOperationException>(() => repo.InsertAsync(packDuplicated).GetAwaiter().GetResult());
            }

        }
        
        //TODO: Uncomment when we'll have a restriction for the two packs with the same name
//        [Fact]
//        public void Insert_WithExistingName_Failed()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "Insert_WithExistingName_Failed")
//                .Options;
//
//            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
//            var packDuplicated = new Pack {Name = "Test Pack", Description = "Test Description1", Language = "ru"};
//
//            // Run the test against one instance of the context
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repo = new PackRepository(context);
//                repo.InsertAsync(pack).GetAwaiter().GetResult();
//                Assert.Throws<InvalidOperationException>(() => repo.InsertAsync(packDuplicated).GetAwaiter().GetResult());
//            }
//        }

        [Fact]
        public void Insert_WithExistingDescriptionAndLanguage_Success()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Insert_WithExistingDescriptionAndLanguage_Success")
                .Options;

            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
            var packDuplicated = new Pack {Name = "Test Pack1", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                repo.InsertAsync(packDuplicated).GetAwaiter().GetResult();
            }
            
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                var savedPack = repo.GetAll().FirstOrDefault(p => p.Name == pack.Name && p.Description == pack.Description);
                var savedPackDuplicated = repo.GetAll().FirstOrDefault(p => p.Name == packDuplicated.Name && p.Description == packDuplicated.Description);
                Assert.NotNull(savedPack);
                Assert.NotNull(savedPackDuplicated);
                Assert.InRange(savedPack.Id, 1, int.MaxValue);
                Assert.InRange(savedPackDuplicated.Id, 1, int.MaxValue);
            }
        }

        [Fact]
        public void Edit_ExistingPack_Success()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_ExistingPack_Success")
                .Options;

            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
            const string newName = "new name";
            const string newDescription = "new description";
            const string newLanguage = "en";
            
            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                pack.Name = newName;
                pack.Description = newDescription;
                pack.Language = newLanguage;
                repo.UpdateAsync(pack);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);

                var updatedPack = repo.GetAll().Single();
                Assert.Equal(updatedPack.Name, pack.Name);
                Assert.Equal(updatedPack.Language, pack.Language);
                Assert.Equal(updatedPack.Description, pack.Description);
            }
        }
        
        [Fact]
        public void Delete_Existing_Success()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Existing_Success")
                .Options;

            var pack = new Pack {Name = "Test Pack111", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                repo.DeleteAsync(pack).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                Assert.Empty(repo.GetAll());
            }
        }
        
        [Fact]
        public void DeleteById_Existing_Success()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteById_Existing_Success")
                .Options;

            var pack = new Pack {Name = "Test Pack111", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                repo.DeleteAsync(pack.Id).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                var repo = new PackRepository(context);
                Assert.Empty(repo.GetAll());
            }
        }
    }
}    