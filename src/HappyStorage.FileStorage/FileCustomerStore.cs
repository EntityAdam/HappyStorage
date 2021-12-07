using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace HappyStorage.FileStorage
{
    public class FileCustomerStore : ICustomerStore
    {
        private readonly IFileCustomerStoreSettings fileCustomerStoreSettings;

        public FileCustomerStore(IFileCustomerStoreSettings fileCustomerStoreSettings) =>
            this.fileCustomerStoreSettings = fileCustomerStoreSettings ?? throw new ArgumentNullException(nameof(fileCustomerStoreSettings));

        public void Create(NewCustomer newCustomer)
        {
            File.WriteAllText(GetFullNamePath(newCustomer.CustomerNumber), newCustomer.FullName);
            File.WriteAllText(GetAddressPath(newCustomer.CustomerNumber), newCustomer.Address);
        }

        public bool CustomerExists(string customerNumber) => File.Exists(GetFullNamePath(customerNumber)) || File.Exists(GetAddressPath(customerNumber));

        public void Delete(string customerNumber)
        {
            File.Delete(GetFullNamePath(customerNumber));
            File.Delete(GetAddressPath(customerNumber));
        }

        private string GetFullNamePath(string customerNumber) => $"{fileCustomerStoreSettings.GetRootPath()}\\{customerNumber}_FullName.txt";

        private string GetAddressPath(string customerNumber) => $"{fileCustomerStoreSettings.GetRootPath()}\\{customerNumber}_Address.txt";

        public IEnumerable<CustomerLookup> ListCustomers()
        {
            var customerFiles = Directory.EnumerateFiles(fileCustomerStoreSettings.GetRootPath(), "*_FullName.txt", SearchOption.TopDirectoryOnly);

            foreach (var file in customerFiles)
            {
                yield return GetLookupFromFileName(file);
            }
        }

        public NewCustomer GetCustomer(string customerNumber)
        {
            var customerFullName = File.ReadAllText(GetFullNamePath(customerNumber));
            var customerAddress = File.ReadAllText(GetAddressPath(customerNumber));
            return new NewCustomer(customerNumber, customerFullName, customerAddress);
        }

        private static CustomerLookup GetLookupFromFileName(string file)
        {
            var filename = Path.GetFileNameWithoutExtension(file).Split('_');
            var fullname = File.ReadAllText(file);
            return new CustomerLookup(filename[0], fullname, 0);
        }

        public void UpdateCustomer(NewCustomer newCustomerDetails)
        {
            Create(newCustomerDetails);
        }
    }
}