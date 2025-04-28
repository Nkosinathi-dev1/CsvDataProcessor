using CsvProcessorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnverSoftExercise1_xUnit
{
    public class CsvFileReaderTests
    {
            [Fact]
            public void ReadFile_ShouldParseValidCsvLines()
            {
                // Arrange
                var tempFile = Path.GetTempFileName();
                File.WriteAllLines(tempFile, new[]
                {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,12 Acton St,12345678",
                "James,Brown,22 Jones Rd,87654321"
            });

                var reader = new CsvFileReader(); 

                // Act
                var result = reader.ReadFile(tempFile); 

                // Assert
                var records = result.ToList();
                Assert.Equal(2, records.Count);
                Assert.Equal("Clive", records[0].FirstName);
                Assert.Equal("12 Acton St", records[0].Address);
                Assert.Equal("James", records[1].FirstName);

                // Cleanup
                File.Delete(tempFile);
            }

        [Fact]
        public void ReadFile_ShouldIgnoreMalformedLines()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,12 Acton St,12345678",
                "InvalidLineWithoutProperColumns"
            });

            var reader = new CsvFileReader();

            // Act
            var result = reader.ReadFile(tempFile).ToList();

            // Assert
            Assert.Single(result); // Only valid row parsed
            Assert.Equal("Clive", result[0].FirstName);

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldHandleFileWithOnlyHeaders()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Empty(result);

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldReturnEmptyWhenAllLinesMalformed()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "BadData1",
                "BadData2"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Empty(result); 

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldProcessValidAndSkipInvalidLines()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,12 Acton St,12345678",
                "InvalidLineWithoutProperColumns",
                "James,Brown,22 Jones Rd,87654321"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Clive", result[0].FirstName);
            Assert.Equal("James", result[1].FirstName);

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldIgnoreExtraColumns()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber,Email",
                "Clive,Owen,12 Acton St,12345678,clive@example.com"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Single(result);
            Assert.Equal("Clive", result[0].FirstName);
            Assert.Equal("12 Acton St", result[0].Address);

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldHandleQuotedFieldsWithLineBreaks()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,\"12 Acton St\nSuite 4\",12345678"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Single(result);
            Assert.Contains("12 Acton St", result[0].Address); // Should contain line break content

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldAllowDuplicateRows()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive,Owen,12 Acton St,12345678",
                "Clive,Owen,12 Acton St,12345678"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Equal(2, result.Count); // Both rows are valid
            Assert.Equal("Clive", result[0].FirstName);
            Assert.Equal("Clive", result[1].FirstName);

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldFailOnMixedDelimiters()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "Clive;Owen;12 Acton St;12345678" // Using semicolons
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Empty(result); // Cannot parse mixed delimiters

            File.Delete(tempFile);
        }

        [Fact]
        public void ReadFile_ShouldIgnoreBlankLines()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[]
            {
                "FirstName,LastName,Address,PhoneNumber",
                "",
                "Clive,Owen,12 Acton St,12345678",
                "",
                "James,Brown,22 Jones Rd,87654321"
            });

            var reader = new CsvFileReader();

            var result = reader.ReadFile(tempFile).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Clive", result[0].FirstName);
            Assert.Equal("James", result[1].FirstName);

            File.Delete(tempFile);
        }


    }
}
