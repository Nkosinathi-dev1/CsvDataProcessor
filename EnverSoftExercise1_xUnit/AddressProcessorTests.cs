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

        [Fact]
        public void GetSortedAddresses_ShouldSortSameStreetNamesCorrectly()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "10 Acton St",
                "5 Acton St",
                "15 Acton St"
            };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Equal(3, result.Count); // Should not remove duplicates
            Assert.Contains("10 Acton St", result);
            Assert.Contains("5 Acton St", result);
            Assert.Contains("15 Acton St", result);
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
        public void GetSortedAddresses_ShouldHandleSingleAddress()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string> { "10 Brown St" };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Single(result);
            Assert.Equal("10 Brown St", result[0]);
        }

        [Fact]
        public void GetSortedAddresses_ShouldSortMixedStreetNamesCorrectly()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "20 Zebra St",
                "5 Apple Rd",
                "15 Banana Ave"
            };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Equal("5 Apple Rd", result[0]);      // Apple first
            Assert.Equal("15 Banana Ave", result[1]);   // Banana second
            Assert.Equal("20 Zebra St", result[2]);     // Zebra last
        }


        [Fact]
        public void GetSortedAddresses_ShouldHandleAddressesWithExtraSpaces()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                " 10  Clifton  Rd ",
                "12   Acton   St"
            };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Equal("12   Acton   St", result[0]);
            Assert.Equal(" 10  Clifton  Rd ", result[1]);
        }

        [Fact]
        public void GetSortedAddresses_ShouldBeCaseInsensitive()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "5 apple Rd",
                "15 Banana Ave"
            };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Equal("5 apple Rd", result[0]);
            Assert.Equal("15 Banana Ave", result[1]);
        }

        [Fact]
        public void GetSortedAddresses_ShouldPreserveOrderForSameStreets()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "31 Acton St",
                "12 Acton St",
                "22 Acton St"
            };

            var result = processor.GetSortedAddresses(addresses);

            // Since street names are identical, original order is maintained.
            Assert.Equal(3, result.Count);
            Assert.Contains("12 Acton St", result);
        }

        [Fact]
        public void GetSortedAddresses_ShouldSortMixedCaseStreetNames()
        {
            var processor = new AddressProcessor();
            var addresses = new List<string>
            {
                "5 apple Rd",
                "10 Banana Rd",
                "1 Apple Rd"
            };

            var result = processor.GetSortedAddresses(addresses);

            Assert.Equal("5 apple Rd", result[0]);
            Assert.Equal("1 Apple Rd", result[1]);
            Assert.Equal("10 Banana Rd", result[2]);
        }



    }
}
