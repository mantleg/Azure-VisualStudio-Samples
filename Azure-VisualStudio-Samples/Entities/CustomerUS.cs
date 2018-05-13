using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure_VisualStudio_Samples.Entities
{
    class CustomerUS:TableEntity
    {
        public string Name { get; set; }
        public string EMail { get; set; }

        public CustomerUS(string name,string email)
        {
            this.Name = name;
            this.EMail = email;
            this.PartitionKey = "US";
            this.RowKey = email;
        }
    }
}
