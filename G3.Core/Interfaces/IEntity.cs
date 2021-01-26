using System;
using G3.Core.Enums;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// A simple interface defining the common data properties for basic auditing of changes to an entity object
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
        
        Guid CreatedByUserId { get; set; }
        
        DateTime DateCreated { get; set; }
        
        Guid? UpdatedByUserId { get; set; }
        
        DateTime? DateUpdated { get; set; }
        
        Guid? DeletedByUserId { get; set; }
        
        DateTime? DateDeleted { get; set; }
        
        EntityStatusEnum EntityStatus { get; set; } 
        
        string Name { get; set; }
    }
}