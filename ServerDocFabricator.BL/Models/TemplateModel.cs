using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.BL.Models
{
    public class TemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<TemplateFieldModel> Fields { get; set; }
        public TemplateSettingsModel Settings { get; set; }
    }

}
