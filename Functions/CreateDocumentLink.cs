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
using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Http.Internal;

namespace AegisVault.Function
{
    public class CreateDocumentLink
    {
        private readonly CreateHelper _helper;
        private readonly AegisVaultContext _context;
        public CreateDocumentLink(AegisVaultContext dbContext) {
            _context = dbContext;
            _helper = new CreateHelper(dbContext);
        }

        [FunctionName(nameof(CreateDocumentLinkFunction))]
        public async Task<IActionResult> CreateDocumentLinkFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v1/CreateFileLink")] HttpRequest req,
            ILogger log)
        {
            IFormFile formData = req.Form.Files.FirstOrDefault();
            IFormCollection cols = await req.ReadFormAsync();
            string password = cols["password"];
            string res = JsonConvert.SerializeObject(await _helper.CreateDocumentPassword(formData, password));
            return new OkObjectResult(res);
        }
    }
}
