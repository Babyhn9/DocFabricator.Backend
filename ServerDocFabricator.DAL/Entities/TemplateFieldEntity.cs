namespace ServerDocFabricator.DAL.Entities
{
    /// <summary>
    /// Сущность 'поля' в бд
    /// </summary>
    public class TemplateFieldEntity : Entity
    {
        /// <summary>
        /// Id Шаблона, которому принадлежит поле
        /// </summary>
        public Guid TemplateID { get; set; }
        /// <summary>
        /// Текст, который был заменён в файле шаблона
        /// </summary>
        public string ReplaceableValue { get; set; }
        /// <summary>
        /// Название поля
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Описание поля
        /// </summary>
        public string Description { get; set; }
        
    }
}
