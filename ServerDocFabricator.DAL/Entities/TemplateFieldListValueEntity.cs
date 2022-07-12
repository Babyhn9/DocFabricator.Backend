using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.DAL.Entities
{
    public class TemplateFieldListValueEntity : Entity
    {
        public Guid FieldId { get; set; }
        public sbyte Value { get; set; }    
    }
}
