using APSData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace APSData.Repositories
{
    public class AnimalRepository : IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public IEnumerable<Animal> GetAnimals() {
            return _context.Animals.AsNoTracking().ToList();
        }

        public IEnumerable<Animal> GetAnimals(long speciesID, long locationID) {
            return _context.Animals
                .AsNoTracking()
                .Include(a => a.Breed)
                .Include(a => a.Gender)
                .Include(a => a.Species)
                .Include(a => a.Location)
                .Where(a => a.Deleted == false)
                .Where(a => a.SpeciesID == speciesID)
                .Where(a => a.LocationID == locationID)
                .ToList();
        }

        public Animal Find(long id)
        {
            return _context.Animals
                .AsNoTracking()
                .First(a => a.ID.Equals(id));
        }

        public bool IsMicrochipInUse(long? microchip)
        {
            if (microchip == null)
            {
                return false;
            }
            else
            {
                var _return = _context.Animals
                    .Any(a => a.Microchip == microchip);
                return _return;
            }
        }

        public int Save(Animal animal)
        {
            if (animal.ID.Equals(0))
            {
                _context.Animals.Add(animal);
            }
            else
            {
                _context.Entry(animal).State = EntityState.Modified;
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