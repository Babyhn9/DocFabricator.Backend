using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.DocumentEditors
{
    public interface IDocumentEditor
    {
        bool OpenTemplate(TemplateEntity entity);
        void SetFieldValue(TemplateFieldEntity field, string value);
        void CreateNewField(string replaceText, int skipCounts);
        void RemoveField(TemplateFieldEntity field);
        bool Save();
        bool Save(Stream file, string fullPath);

        string GetText(TemplateEntity templateEntity);
    }
}