﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HatServer.DAL
{
    public class PhraseItemRepository : Repository<PhraseItem>
    {
        public PhraseItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<PhraseItem> GetAll()
        {
            return Context.PhraseItem.Include(p => p.Pack);
        }

        public override Task<PhraseItem> GetAsync(int id)
        {
            return Context.PhraseItem.Include(p => p.Pack).SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
