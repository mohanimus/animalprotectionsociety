using System;
using System.ComponentModel.DataAnnotations;

namespace APSData.Entities
{
    public class Appointment
    {
        // PK
        public long ID { get; set; }

        // FK(s)
        public long AnimalID { get; set; }
        public long? TimeslotID { get; set; }
        public long AppointmentTypeID { get; set; }

        // Field(s)
        public DateTime? Date { get; set; }
        [MaxLength(250)]
        public string Notes { get; set; }
        public bool Cancelled { get; set; }
        public bool Deleted { get; set; }
        public bool Completed { get; set; }
        
        // Related entities
        public virtual Animal Animal { get; set; }
        public virtual Timeslot Timeslot { get; set; }
        public virtual AppointmentType AppointmentType { get; set; }
    }
}