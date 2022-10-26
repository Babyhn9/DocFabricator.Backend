using Microsoft.Extensions.Options;
using ServerDocFabricator.BL;
using ServerDocFabricator.BL.Utils.Attributes;

namespace ServerDocFabricator.Utils;

[Buisness]
public class PathBuilder : IPathBuilder
{
    private RouteSettings _settings;
   
    public PathBuilder(IOptions<RouteSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GetLocalForTemplate(string fileName)
    {
        return Path.Combine(_settings.Template, fileName);
    }

    public string GetFullForTemplate(string localPath)
    {
        var pathToCurrentDirectory = Directory.GetCurrentDirectory();
        return Path.Combine(pathToCurrentDirectory, _settings.Template, localPath);
    }
    
    public string CreateFileName(string postFix = "") =>
        Guid.NewGuid().ToString()
            .Replace("-", "") +
        new Random((int)(DateTime.Now.Ticks % 10000)).Next(1000000) + postFix;
}