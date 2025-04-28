using CsvProcessorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnverSoftExercise1_xUnit
{
    public class NameProcessorTests
    {
        [Fact]
        public void GetNameFrequencies_ShouldReturnCorrectFrequencies()
        {
            // Arrange
            var processor = new NameProcessor();
            var names = new List<string> { "Clive", "James", "Clive", "Graham", "Graham", "James", "Jimmy", "John" };

            // Act
            var result = processor.GetNameFrequencies(names);

            // Assert
            Assert.Equal("Clive, 2", result[0]);
            Assert.Equal("Graham, 2", result[1]);
            Assert.Equal("James, 2", result[2]);
            Assert.Contains("Jimmy, 1", result);
            Assert.Contains("John, 1", result);
        }

        [Fact]
        public void GetNameFrequencies_ShouldBeCaseInsensitive()
        {
            // Arrange
            var processor = new NameProcessor();
            var names = new List<string> { "Clive", "clive", "CLIVE" };

            // Act
            var result = processor.GetNameFrequencies(names);

            // Assert
            Assert.Single(result);
            Assert.Equal("Clive, 3", result[0]);
        }

        [Fact]
        public void GetNameFrequencies_ShouldTrimSpaces()
        {
            // Arrange
            var processor = new NameProcessor();
            var names = new List<string> { " Clive ", "Clive" };

            // Act
            var result = processor.GetNameFrequencies(names);

            // Assert
            Assert.Single(result);
            Assert.Equal("Clive, 2", result[0]);
        }


        [Fact]
        public void GetNameFrequencies_ShouldHandleEmptyList()
        {
            var processor = new NameProcessor();
            var names = new List<string>();

            var result = processor.GetNameFrequencies(names);

            Assert.Empty(result);
        }

        [Fact]
        public void GetSortedAddresses_ShouldHandleEmptyList()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>();

            var result = processor.GetSortedAddresses(addresses);

            Assert.Empty(result);
        }

        [Fact]
        public void GetNameFrequencies_ShouldHandleMixedCaseAndSpaces()
        {
            var processor = new NameProcessor();
            var names = new List<string> { "Clive", " Clive ", "CLIVE", "clive" };

            var result = processor.GetNameFrequencies(names);

            Assert.Single(result);
            Assert.Equal("Clive, 4", result[0]);
        }

        [Fact]
        public void GetNameFrequencies_ShouldHandleSpecialCharacters()
        {
            var processor = new NameProcessor();
            var names = new List<string> { "O'Connor", "O'Connor", "Anne-Marie" };

            var result = processor.GetNameFrequencies(names);

            Assert.Equal("O'Connor, 2", result[0]);
            Assert.Equal("Anne-Marie, 1", result[1]);
        }

        [Fact]
        public void GetNameFrequencies_ShouldIgnoreEmptyOrNumericNames()
        {
            var processor = new NameProcessor();
            var names = new List<string> { "Clive", "123", "", " ", "Clive" };

            var result = processor.GetNameFrequencies(names);

            Assert.Single(result);
            Assert.Equal("Clive, 2", result[0]);
        }

        [Fact]
        public void GetNameFrequencies_ShouldHandleNamesWithWhitespace()
        {
            var processor = new NameProcessor();
            var names = new List<string> { " Clive ", "\tClive\n", "CLIVE" };

            var result = processor.GetNameFrequencies(names);

            Assert.Single(result);
            Assert.Equal("Clive, 3", result[0]);
        }

        [Fact]
        public void GetNameFrequencies_ShouldHandleNonEnglishCharacters()
        {
            var processor = new NameProcessor();
            var names = new List<string> { "José", "José", "Müller", "Mueller" };

            var result = processor.GetNameFrequencies(names);

            Assert.Equal(3, result.Count);
            Assert.Equal("José, 2", result[0]);
            Assert.Contains("Müller, 1", result);
        }

        [Fact]
        public void GetNameFrequencies_ShouldHandleVeryLongNames()
        {
            var processor = new NameProcessor();
            var longName = new string('A', 1000);
            var names = new List<string> { longName, longName };

            var result = processor.GetNameFrequencies(names);

            Assert.Single(result);
            Assert.Equal($"{longName}, 2", result[0]);
        }


    }
}
