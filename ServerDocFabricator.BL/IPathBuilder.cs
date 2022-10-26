namespace ServerDocFabricator.BL;

public interface IPathBuilder
{
    string GetFullForTemplate(string fileName);
    string CreateFileName(string postfix = "");
}