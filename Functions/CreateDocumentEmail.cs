using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AegisVault.Create.Helpers;
using AegisVault.Create.Models.Integration;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;
using Microsoft.AspNetCore.Http;
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
        public CreateDocumentEmail(AegisVaultContext dbContext)
        {
            _context = dbContext;
            _helper = new CreateHelper(dbContext);
        }

        [FunctionName(nameof(CreateDocumentEmailFunction))]
        public async Task<IActionResult> CreateDocumentEmailFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v2/CreateDocumentEmail")] HttpRequest req,
            ILogger log)
        {
            IFormFile formData = req.Form.Files.FirstOrDefault();
            IFormCollection cols = await req.ReadFormAsync();
            string password = cols["password"];
            CreateLinkOutbound res = await _helper.CreateDocumentPassword(formData, password);

            //prepare emailgen body
            var sendEmailRequest = new SendEmailRequest
            {
                Brand = new ()
                {
                    BrandlogoUrl = cols["brandLogoURL"],
                    Brandname = cols["brandname"],
                    BrandPrimaryColor = cols["brandPrimaryColor"],
                    BrandSecondaryColor = cols["brandSecondaryColor"]
                },
                DocumentType = cols["DocumentType"],
                RequiredContent = cols["RequiredContent"],
                AegisLink = cols["Link"],
                Name = cols["Name"],
                AccountNumber = "",
                Email = cols["Email"]
            };

            //Create HttpClient
            using var httpClient = new HttpClient();

            //Serialize your request
            var content = new StringContent(JsonConvert.SerializeObject(sendEmailRequest), Encoding.UTF8, "application/json");

            //send request to https://aegisvault-email.azurewebsites.net/api/SendEmail
            var sendEmailResponse = await httpClient.PostAsync("https://aegisvault-email.azurewebsites.net/api/SendEmail", content);

            var response = new CreateRedirectOutbound
            {
                Link = res.Link,
                Email = sendEmailRequest.Email
            };
            string serializedResponse = JsonConvert.SerializeObject(response);

            //Ensure the request was successful
            if (!sendEmailResponse.IsSuccessStatusCode)
            {
                log.LogError($"Error: {sendEmailResponse.StatusCode}");
                return new BadRequestObjectResult(serializedResponse);

            }

            return new OkObjectResult(serializedResponse);
        }
    }
}
