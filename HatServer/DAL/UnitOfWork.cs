using HatServer.Data;
using HatServer.Models;
using System;

namespace HatServer.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private IRepository<Pack> _packRepository;

        public IRepository<Pack> PackRepository
        {
            get
            {

                if (_packRepository == null)
                {
                    _packRepository = new Repository<Pack>(context);
                }
                return _packRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
