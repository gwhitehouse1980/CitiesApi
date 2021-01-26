using System;
using G3.Core.Enums;
using G3.Core.Interfaces;

namespace G3.Modules.Cities.Entities
{
    public class City : IEntity
    {
        // Interface
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public DateTime? DateDeleted { get; set; }
        public EntityStatusEnum EntityStatus { get; set; }
        public string Name { get; set; }

        // Custom
        public string State { get; set; }
        public string CountryCode { get; set; }
        public int? TouristRating { get; set; }
        public DateTime? DateEstablished { get; set; }
        public int? EstimatedPopulation { get; set; }
    }
}