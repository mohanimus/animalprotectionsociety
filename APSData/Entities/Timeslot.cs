using System.ComponentModel.DataAnnotations;

namespace APSData.Entities
{
    public class Timeslot
    {
        // PK
        public long ID { get; set; }

        // Field(s)
        [MaxLength(50)]
        public string Time { get; set; }
    }
}