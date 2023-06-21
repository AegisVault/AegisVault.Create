using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AegisVault.Create.Helpers;
using AegisVault.Models.Inbound;

namespace AegisVault.Function
{
    public class CreateLinkEmail
    {
        private readonly CreateLinkHelper _helper;
        private readonly AegisVaultContext _context;
        public CreateLinkEmail(AegisVaultContext dbContext) {
            _context = dbContext;
            _helper = new CreateLinkHelper(dbContext);
        }

        [FunctionName(nameof(CreateLinkEmailFunction))]
        public async Task<IActionResult> CreateLinkEmailFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v1/CreateLink")] CreateLinkInbound body,
            ILogger log)
        {            
            string res = JsonConvert.SerializeObject(await _helper.CreateLinkPassword(body));

            return new OkObjectResult(res);
        }
    }
}
