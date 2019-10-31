using System;
using System.Text;
using System.Text.RegularExpressions;

namespace duplHEX
{
    public class HexBuilder
    {
        public string BuildHex(ReadOnlySpan<byte> fileBytes)
        {
            var hex = new StringBuilder();

            int rowCount = (int)fileBytes.Length / 16;
            int byteOffset = 0;
            int rowNumber = 0;

            for (int i = 0; i < rowCount+1; i++)
            {
                // Grabs the next 16 bytes for a row (unless we're on the last row and so we just get the remaining bytes)
                var rowBytes = rowCount != i ? fileBytes.Slice(byteOffset, 16) : fileBytes.Slice(byteOffset);

                hex.AppendLine($"{rowNumber}  {BuildHexRow(rowBytes)}  {BuildAscii(rowBytes)}");

                rowNumber += 20;
                byteOffset += 16;
            }

            return hex.ToString();
        }

        private string BuildHexRow(ReadOnlySpan<byte> rowBytes)
        {
            var row = new StringBuilder();

            foreach (var singleByte in rowBytes)
            {
                row.Append($"{singleByte.ToString("X2")}  ");
            }

            return row.ToString();
        }

        private string BuildAscii(ReadOnlySpan<byte> rowBytes)
        {
            var asciiString = Encoding.ASCII.GetString(rowBytes);
            // TODO: This is wrong! Fix it later! We want to get all the "visible" characters of the ASCII character set.
            return Regex.Replace(asciiString, "[^a-zA-Z0-9! %\";#,]", ".");
        }
    }
}
