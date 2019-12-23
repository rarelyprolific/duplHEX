using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace duplHEX
{
    public class FileLoader
    {
        private const int maximumFileSizeOfTenMegabytesInBytes = 10485760;

        public async Task<byte[]> LoadFile(string pathToFile)
        {
            // TODO: Add in additional logic here to verify the file (does it exist?) and the filesize.
            var fileInformation = new FileInfo(pathToFile);

            if (fileInformation.Length > maximumFileSizeOfTenMegabytesInBytes)
            {
                // TODO: Let's cap at 10MB for now. Do something nicer later!
                throw new ArgumentException("File cannot be over 10MB");
            }

            return await File.ReadAllBytesAsync(pathToFile);
        }
    }
}
