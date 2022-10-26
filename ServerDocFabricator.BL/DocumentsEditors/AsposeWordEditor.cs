using Aspose.Words;
using Aspose.Words.Replacing;
using ColoredLive.DAL;
using Microsoft.EntityFrameworkCore;
using ServerDocFabricator.BL.DocumentEditors;
using ServerDocFabricator.BL.DocumentsEditors.AsposeSearchCallbacks;
using ServerDocFabricator.BL.Utils.Attributes;
using ServerDocFabricator.DAL.Entities;
namespace ServerDocFabricator.BL.DocumentsEditors;

[Buisness]
public class AsposeWordEditor : BaseWordEditor
{
    private readonly AppDbContext _context;
    private readonly IPathBuilder _pathBuilder;
    private TemplateEntity _currentTemplate = null;
    private Document _document = null;
    public AsposeWordEditor(AppDbContext context, IPathBuilder pathBuilder)
    {
        _context = context;
        _pathBuilder = pathBuilder;
    }

    public override bool OpenTemplate(TemplateEntity entity)
    {
        var fullPath = _pathBuilder.GetFullForTemplate(entity.TemplateName);
        _document = new Document(fullPath);
        _currentTemplate = entity;
        return true;
    }

    public override void SetFieldValue(TemplateFieldEntity field, string value)
    {
        throw new NotImplementedException();
    }

    public override void CreateNewField(string replaceText, int skipCounts)
    {
        var replacement = CreateReplacement();
        var newTemplateField = new TemplateFieldEntity
        {
            TemplateID = _currentTemplate.Id,
            ReplaceableValue = replacement,
        };
    }

    public override void RemoveField(TemplateFieldEntity field)
    {
        var fieldToRemove = _currentTemplate.Fields.Find(el => el.Id == field.Id);
        if (field != null || !fieldToRemove.IsEmpty)
        {
            _currentTemplate.Fields.Remove(fieldToRemove);
            _context.Entry(_currentTemplate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public override bool Save()
    {
        var full = _pathBuilder.GetFullForTemplate(_currentTemplate.FileName);
        File.Delete(full);
        _document.Save(full, SaveFormat.Docx);
        return true;
    }

    public override bool Save(Stream file, string fullPath)
    {
        if (File.Exists(fullPath)) return false;
        
        var document = new Document(file);
        using var fileStream = File.Create(fullPath);
        document.Save(fileStream, SaveFormat.Docx);
        return true;
    }

    public override string GetText(TemplateEntity templateEntity)
    {
        var full = _pathBuilder.GetFullForTemplate(templateEntity.FileName);
        var doc = new Document(full);
        return doc.Range.Text;
    }
}

 // public override string Save()
    // {
    //     var templatesFolder = "templates";
    //     var templateFolderPath = Path.Combine(Directory.GetCurrentDirectory(), templatesFolder);
    //         
    //     var fileName = CreateFileName(".docx");
    //     var path = Path.Combine(Directory.GetCurrentDirectory(), templatesFolder, fileName);
    //         
    //     if (!Directory.Exists(templateFolderPath))
    //         Directory.CreateDirectory(templateFolderPath);
    //         
    //     if (File.Exists(path))
    //         File.Delete(path);
    //
    //     _document.Save(path);
    //         
    //     return path;
    // }
    //
    // public override void AttachFile(Stream document)
    // {
    //     _document = new Document(document);
    // }
    //
    // public override void AttachFile(string pathToFile)
    // {
    //     try
    //     {
    //         var memo = new MemoryStream();
    //         var fs = File.OpenRead(pathToFile);
    //         fs.CopyTo(memo);
    //         AttachFile(memo);
    //         fs.Close();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
    //
    // public override void SetFieldValue(string fieldId, string fieldValue)
    // {
    //     _document.Range.Replace(fieldId, fieldValue);
    // }
    //
    // public override string CreateField(string value, int skipCount)
    // {
    //     var skipOption = new FindReplaceOptions { ReplacingCallback = new SearchWhitSkipCallback(skipCount) };
    //     var replacement = CreateReplacement();
    //     _fields.Add(new NewDocumentFieldModel { From = value, To = replacement, SkipCount = skipCount });
    //     _document.Range.Replace("value", replacement, skipOption);
    //     return replacement;
    // }
    //
    // public override DocumentEditorSaveModel GetModifiedFile()
    // {
    //     var memory = new MemoryStream();
    //     _document.Save(memory, SaveFormat.Docx);
    //     memory.Position = 0;
    //     return new DocumentEditorSaveModel { File = memory, Fields = _fields };
    // }
    //
    // public override string GetText()
    // {
    //     var result = "";
    //     var textsByParagraphs = new List<string>();
    //
    //     foreach (var p in  _document.FirstSection.Body.Paragraphs)
    //     {
    //         textsByParagraphs.Add(p.Range.Text);
    //     }
    //
    //     return string.Join('\n', textsByParagraphs);
    // }