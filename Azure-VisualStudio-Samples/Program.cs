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
            AzureStorageTable.CreateTable("customers");
        }
    }
}
