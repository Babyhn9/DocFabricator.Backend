using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.DAL.Entities
{
    internal class VaultValueEntity : Entity
    {
        public Guid VaultId { get; set; }
        public string Value { get; set; }
    }
}
