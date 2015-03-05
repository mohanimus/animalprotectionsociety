using APSData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace APSData.Repositories
{
    public class AppointmentRepository: IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public IEnumerable<Appointment> GetAppointments(long appointmentTypeID, DateTime date) {
            return _context.Appointments
                .AsNoTracking()
                .Include(a => a.Animal)
                .Include(a => a.Animal.Breed)
                .Include(a => a.Animal.Gender)
                .Include(a => a.Timeslot)
                .Where(a => a.AppointmentTypeID.Equals(appointmentTypeID))
                .Where(a => a.Deleted.Equals(false))
                .Where(a => DbFunctions.TruncateTime(a.Date) == DbFunctions.TruncateTime(date)).ToList();
        }

        public Appointment Find(long id)
        {
            return _context.Appointments
                .AsNoTracking()
                .First(a => a.ID.Equals(id));
        }

        public int Save(Appointment appointment)
        {
            if (appointment.ID.Equals(0))
            {
                _context.Appointments.Add(appointment);
            }
            else
            {
                _context.Entry(appointment).State = EntityState.Modified;
            }
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing && _context != null) {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}