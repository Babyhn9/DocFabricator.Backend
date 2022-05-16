namespace ServerDocFabricator.BL.Interfaces
{
    public interface IPDFConverter
    {
        Task<MemoryStream> Convert(Stream stream);
    }
}
