using ServerDocFabricator.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface ITemplateBl
    {
        TemplateModel CreateTemplate(CreateTemplateModel info);
        TemplateModel GetTemplate(Guid templateId);
        Stream GetTemplateFile(Guid templateId);
        List<TemplateModel> GetUserTemplates(Guid userId);
        string GetFlatText(Guid templateId);
        void InitTemplate(Guid templateId, List<CreateTemplateFieldModel> fields);
    }

}
