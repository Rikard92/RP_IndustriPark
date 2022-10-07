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

namespace RP_IndustriPark.Server
{
    public static class IndustryPark
    {
        //[FunctionName("IndustryPark")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "industrypark")] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    string responseMessage = string.IsNullOrEmpty(name)
        //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"Hello, {name}. This HTTP triggered function executed successfully.";

        //    return new OkObjectResult(responseMessage);
        //}
        [FunctionName("GetIndustry")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "industrypark")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request to get a list of industries.");

            var industries = GetIndustries();

            return new OkObjectResult(industries);
        }

        private static IEnumerable<MachineIndustry> GetIndustries()
        {
            return new List<MachineIndustry>()
            {
                new MachineIndustry()
                {
                    DeviceID = "12cw3-dsd23-rt64t",
                    Date = DateTime.Now,
                    Status = true,
                    Location = "Stockholm",
                    Type = "Weather Sensor"
                }
            };
        }
    }
}
