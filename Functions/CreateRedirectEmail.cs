using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AegisVault.Create.Helpers;
using AegisVault.Create.Models.Inbound;
using AegisVault.Create.Models.Integration;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;

namespace AegisVault.Function
{
    public class CreateRedirectEmail
    {
        private readonly CreateHelper _helper;
        private readonly AegisVaultContext _context;
        public CreateRedirectEmail(AegisVaultContext dbContext) {
            _context = dbContext;
            _helper = new CreateHelper(dbContext);
        }

        [FunctionName(nameof(CreateLinkEmailFunction))]
        public async Task<IActionResult> CreateLinkEmailFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/CreateRedirectEmail")] CreateRedirectEmailInbound body,
            ILogger log)
        {
            var generatedLink = await _helper.CreateLinkPassword(body.Link);
            
            //prepare emailgen body
            var sendEmailRequest = new SendEmailRequest
            {
                Brand = body.Brand,
                DocumentType = body.DocumentType,
                RequiredContent = body.RequiredContent,
                AegisLink = generatedLink.Link,
                Name = body.Name,
                AccountNumber = "",
                Email = body.Email
            };

            //Create HttpClient
            using var httpClient = new HttpClient();
            if (httpClient.Timeout != TimeSpan.FromMinutes(3)) httpClient.Timeout = TimeSpan.FromMinutes(3);

            //Serialize your request
            var content = new StringContent(JsonConvert.SerializeObject(sendEmailRequest), Encoding.UTF8, "application/json");

            //send request to https://aegisvault-email.azurewebsites.net/api/SendEmail
            var sendEmailResponse = await httpClient.PostAsync("https://aegisvault-email.azurewebsites.net/api/SendEmail", content);
    
            var response = new CreateRedirectOutbound
            {
                Link = generatedLink.Link,
                Email = body.Email
            };
            string serializedResponse = JsonConvert.SerializeObject(response);
            
            //Ensure the request was successful
            if(!sendEmailResponse.IsSuccessStatusCode)
            {
                log.LogError($"Error: {sendEmailResponse.StatusCode}");
                return new BadRequestObjectResult(serializedResponse);

            }
            
            return new OkObjectResult(serializedResponse);
        }
    }
}
