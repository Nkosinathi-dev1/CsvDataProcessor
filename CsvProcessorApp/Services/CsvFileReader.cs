using CsvProcessorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;


namespace CsvProcessorApp.Services
{
    public class CsvFileReader : IFileReader
    {
        //modified after testing
        public IEnumerable<(string FirstName, string LastName, string Address)> ReadFile(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();

            int skippedLines = 0;
            while (csv.Read())
            {
                string? firstName = null;
                string? lastName = null;
                string? address = null;

                try
                {
                    firstName = csv.GetField("FirstName");
                    lastName = csv.GetField("LastName");
                    address = csv.GetField("Address");
                }
                catch (Exception ex)
                {
                    skippedLines++;
                    Console.WriteLine($"Skipped line {csv.Context.Parser.RawRow}: {ex.Message}");
                    continue;
                }

                if (skippedLines > 0)
                {
                    Console.WriteLine($"Total malformed lines skipped: {skippedLines}");
                }

                if (!string.IsNullOrWhiteSpace(firstName) &&
                    !string.IsNullOrWhiteSpace(lastName) &&
                    !string.IsNullOrWhiteSpace(address))
                {
                    yield return (firstName.Trim(), lastName.Trim(), address.Trim());
                }
            }
        }
    }
}
