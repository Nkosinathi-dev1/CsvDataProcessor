using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<(string FirstName, string LastName, string Address)> ReadFile(string filePath);

    }
}
