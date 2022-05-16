using System;
using System.Text.Json.Serialization;

namespace ServerDocFabricator.DAL
{
    public interface IEntity
    {
        Guid Id { get; }
        [JsonIgnore]
        bool IsEmpty { get; }
        
    }
}