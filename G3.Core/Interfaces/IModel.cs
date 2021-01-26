using System;
using G3.Core.Enums;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// A simple interface defining the common properties you can expect on an Api facing model
    /// </summary>
    public interface IModel
    {
        Guid? Id { get; set; }
        EntityStatusEnum EntityStatus { get; set; }
        string Name { get; set; }
    }
}