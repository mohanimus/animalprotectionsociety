﻿using System.ComponentModel.DataAnnotations;

namespace APSData.Entities
{
    public class AppointmentType
    {
        // PK
        public long ID { get; set; }

        // Field(s)
        [MaxLength(50)]
        public string Name { get; set; }
    }
}