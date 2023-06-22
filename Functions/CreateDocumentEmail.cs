using System.Threading.Tasks;
using AegisVault.Create.Helpers;
using AegisVault.Models.Inbound;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AegisVault.Create.Functions
{
    public class CreateDocumentEmail
    {
        private readonly CreateHelper _helper;
        private readonly AegisVaultContext _context;
        public CreateDocumentEmail(AegisVaultContext dbContext) {
            _context = dbContext;
            _helper = new CreateHelper(dbContext);
        }

        [FunctionName(nameof(CreateDocumentEmailFunction))]
        public async Task<IActionResult> CreateDocumentEmailFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v1/CreateDocumentEmail")] CreateLinkInbound body,
            ILogger log)
        {            
            string res = JsonConvert.SerializeObject(await _helper.CreateLinkPassword(body));

            return new OkObjectResult(res);
        }
    }
}
