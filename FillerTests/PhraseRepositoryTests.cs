using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using HatServer.Data;
using HatServer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model.Entities;
using Xunit;

namespace FillerTests
{
    public class PhraseRepositoryTests
    {
        [Fact]
        public void GetAll_Success()
        {
            var options = TestUtilities.GetDbContextOptions();

            (List<Pack> packs, List<PhraseItem> phrases, List<ServerUser> users) data =
                TestUtilities.GeneratePackData(options);
        }
    }
}