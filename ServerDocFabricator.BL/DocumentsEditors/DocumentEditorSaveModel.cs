using ServerDocFabricator.BL.DocumentsEditors;

namespace ServerDocFabricator.BL.DocumentEditors;

public class DocumentEditorSaveModel
{
    public Stream File { get; set; }
    public List<NewDocumentFieldModel> Fields { get; set; }
}