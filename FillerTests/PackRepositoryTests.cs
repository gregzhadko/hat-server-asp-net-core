using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using HatServer.Data;
using HatServer.DAL;
using Model.Entities;
using NUnit.Framework;
using KellermanSoftware.CompareNetObjects;

namespace FillerTests
{
    [TestFixture]
    public sealed class PackRepositoryTests
    {
        [Test]
        public void Insert_WithoutPhrase_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            var pack = new Pack {Name = "Test Pack111", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                var savedPack = repo.GetAll().FirstOrDefault(p => p.Name == pack.Name && p.Description == pack.Description);
                Assert.NotNull(savedPack);
                Assert.GreaterOrEqual(savedPack.Id, 1);
            }
        }

        [Test]
        public void Insert_WithExistingId_Failed()
        {
            var options = TestUtilities.GetDbContextOptions();

            var pack = new Pack {Id = 1, Name = "Test Pack", Description = "Test Description", Language = "ru"};
            var packDuplicated = new Pack {Id = 1, Name = "Test Pack1", Description = "Test Description1", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new FillerDbContext(options))
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
//            var options = TestUtilities.GetDbContextOptions();
//
//            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
//            var packDuplicated = new Pack {Name = "Test Pack", Description = "Test Description1", Language = "ru"};
//
//            // Run the test against one instance of the context
//            using (var context = new FillerDbContext(options))
//            {
//                var repo = new PackRepository(context);
//                repo.InsertAsync(pack).GetAwaiter().GetResult();
//                Assert.Throws<InvalidOperationException>(() => repo.InsertAsync(packDuplicated).GetAwaiter().GetResult());
//            }
//        }

        [Test]
        public void Insert_WithExistingDescriptionAndLanguage_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
            var packDuplicated = new Pack {Name = "Test Pack1", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                repo.InsertAsync(packDuplicated).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                var savedPack = repo.GetAll().FirstOrDefault(p => p.Name == pack.Name && p.Description == pack.Description);
                var savedPackDuplicated = repo.GetAll().FirstOrDefault(p => p.Name == packDuplicated.Name && p.Description == packDuplicated.Description);
                Assert.NotNull(savedPack);
                Assert.NotNull(savedPackDuplicated);
                Assert.GreaterOrEqual(savedPack.Id, 1);
                Assert.GreaterOrEqual(savedPackDuplicated.Id, 1);
            }
        }

        [Test]
        public void Edit_ExistingPack_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            var pack = new Pack {Name = "Test Pack", Description = "Test Description", Language = "ru"};
            const string newName = "new name";
            const string newDescription = "new description";
            const string newLanguage = "en";

            // Run the test against one instance of the context
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                pack.Name = newName;
                pack.Description = newDescription;
                pack.Language = newLanguage;
                repo.UpdateAsync(pack);
            }

            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);

                var updatedPack = repo.GetAll().Single();
                Assert.AreEqual(updatedPack.Name, pack.Name);
                Assert.AreEqual(updatedPack.Language, pack.Language);
                Assert.AreEqual(updatedPack.Description, pack.Description);
            }
        }

        [Test]
        public void Delete_Existing_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            var pack = new Pack {Name = "Test Pack111", Description = "Test Description", Language = "ru"};

            // Run the test against one instance of the context
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                repo.InsertAsync(pack).GetAwaiter().GetResult();
                repo.DeleteAsync(pack).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                Assert.IsEmpty(repo.GetAll());
            }
        }

        [Test]
        public void DeleteById_Existing_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            (List<Pack> packs, List<PhraseItem>, List<ServerUser>) data = TestUtilities.GeneratePackData(options);

            var pack = new Faker().PickRandom(data.packs);
            using (var context = new FillerDbContext(options))
            {
                new PackRepository(context).DeleteAsync(pack.Id).GetAwaiter().GetResult();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                var actual = repo.GetAsync(pack.Id).GetAwaiter().GetResult();
                Assert.Null(actual);
            }
        }

        [Test]
        public void GetPack_ByName_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            (List<Pack> packs, List<PhraseItem>, List<ServerUser>) data = TestUtilities.GeneratePackData(options);

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var pack = new Faker().PickRandom(data.packs);
                var repo = new PackRepository(context);
                var savedPack = repo.GetByNameAsync(pack.Name).GetAwaiter().GetResult();
                Assert.AreEqual(pack.Id, savedPack.Id);
                var randomPack = repo.GetByNameAsync("random name").GetAwaiter().GetResult();
                Assert.Null(randomPack);
            }
        }

        [Test]
        public void GetPack_ById_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            (List<Pack> packs, List<PhraseItem>, List<ServerUser>) data = TestUtilities.GeneratePackData(options);

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new FillerDbContext(options))
            {
                var pack = new Faker().PickRandom(data.packs);
                var repo = new PackRepository(context);
                var savedPack = repo.GetAsync(pack.Id).GetAwaiter().GetResult();
                Assert.AreEqual(pack.Id, savedPack.Id);
                var randomPack = repo.GetAsync(int.MaxValue - 1).GetAwaiter().GetResult();
                Assert.Null(randomPack);
            }
        }

        [Test]
        public void GetFullInfoAsync_Existing_Success()
        {
            var options = TestUtilities.GetDbContextOptions();
            (List<Pack> packs, List<PhraseItem>, List<ServerUser>) data = TestUtilities.GeneratePackData(options);

            using (var context = new FillerDbContext(options))
            {
                var repo = new PackRepository(context);
                var actualPacks = data.packs.Select(p => p.Id)
                    .Select(id => repo.GetFullInfoAsync(id).GetAwaiter().GetResult()).ToList();

                var compareLogic = new CompareLogic();
                compareLogic.Config.MembersToIgnore.Add("User");
                compareLogic.Config.MembersToIgnore.Add("CreatedBy");

                var result = compareLogic.Compare(data.packs, actualPacks);
                Assert.True(result.AreEqual, result.DifferencesString);
            }
        }
    }
}