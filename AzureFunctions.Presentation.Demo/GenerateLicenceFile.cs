using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AzureFunctions.Presentation.Demo
{
    public static class GenerateLicenceFile
    {
        //Documentation of Queue trigger - https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-queue-trigger?tabs=csharp#usage
        //Documentation of Blob Output Binding - https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-output?tabs=csharp
        [FunctionName("GenerateLicenceFile")]
        public static void Run(
            [QueueTrigger("orders", Connection = "AzureWebJobsStorage")]Order order,
            [Blob("licenses/{rand-guid}.lic")] TextWriter outputBlob, //Example of input binding
            ILogger log)
        {
            outputBlob.WriteLine($"OrderId: {order.OrderId}");
            outputBlob.WriteLine($"Email: {order.Email}");
            outputBlob.WriteLine($"ProductId: {order.ProductId}");
            outputBlob.WriteLine($"PurchaseDate: {DateTime.UtcNow}");
            outputBlob.WriteLine($"LicenceCode: {Guid.NewGuid()}");

            log.LogInformation("************** GENERATING LICENCE FILE *******************\n");
            log.LogInformation($"OrderId: {order.OrderId}");
            log.LogInformation($"Email: {order.Email}");
            log.LogInformation($"ProductId: {order.ProductId}");
            log.LogInformation($"PurchaseDate: {DateTime.UtcNow}");
            log.LogInformation($"LicenceCode: {Guid.NewGuid()}");
        }
    }
}
