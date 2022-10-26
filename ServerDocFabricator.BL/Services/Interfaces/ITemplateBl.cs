using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerDocFabricator.BL.DTO;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface ITemplateBl
    {
        /// <summary>
        /// Создаёт шаблон
        /// </summary>
        /// <param name="info">Информация о новом шаблоне</param>
        /// <returns>Модель шаблона</returns>
        TemplateEntity CreateTemplate(CreateTemplateDto info);
        
        /// <summary>
        /// Возвращает шаблон по id
        /// </summary>
        /// <param name="templateId">id шаблона</param>
        /// <returns>Модель шаблона</returns>
        TemplateEntity GetTemplate(Guid templateId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <returns>Список шаблонов, созданных пользователем</returns>
        List<DisplayTemplateDto> GetUserTemplates(UserEntity user);
        
        /// <summary>
        /// Возвращает текст шаблона без форматирования
        /// </summary>
        /// <param name="templateId">id шаблона</param>
        /// <returns></returns>
        string GetFlatText(TemplateEntity template);

        /// <summary>
        /// Добавляет шаблону новые поля
        /// </summary>
        /// <param name="template">шаблон</param>
        /// <param name="fields">список новых полей</param>
        void AddFields(TemplateEntity template, List<CreateTemplateFieldDto> fields);
    }

}
