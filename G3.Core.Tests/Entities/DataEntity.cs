using System;
using G3.Core.Enums;
using G3.Core.Interfaces;

namespace G3.Core.Tests.Mocks.Entities
{
    public class DataEntity : IEntity
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
    }
}