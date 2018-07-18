using System;
using System.Collections.Generic;
using FillerTests;
using HatServer.Data;
using HatServer.DAL;
using Model.Entities;
using Xunit;

namespace ProdTests
{
    public class ProdPackRepositoryTests
    {
        [Fact]
        public void InsertAsyncTest_WithPhrases_Success()
        {
            var options = TestUtilities.GetDbContextOptions<StatisticsDbContext>();
            (List<Pack> packs, List<PhraseItem>, List<ServerUser>) data = TestUtilities.GeneratePackData();

            using (var context = new StatisticsDbContext(options))
            {
                var repo = new ProdPackRepository(context);
                repo.InsertAsync(new ProdPack(data.Item1[0])).GetAwaiter().GetResult();
            }
        }
    }
}
