using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunctions.Presentation.Demo
{
    public static class ReceivedPayment
    {
        //Documentation of output bindings - https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-queue-output?tabs=csharp
        [FunctionName("ReceivedPayment")]
        [return: Queue("orders")] // Example of output binding
        public static async Task<Order> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Payment payment = JsonConvert.DeserializeObject<Payment>(requestBody);
            
            log.LogInformation("*************** RECEIVED PAYMENT ******************");
            log.LogInformation($"ProductId: {payment.ProductId}");
            log.LogInformation($"Email: {payment.Email}");

            var order = new Order
            {
                Email = payment.Email,
                ProductId = payment.ProductId,
                OrderId = new Random().Next()
            };
            
            log.LogInformation("*************** GENERATE LICENCE ORDER ******************");
            log.LogInformation($"OrderId: {order.OrderId}");

            return order;
        }
    }
}
