using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Presentation.Demo
{
    public static class ReadLicenceFileContent
    {
        // Documentation of BlobTrigger - https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-trigger?tabs=csharp#usage
        [FunctionName("ReadLicenceFileContent")]
        public static void Run(
            [BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")] string licenceFileContents, //If the file is Text file, we can use string as a file content parameter.
            string name, //Blob name
            ILogger log)
        {
            log.LogInformation($"******** READING LICENCE FILE *****************");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name}");
            log.LogInformation(licenceFileContents);
        }
    }
}
