using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CarDto
    {
        [Required]
        public string CarType { get; set; }
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public string AddedBy { get; set; }
        public DateTime InspectedAt { get; set; }
        public int InspectionInterval { get; set; }
        public string Comments { get; set; }
        public string OwnerName { get; set; }
        public string OwnerTelNr { get; set; } 
    }
}