using APSData.Entities;
using System;
using System.Linq;

namespace APSData.Repositories
{
    public class SpeciesRepository : IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public Species Find(long id)
        {
            return _context.Species
                .First(s => s.ID.Equals(id));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose();
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