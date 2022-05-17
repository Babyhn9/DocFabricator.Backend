using System;
using System.Text.Json.Serialization;

namespace ServerDocFabricator.DAL
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public bool IsEmpty => Id == Guid.Empty;
    }
}
