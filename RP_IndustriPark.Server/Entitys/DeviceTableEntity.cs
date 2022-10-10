//using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_IndustriPark.Server.Entitys
{
    public class DeviceTableEntity : TableEntity
    {

        public string? Location { get; set; }

        public DateTime Date { get; set; }

        public string? Type { get; set; }

        public bool Status { get; set; }
    }
}
