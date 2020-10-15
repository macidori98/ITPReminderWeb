using System;

namespace API.Entities
{
    public class Cars
    {
        public int ID { get; set; }
        public int AddedBy { get; set; }
        public string CarType { get; set; }
        public string PlateNumber { get; set; }
        public DateTime InspectedAt { get; set; }
        public DateTime InspectedUntil { get; set; }
        public int InspectionInterval { get; set; }
        public string Comments { get; set; }
        public string OwnerName { get; set; }
        public string OwnerTelNr { get; set; } 
    }
}