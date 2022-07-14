using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.BL.Models
{
    public class TemplateFieldModel
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }

    }
}
  