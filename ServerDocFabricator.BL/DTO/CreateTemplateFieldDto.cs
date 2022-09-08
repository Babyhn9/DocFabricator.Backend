namespace ServerDocFabricator.BL.DTO
{
    public class CreateTemplateFieldDto
    {
        
        /// <summary>
        /// Имя поля
        /// </summary>
        public string FieldName { get; set; }
        
        /// <summary>
        /// Описание поля
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Текст для замены в файле
        /// </summary>
        public string ReplaceableValue { get; set; }
     
        /// <summary>
        /// Количество повторений ReplaceableValue, которые нужно пропустить, для коректной замены.
        /// Исходный текст: 1 1 1 1.
        /// Желаемый результат: 1 1 {newFields} 1.
        /// Значение SkipCount: 2.
        /// </summary>
        public int SkipCount { get; set; }
    }
}

