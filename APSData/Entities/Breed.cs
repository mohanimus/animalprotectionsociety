using System.ComponentModel.DataAnnotations;

namespace APSData.Entities
{
    public class Breed
    {
        // PK
        public long ID { get; set; }

        // FK(s)
        public long SpeciesID { get; set; }

        // Field(s)
        [MaxLength(50)]
        public string Name { get; set; }
    }
}