using CsvProcessorApp.Interfaces;
using CsvProcessorApp.Services;

namespace CsvProcessorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set uping CsvProcessorApp");

            //These are other ways of getting the project root path ... 
            //string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            //string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dirInfo = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent;

            if (dirInfo == null)
                throw new InvalidOperationException("Could not determine project root path.");

            string projectRoot = dirInfo.FullName;
            string inputFilePath = Path.Combine(projectRoot, "Data", "Data.csv");
            Console.WriteLine(inputFilePath);

            IFileReader fileReader = new CsvFileReader();
            INameProcessor nameProcessor = new NameProcessor();
            IAddressProcessor addressProcessor = new AddressProcessor();
            IFileWriter fileWriter = new FileWriter();

            var records = fileReader.ReadFile(inputFilePath).ToList();
            var names = records.SelectMany(r => new[] { r.FirstName, r.LastName }).ToList();
            var nameFrequencies = nameProcessor.GetNameFrequencies(names);

            var addresses = records.Select(r => r.Address).ToList();
            var sortedAddresses = addressProcessor.GetSortedAddresses(addresses);


            fileWriter.WriteLines("name_frequencies.txt", nameFrequencies);
            fileWriter.WriteLines("sorted_addresses.txt", sortedAddresses);

            Console.WriteLine("Processing Complete.");
        }
    }
}
