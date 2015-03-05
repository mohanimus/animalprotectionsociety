using System;
using System.ComponentModel.DataAnnotations;

namespace APSData.Entities
{
    public class Animal
    {
        // PK
        public long ID { get; set; }
        
        // FK(s)
        public long SpeciesID { get; set; }
        public long GenderID { get; set; }
        public long BreedID { get; set; }
        public long LocationID { get; set; }

        // Field(s)
        [MaxLength(250)]
        public string Name { get; set; }
        public long? Microchip { get; set; }
        [MaxLength(50)]
        public string Colour { get; set; }
        public DateTime? DateAdopted { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDesexed { get; set; }
        public DateTime? DateFlead { get; set; }
        public DateTime? DateWormed { get; set; }
        public DateTime? DateVaccinated { get; set; }
        public string MedicalRecord { get; set; }
        [MaxLength(250)]
        public string Contact { get; set; }
        public bool Deleted { get; set; }

        // Related entities
        public virtual Species Species { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Breed Breed { get; set; }
        public virtual Location Location { get; set; }
    }
}