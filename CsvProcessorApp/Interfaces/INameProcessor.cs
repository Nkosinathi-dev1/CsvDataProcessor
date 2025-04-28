using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Interfaces
{
    public interface INameProcessor
    {
        List<string> GetNameFrequencies(List<string> names);
    }
}
