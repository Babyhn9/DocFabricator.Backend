using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.DAL.Entities.RefEntities
{
    public class TemplateFieldSettingsEntity : Entity
    {
        public Guid TemplateId { get; set; }
        public bool HasList { get; set; }
    }
}
