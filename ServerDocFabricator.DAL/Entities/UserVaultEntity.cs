using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.DAL.Entities
{
    public class UserVaultEntity : Entity
    {
        public Guid UserId { get; set; }
        public string VaultName { get; set; }
    }
}
