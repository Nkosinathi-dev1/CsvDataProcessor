using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Interfaces
{
    public interface IFileWriter
    {
        void WriteLines(string filePath, IEnumerable<string> lines);
    }
}
