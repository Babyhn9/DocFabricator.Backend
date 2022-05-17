using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.BL.Utils.Attributes;

namespace ServerDocFabricator.BL.Realizations
{
    [Buisness]
    public class PDFConverter : IPDFConverter
    {
        
        public async Task<MemoryStream> Convert(Stream stream)
        {
            try
            {
                var guid = Guid.NewGuid();
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "tempfiles");
                var fullName = Path.Combine(savePath, guid + ".pdf");

              
                var memory = new MemoryStream();

                using (var fs = File.OpenRead(fullName))
                {
                    fs.CopyTo(memory);
                }

                File.Delete(fullName);

                return memory;  

            }
            catch
            {
                return new MemoryStream();
            }
            
        }

        
    }
}
