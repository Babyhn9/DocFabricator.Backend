using ServerDocFabricator.BL.DocumentEditors;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.DocumentsEditors;

public abstract class BaseWordEditor : IDocumentEditor
{
   
    protected string CreateReplacement()
    {
        var dateTime = DateTime.Now.Ticks.ToString();
        var randomPart = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
        var dateTimePart = dateTime.Substring(dateTime.Length - 8, 7);
        return $"<{randomPart}_{dateTimePart}>";
    }

    public abstract bool OpenTemplate(TemplateEntity entity);
    public abstract void SetFieldValue(TemplateFieldEntity field, string value);
    public abstract void CreateNewField(string replaceText, int skipCounts);
    public abstract void RemoveField(TemplateFieldEntity field);
    public abstract bool Save();
    public abstract bool Save(Stream file, string fullPath);

    public abstract string GetText(TemplateEntity templateEntity);
}