﻿using System.Threading.Tasks;
using HatServer.Data;
using HatServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HatServer.DAL
{
    public class PackRepository : Repository<Pack>, IPackRepository
    {
        public PackRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Pack> GetByNameAsync(string name)
        {
            return Entities.FirstOrDefaultAsync(p => p.Name == name);
        }

        public override Task<Pack> GetAsync(int id)
        {
            return Context.Packs.Include(p=>p.Phrases).FirstOrDefaultAsync(p => p.Id == id);
        }
        
    }
}
