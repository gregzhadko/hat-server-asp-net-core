using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bogus;
using HatServer.Data;
using HatServer.DAL;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model.Entities;

namespace FillerTests
{
    public static class TestUtilities
    {
        public static DbContextOptions<FillerDbContext> GetDbContextOptions()
        {
            var methodName = new StackTrace().GetFrame(1).GetMethod().Name;
            var dbContextOptions = new DbContextOptionsBuilder<FillerDbContext>()
                .UseInMemoryDatabase(databaseName: methodName).Options;
            return dbContextOptions;
        }

        public static (List<Pack>, List<PhraseItem>, List<ServerUser>) GeneratePackData(
            DbContextOptions<FillerDbContext> options)
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
                    p.Track = new Track();
                    p.Version = 1;
                    p.ReviewStates = GenerateReviewStates(users);
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

            using (var context = new FillerDbContext(options))
            {
                var userRepository = new UserRepository(context);
                userRepository.InsertRangeAsync(users).GetAwaiter().GetResult();

                var repo = new PackRepository(context);
                repo.InsertRangeAsync(packs).GetAwaiter().GetResult();
            }

            return (packs, phrases, users);
        }

        [NotNull]
        private static List<ReviewState> GenerateReviewStates([NotNull] IReadOnlyList<ServerUser> users)
        {
            var number = new Randomizer().Number(0, users.Count);

            var result = new List<ReviewState>();
            for (var i = 0; i < number; i++)
            {
                var i1 = i;
                var reviewState = new Faker<ReviewState>()
                    .Rules((f, r) =>
                    {
                        r.State = f.PickRandom<State>();
                        r.Comment = f.Lorem.Text();
                        r.UserId = users[i1].Id;
                    });
                result.Add(reviewState);
            }

            return result;
        }
    }
}