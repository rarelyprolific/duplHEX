using System;
using System.Text;
using System.Text.RegularExpressions;

namespace duplHEX
{
    /// <summary>
    /// Builds the hex display view from the raw bytes of a file
    /// </summary>
    public class HexBuilder
    {
        private const char SingleSpace = ' ';

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

                // Calculate the white space padding to append to the end of the hex value to align the ASCII view
                // (Really hacky (but fine for proof of concept).. Tidy this up later when I've got the essentials working.)
                string spacePadding = string.Empty;
                if (rowCount == i)
                {
                    spacePadding = spacePadding.PadRight((16 - (fileBytes.Length - byteOffset)) * 4);
                }

                // Builds a single row to display 16 bytes of the file
                hex.AppendLine($"{rowNumber.ToString().PadRight(10, SingleSpace)}  {BuildHexRow(rowBytes)} {spacePadding} {BuildAscii(rowBytes)}");

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
