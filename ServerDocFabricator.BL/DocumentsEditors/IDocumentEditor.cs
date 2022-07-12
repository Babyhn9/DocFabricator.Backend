using ServerDocFabricator.BL.DocumentsEditors;

namespace ServerDocFabricator.BL.DocumentEditors
{
    public interface IDocumentEditor
    {
        /// <summary>
        /// сохраняет файл на диск и возврашает путь сохранёному файлу.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        string SaveToDisk();
        void AttachFile(Stream document);
        void AttachFile(string pathToFile);
        void SetFieldValue(string fieldId, string fieldValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="skipCount"></param>
        /// <returns>Возвращает id поля в документе в формате <someValue> </returns>
        string CreateField(string value, int skipCount);
        /// <summary>
        /// Возвращает новый поток с изменёным документом.
        /// </summary>
        /// <returns></returns>
        DocumentEditorSaveModel GetModifiedFile();
        /// <summary>
        /// Возвращает текст и закрывает документ
        /// </summary>
        /// <returns></returns>
        string GetText();
    }

    public class DocumentEditorSaveModel
    {
        public Stream File { get; set; }
        public List<NewDocumentFieldModel> Fields { get; set; }
    }
}
