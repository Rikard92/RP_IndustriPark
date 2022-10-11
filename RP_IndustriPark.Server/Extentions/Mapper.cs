using RP_IndustriPark.Server.Entitys;
using RP_IndustriPark.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_IndustriPark.Server.Extentions
{
    public static class Mapper
    {
        public static DeviceTableEntity ToTableEntity(this Device device)
        {
            return new DeviceTableEntity
            {
                Status = device.Status,
                Date = device.Date,
                Location = device.Location,
                Type = device.Type,
                PartitionKey = "Machine",
                RowKey = device.Id
                
            };
        }

        public static Device ToDevice(this DeviceTableEntity deviceTableEntity)
        {
            return new Device
            {
                Id = deviceTableEntity.RowKey,
                Status = deviceTableEntity.Status,
                Date = deviceTableEntity.Date,
                Location = deviceTableEntity.Location,
                Type = deviceTableEntity.Type

            };
        }

    }
}
