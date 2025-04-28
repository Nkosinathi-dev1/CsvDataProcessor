using CsvProcessorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvProcessorApp.Services
{
    public class AddressProcessor : IAddressProcessor
    {
        public List<string> GetSortedAddresses(List<string> addresses)
        {
            return addresses
                .OrderBy(addr => GetStreetName(addr))
                .ToList();
        }

        private string GetStreetName(string address)
        { 
            //xunit tested and modified 
            var parts = address.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", parts.Skip(1)).Trim();
        }


    }
}
