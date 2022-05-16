
using ServerDocFabricator.BL.DocumentEditors;
using ServerDocFabricator.DAL.Models.Fields;
using ServerDocFabricator.Utils.Attributes;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace ServerDocFabricator.BL.DocumentsEditors
{
    [Buisness]
    public class DockIoWordEditor : IDocumentEditor
    {
        private List<NewFieldModel> _fields;
        private WordDocument _document;

        public DockIoWordEditor()
        {

        }
        public void AttachFile(string pathToFile) => AttachFile(File.OpenRead(pathToFile));
        public void AttachFile(Stream document)
        {
            _document?.Close();
            _fields = new List<NewFieldModel>();
            _document = new WordDocument(document, FormatType.Docx);
        }

        public string CreateField(string value, int skipCount)
        {
            if (_document == null) throw GetException();

            var replacement = CreateReplacement();
            _fields.Add(new NewFieldModel { From = value, To = replacement, SkipCount = skipCount });
            var entries = _document.FindAll(value, false, true);
           
            if(skipCount < entries.Length)
                entries[skipCount].GetAsOneRange().Text = replacement;
            
            return replacement;

        }

        public DocumentEditorSaveModel GetModifiedFile()
        {
            var memory = new MemoryStream();
            _document.Save(memory, FormatType.Docx);
            memory.Position = 0;
            return new DocumentEditorSaveModel { File = memory, Fields = _fields };
        }
        
        public string SaveToDisk()
        {
            var path = "templates";
            var fileName = CreateFileName(".docx");
            path = Path.Combine(path, fileName);
            
            if (File.Exists(path))
                File.Delete(path);
            
            using var file = File.OpenWrite(path);
            _document.Save(file, FormatType.Docx);
            return file.Name;
        }

        public void SetFieldValue(string fieldId, string fieldValue) => _document.Find(fieldId, false, true).GetAsOneRange().Text = fieldValue;
        
        private ArgumentException GetException() => throw new InvalidOperationException("Перед использованием, необходимо присоединить файл");

        private string CreateFileName(string postFix = "") =>
            Guid.NewGuid().ToString()
            .Replace("-", "") +
            new Random((int)(DateTime.Now.Ticks % 10000)).Next(1000000) + postFix;
        private string CreateReplacement()
        {
            var dateTime = DateTime.Now.Ticks.ToString();
            var randomPart = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            var dateTimePart = dateTime.Substring(dateTime.Length - 8, 7);
            return $"<{randomPart}_{dateTimePart}>";
        }
    }
}
