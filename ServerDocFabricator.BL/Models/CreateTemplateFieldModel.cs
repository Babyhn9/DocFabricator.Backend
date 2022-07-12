using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.BL.Models
{
    public class CreateTemplateFieldModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int SkipCount { get; set; }
    }
}

