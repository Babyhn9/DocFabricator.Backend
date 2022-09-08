namespace ServerDocFabricator.BL.DTO
{
    public class TemplateDto
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; }
        public string? Description { get; set; }
        public List<TemplateFieldDto> Fields { get; set; }
        public TemplateSettingsDto Settings { get; set; }
    }
}
