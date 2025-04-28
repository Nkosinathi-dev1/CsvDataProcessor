using CsvProcessorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Services
{
    public class CsvFileReader : IFileReader
    {
        public IEnumerable<(string FirstName, string LastName, string Address)> ReadFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Skip(1); // Skip header
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    yield return (parts[0].Trim(), parts[1].Trim(), parts[2].Trim());
                }
            }
        }

    }
}
