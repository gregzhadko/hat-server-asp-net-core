using Microsoft.EntityFrameworkCore;

namespace HatServer.Data
{
    internal interface IDbSeeder<in T> where T : DbContext 
    {
        void Seed(T context);
    }
}