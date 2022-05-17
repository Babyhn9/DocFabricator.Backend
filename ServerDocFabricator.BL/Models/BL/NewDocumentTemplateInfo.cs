namespace ServerDocFabricator.DAL.Models.BL
{
    public class NewDocumentTemplateInfo
    {

        public Stream File { get; set; }
        public string Name { get; set; }
        public List<NewTemplateFieldModel> Fields { get; set; }

        public NewDocumentTemplateInfo(string name, Stream file,  List<NewTemplateFieldModel> enters)
        {
            File = file;
            Name = name;
            Fields = enters;
        }
    }
}
