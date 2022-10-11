using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using RP_IndustriPark.Shared;
using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;
using RP_IndustriPark.Server.Entitys;
using RP_IndustriPark.Server.Extentions;
using TableAttribute = Microsoft.Azure.WebJobs.TableAttribute;
using CloudTable = Microsoft.Azure.Cosmos.Table.CloudTable;
using TableOperation = Microsoft.Azure.Cosmos.Table.TableOperation;

namespace RP_IndustriPark.Server
{
    public static class IndustryPark
    {
        
        //[FunctionName("GetIndustry")]
        //public static async Task<IActionResult> Get(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "industrypark")] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request to get a list of industries.");

        //    var industries = GetIndustries();

        //    return new OkObjectResult(industries);
        //}

        //private static IEnumerable<Device> GetIndustries()
        //{
        //    return new List<Device>()
        //    {
        //        new Device()
        //        {
        //            DeviceID = "12cw3-dsd23-rt64t",
        //            Date = DateTime.Now,
        //            Status = true,
        //            Location = "Stockholm",
        //            Type = "Weather Sensor"
        //        }
        //    };
        //}


        [FunctionName("GetIndustry")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "industrypark")] HttpRequest req,
            [Table("items", Connection = "AzureWebJobsStorage")] CloudTable itemTable,
            ILogger log)
        {
            log.LogInformation("Get Device");

            var query = new Microsoft.Azure.Cosmos.Table.TableQuery<DeviceTableEntity>();
            var result = await itemTable.ExecuteQuerySegmentedAsync(query, null);

            var response = result.Select(Mapper.ToDevice).ToList();

            return new OkObjectResult(response);
        }

        [FunctionName("CreateDevice")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "industrypark")] HttpRequest req,
            [Table("items", Connection = "AzureWebJobsStorage")] CloudTable itemTable, // IAsyncCollector<ItemTableEntity> itemTable,
            ILogger log)
        {
            log.LogInformation("Create Device");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createDevice = JsonConvert.DeserializeObject<Device>(requestBody);

            //if (createItem == null || string.IsNullOrWhiteSpace(Device.DeviceId)) return new BadRequestResult();

            var device = new Device {
                //DeviceID = createDevice.DeviceID,
                Status = createDevice.Status,
                Date = createDevice.Date,
                Location = createDevice.Location,
                Type = createDevice.Type
            };

            // await itemTable.AddAsync(item.ToTableEntity());

            var operation = TableOperation.Insert(device.ToTableEntity());
            var res = await itemTable.ExecuteAsync(operation);

            return new OkObjectResult(device);
        }
        [FunctionName("DeleteDevice")]
        public static async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "industrypark/{id}")] HttpRequest req,
            [Table("items", "Machine", "{id}", Connection = "AzureWebJobsStorage")] DeviceTableEntity deviceTableToDelete,
            [Table("items", Connection = "AzureWebJobsStorage")] CloudTable itemTable,
            ILogger log, string id)
        {
            log.LogInformation("Delete Device");

            var operation = TableOperation.Delete(deviceTableToDelete);
            await itemTable.ExecuteAsync(operation);
            return new NoContentResult();
        }

        [FunctionName("ToggleDevice")]
        public static async Task<IActionResult> Put(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "industrypark/{id}")] HttpRequest req,
            [Table("items", Connection = "AzureWebJobsStorage")] CloudTable itemTable,
            ILogger log, string id)
        {
            log.LogInformation("Put Device");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deviceToUpdate = JsonConvert.DeserializeObject<Device>(requestBody);

            //if (itemToUpdate is null || string.IsNullOrEmpty(id)) return new BadRequestResult();

            var opertaion = TableOperation.Retrieve<DeviceTableEntity>("Machine", id);
            var found = await itemTable.ExecuteAsync(opertaion);

            if (found.Result == null) return new NotFoundResult();

            var existingDevice = found.Result as DeviceTableEntity;
            existingDevice.Status = !deviceToUpdate.Status;

            var opertionReplace = TableOperation.Replace(existingDevice);
            await itemTable.ExecuteAsync(opertionReplace);

            return new NoContentResult();
        }

    }
}
