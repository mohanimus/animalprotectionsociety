using APSData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace APSData.Repositories
{
    public class TimeslotRepository : IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public IEnumerable<Timeslot> GetFreeTimeslots(DateTime date)
        {
            return _context.Timeslots
                .AsNoTracking()
                .Where(t => !_context.Appointments
                    .Where(a => DbFunctions.TruncateTime(a.Date) == DbFunctions.TruncateTime(date))
                    .Where(a => a.Deleted == false)
                    .Select(a => a.TimeslotID)
                    .Contains(t.ID))
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
