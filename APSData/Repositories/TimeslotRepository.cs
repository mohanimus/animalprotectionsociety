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

        public IEnumerable<Timeslot> GetTimeslots()
        {
            return _context.Timeslots
                .AsNoTracking()
                .ToList()
                .OrderBy(t => Convert.ToDateTime(t.Time));
        }

        public IEnumerable<Timeslot> GetFreeTimeslots(DateTime date)
        {
            return _context.Timeslots
                .AsNoTracking()
                .Where(t => !_context.Appointments
                    .Where(a => DbFunctions.TruncateTime(a.Date) == DbFunctions.TruncateTime(date))
                    .Where(a => a.Deleted == false)
                    .Select(a => a.TimeslotID)
                    .Contains(t.ID))
                .Where(t => t.Active == true)
                .ToList()
                .OrderBy(t => Convert.ToDateTime(t.Time));
        }

        public void BulkUpdateActive(List<long> idsToActivate)
        {
            IQueryable<Timeslot> timeslots = _context.Timeslots;

            foreach (Timeslot timeslot in timeslots)
            {
                timeslot.Active = idsToActivate.Contains(timeslot.ID);
            }

            _context.SaveChanges();
        }

        public int Save(Timeslot timeslot)
        {
            if (timeslot.ID.Equals(0))
            {
                _context.Timeslots.Add(timeslot);
            }
            else
            {
                _context.Entry(timeslot).State = EntityState.Modified;
            }
            return _context.SaveChanges();
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
