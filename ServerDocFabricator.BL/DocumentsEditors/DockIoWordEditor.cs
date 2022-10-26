
using ServerDocFabricator.BL.DocumentEditors;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using ServerDocFabricator.BL.Utils.Attributes;

namespace ServerDocFabricator.BL.DocumentsEditors
{
    public class DockIoWordEditor : IDocumentEditor
    {
        private List<NewDocumentFieldModel> _fields;
        private WordDocument _document;
        
        public void AttachFile(string pathToFile) => AttachFile(File.OpenRead(pathToFile));
        public void AttachFile(Stream document)
        {
            _document?.Close();
            _fields = new List<NewDocumentFieldModel>();
           
            using var memo = new MemoryStream();
            document.CopyTo(memo);
           
            _document = new WordDocument(memo, FormatType.Docx);
        }

        public string CreateField(string value, int skipCount)
        {
            if (_document == null) throw GetException();

            var replacement = CreateReplacement();
            _fields.Add(new NewDocumentFieldModel { From = value, To = replacement, SkipCount = skipCount });
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
        
        public string Save()
        {
            var templatesFolder = "templates";
            var templateFolderPath = Path.Combine(Directory.GetCurrentDirectory(), templatesFolder);
            
            var fileName = CreateFileName(".docx");
            var path = Path.Combine(Directory.GetCurrentDirectory(), templatesFolder, fileName);
            
            if (!Directory.Exists(templateFolderPath))
                Directory.CreateDirectory(templateFolderPath);
            
            if (File.Exists(path))
                File.Delete(path);

            using var file = File.OpenWrite(path);
            _document.Save(file, FormatType.Docx);
            
            return file.Name;
        }

        public string SaveToDisk(string filePath)
        {

            if (File.Exists(filePath))
                File.Delete(filePath);

            using var file = File.OpenWrite(filePath);
            _document.Save(file, FormatType.Docx);
            return file.Name;
        }

        /// <summary>
        /// позволяет сохранить данные по тому же пути.
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            using var memo = new MemoryStream();
            _document.Save(memo, FormatType.Docx);
            _document.Close();
            using var fs = File.Create(filePath);

            memo.Position = 0;
            memo.WriteTo(fs);
        }
        public void SetFieldValue(string fieldId, string fieldValue) => _document.Find(fieldId, false, true).GetAsOneRange().Text = fieldValue;
        
        public string GetText()
        {
            var text = _document.GetText();
            _document.Close();
            return text;
        } 
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
