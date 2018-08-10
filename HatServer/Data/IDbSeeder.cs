using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Data
{
    internal interface IDbSeeder<in T> where T : DbContext 
    {
        void Seed(T context);
    }
}