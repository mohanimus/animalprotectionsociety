﻿using APSData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APSData.Repositories
{
    public class LocationRepository : IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations
                .AsNoTracking()
                .ToList();
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
