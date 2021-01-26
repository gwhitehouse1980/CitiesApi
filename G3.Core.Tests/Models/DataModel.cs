using System;
using G3.Core.Enums;
using G3.Core.Interfaces;

namespace G3.Core.Tests.Mocks.Models
{
    public class DataModel : IModel
    {
        // Interface
        public Guid? Id { get; set; }
        public EntityStatusEnum EntityStatus { get; set; }
        public string Name { get; set; }
    }
}