using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.BL.Models
{
    public class TemplateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<TemplateFieldDTO> Fields { get; set; }
        public TemplateSettingsDTO Settings { get; set; }
    }
}
