using CsvProcessorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnverSoftExercise1_xUnit
{
    public class AddressProcessorTests
    {
        [Fact]
        public void GetSortedAddresses_ShouldSortByStreetName()
        {
            // Arrange
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "31 Clifton Rd",
                "12 Acton St",
                "22 Jones Rd"
            };

            // Act
            var result = processor.GetSortedAddresses(addresses);

            // Assert
            Assert.Equal("12 Acton St", result[0]);
            Assert.Equal("31 Clifton Rd", result[1]);
            Assert.Equal("22 Jones Rd", result[2]);
        }
    }
}
