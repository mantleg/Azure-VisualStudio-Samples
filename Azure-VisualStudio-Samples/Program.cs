using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSamples.Libs;

namespace Azure_VisualStudio_Samples
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a new table object and add some customer data
            //AzureStorageTable.CreateCustomer(
            //    AzureStorageTable.CreateTable("customers"),
            //    new Entities.CustomerUS("glenn", "glenn_mantle@hotmail.com"));

            //Console.WriteLine(AzureStorageTable.GetCustomer(AzureStorageTable.CreateTable("customers"), "US", "glenn_mantle@hotmail.com"));
            AzureStorageTable.GetAllCustomers(AzureStorageTable.CreateTable("customers"));
                Console.ReadLine();

        }
    }
}
