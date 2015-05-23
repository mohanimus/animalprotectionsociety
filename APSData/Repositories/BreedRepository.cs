using APSData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace APSData.Repositories
{
    public class BreedRepository : IDisposable
    {
        private APSContext _context = new APSContext();
        private bool _disposed;

        public IEnumerable<Breed> GetBreedsBySpecies(long speciesID, bool includeInactives = false)
        {
            IQueryable<Breed> results = _context.Breeds.AsNoTracking()
                .Where(b => b.SpeciesID.Equals(speciesID))
                .Select(b => new { Breed = b, Weight = b.ID.Equals(22) ? 1 : (b.ID.Equals(81) ? 1 : 0) })
                .OrderBy(b => b.Weight)
                .ThenBy(b => b.Breed.Name)
                .Select(b => b.Breed);

            if (includeInactives)
            {
                return results.ToList();
            }
            else
            {
                return results.Where(b => b.Active == true).ToList();
            }
        }

        public void BulkUpdateActive(List<long> idsToActivate, long speciesID)
        {
            IQueryable<Breed> breeds = _context.Breeds
                .Where(b => b.SpeciesID.Equals(speciesID));

            foreach (Breed breed in breeds)
            {
                breed.Active = idsToActivate.Contains(breed.ID);
            }

            _context.SaveChanges();
        }

        public int Save(Breed breed)
        {
            if (breed.ID.Equals(0))
            {
                _context.Breeds.Add(breed);
            }
            else
            {
                _context.Entry(breed).State = EntityState.Modified;
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
