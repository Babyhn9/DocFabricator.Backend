using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerDocFabricator.BL.DTO;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface ITemplateBl
    {
        /// <summary>
        /// Создаёт шаблон
        /// </summary>
        /// <param name="info">Информация о новом шаблоне</param>
        /// <returns>Модель шаблона</returns>
        DisplayTemplateDto CreateTemplate(CreateTemplateDto info);
        
        /// <summary>
        /// Возвращает шаблон по id
        /// </summary>
        /// <param name="templateId">id шаблона</param>
        /// <returns>Модель шаблона</returns>
        TemplateDto GetTemplate(Guid templateId);
        
        /// <summary>
        /// Возвращает файл шаблона по id
        /// </summary>
        /// <param name="templateId">id - шаблона</param>
        /// <returns>Стрим Файла</returns>
        Stream GetTemplateFile(Guid templateId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <returns>Список шаблонов, созданных пользователем</returns>
        List<DisplayTemplateDto> GetUserTemplates(Guid userId);
        
        /// <summary>
        /// Возвращает текст шаблона без форматирования
        /// </summary>
        /// <param name="templateId">id шаблона</param>
        /// <returns></returns>
        string GetFlatText(Guid templateId);

        /// <summary>
        /// Добавляет шаблону новые поля
        /// </summary>
        /// <param name="templateId">id - шаблона</param>
        /// <param name="fields">список новых полей</param>
        void AddFields(Guid templateId, List<CreateTemplateFieldDto> fields);
    }

}
