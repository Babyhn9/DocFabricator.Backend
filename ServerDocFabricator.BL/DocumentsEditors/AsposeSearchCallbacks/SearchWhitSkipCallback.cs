using Aspose.Words.Replacing;

namespace ServerDocFabricator.BL.DocumentsEditors.AsposeSearchCallbacks;

public class SearchWhitSkipCallback : IReplacingCallback
{
    private int _needSkipCount;
    private int _skipCount;
    public SearchWhitSkipCallback(int needSkipCount)
    {
        _needSkipCount = needSkipCount;
    }
    public ReplaceAction Replacing(ReplacingArgs args)
    {
        if (_skipCount == _needSkipCount)
            return ReplaceAction.Replace;

        _skipCount++;
        return ReplaceAction.Skip;
    }
}