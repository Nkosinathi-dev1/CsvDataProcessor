using CsvProcessorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Services
{
    public class NameProcessor : INameProcessor
    {
        public List<string> GetNameFrequencies(List<string> names)
        {
            var frequencyDict = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase);

            foreach (var name in names)
            {
                var parts = name.Split(' ');
                foreach (var part in parts)
                {
                    var cleanPart = part.Trim();
                    if (!string.IsNullOrEmpty(cleanPart))
                    {
                        if (frequencyDict.ContainsKey(cleanPart))
                            frequencyDict[cleanPart]++;
                        else
                            frequencyDict[cleanPart] = 1;
                    }
                }
            }

            var sorted = frequencyDict
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key}, {kvp.Value}")
                .ToList();

            return sorted;
        }
    }
}
