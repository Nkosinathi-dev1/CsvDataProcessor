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
    }
}
