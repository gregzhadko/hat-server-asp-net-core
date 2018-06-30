using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bogus;
using HatServer.Data;
using HatServer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model.Entities;

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

        public static (List<Pack>, List<PhraseItem>, List<ServerUser>) GeneratePackData(
            DbContextOptions<ApplicationDbContext> options)
        {
            const int packNumber = 10;
            const int phrasesPerPack = 10;
            var users = new Faker<ServerUser>()
                .Rules((f, u) =>
                {
                    u.Id = Guid.NewGuid().ToString();
                    u.Email = f.Person.Email;
                    u.UserName = f.Person.UserName;
                })
                .Generate(new Randomizer().Number(4, 5));

            var phrases = new Faker<PhraseItem>()
                .Rules((f, p) =>
                {
                    p.Id = f.UniqueIndex;
                    p.Complexity = f.Random.Number(1, 5);
                    p.CreatedById = f.PickRandom(users).Id;
                    p.CreatedDate = f.Date.Past();
                    p.Description = f.Lorem.Text();
                    p.Phrase = f.Lorem.Words(f.Random.Number(1, 4)).Join(" ");
                    p.TrackId = f.UniqueIndex;
                    p.Version = 1;
                })
                .Generate(packNumber * phrasesPerPack);

            var phraseIndex = 0;
            var packs = new Faker<Pack>()
                .Rules((f, p) =>
                {
                    p.Id = f.UniqueIndex;
                    p.Name = f.Lorem.Word();
                    p.Description = f.Lorem.Sentence();
                    p.Language = "en";
                    p.Phrases = phrases.GetRange(phraseIndex, phrasesPerPack);
                    phraseIndex += phrasesPerPack;
                })
                .Generate(packNumber);

            using (var context = new ApplicationDbContext(options))
            {
                var userRepository = new UserRepository(context);
                userRepository.InsertRangeAsync(users).GetAwaiter().GetResult();

                var repo = new PackRepository(context);
                repo.InsertRangeAsync(packs).GetAwaiter().GetResult();
            }

            return (packs, phrases, users);
        }
    }
}
