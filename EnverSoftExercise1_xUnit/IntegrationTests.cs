using CsvProcessorApp.Interfaces;
using CsvProcessorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnverSoftExercise1_xUnit
{
    public class IntegrationTests
    {
        [Fact]
        public void FullProcessing_ShouldGenerateCorrectOutputs()
        {
            // Arrange
            var csvLines = new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,12 Acton St,12345678",
                "James,Brown,22 Jones Rd,87654321",
                "Clive,Brown,10 Apple Rd,12344321"
            };
            var tempCsv = Path.GetTempFileName();
            File.WriteAllLines(tempCsv, csvLines);

            IFileReader fileReader = new CsvFileReader();
            INameProcessor nameProcessor = new NameProcessor();
            IAddressProcessor addressProcessor = new AddressProcessor();

            // Act
            var records = fileReader.ReadFile(tempCsv).ToList();
            var names = records.SelectMany(r => new[] { r.FirstName, r.LastName }).ToList();
            var nameFrequencies = nameProcessor.GetNameFrequencies(names);

            var addresses = records.Select(r => r.Address).ToList();
            var sortedAddresses = addressProcessor.GetSortedAddresses(addresses);

            // Assert
            Assert.Contains("Clive, 2", nameFrequencies);
            Assert.Contains("Brown, 2", nameFrequencies);

            // Correct sorting by street name
            Assert.Equal("12 Acton St", sortedAddresses[0]);
            Assert.Equal("10 Apple Rd", sortedAddresses[1]);
            Assert.Equal("22 Jones Rd", sortedAddresses[2]);

            File.Delete(tempCsv);
        }

        [Fact]
        public void FullProcessing_ShouldHandleEmptyCsvGracefully()
        {
            var csvLines = new[]
            {
                "FirstName,LastName,Address,PhoneNumber"
            };
            var tempCsv = Path.GetTempFileName();
            File.WriteAllLines(tempCsv, csvLines);

            IFileReader fileReader = new CsvFileReader();
            INameProcessor nameProcessor = new NameProcessor();
            IAddressProcessor addressProcessor = new AddressProcessor();

            var records = fileReader.ReadFile(tempCsv).ToList();
            var names = records.SelectMany(r => new[] { r.FirstName, r.LastName }).ToList();
            var nameFrequencies = nameProcessor.GetNameFrequencies(names);
            var addresses = records.Select(r => r.Address).ToList();
            var sortedAddresses = addressProcessor.GetSortedAddresses(addresses);

            Assert.Empty(nameFrequencies);
            Assert.Empty(sortedAddresses);

            File.Delete(tempCsv);
        }

        [Fact]
        public void ReadFile_ShouldHandleAddressWithComma()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,\"123 King St, Apt 4\",12345678"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Single(result);
            Assert.Equal("123 King St, Apt 4", result[0].Address);

            File.Delete(tempFile);
        }

        [Fact]
        public void FullProcessing_ShouldHandleSpecialCharacters()
        {
            var csvLines = new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "José,Müller,\"10 Rue de Paris\",98765432"
            };
            var tempCsv = Path.GetTempFileName();
            File.WriteAllLines(tempCsv, csvLines);

            IFileReader fileReader = new CsvFileReader();
            INameProcessor nameProcessor = new NameProcessor();
            IAddressProcessor addressProcessor = new AddressProcessor();

            var records = fileReader.ReadFile(tempCsv).ToList();
            var names = records.SelectMany(r => new[] { r.FirstName, r.LastName }).ToList();
            var nameFrequencies = nameProcessor.GetNameFrequencies(names);
            var addresses = records.Select(r => r.Address).ToList();
            var sortedAddresses = addressProcessor.GetSortedAddresses(addresses);

            Assert.Contains("José, 1", nameFrequencies);
            Assert.Contains("Müller, 1", nameFrequencies);
            Assert.Single(sortedAddresses);
            Assert.Contains("Rue de Paris", sortedAddresses[0]);

            File.Delete(tempCsv);
        }


    }
}
