using AegisVault.Models.Database;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisVault.Create.Models.Database;
using Microsoft.AspNetCore.Http;

namespace AegisVault.Create.Helpers
{
    public class CreateHelper
    {
        private readonly AegisVaultContext _context;
        public CreateHelper(AegisVaultContext context)
        {
            _context = context;
        }

        public async Task<CreateLinkOutbound> CreateLinkPassword(CreateLinkInbound inboundData)
        {
            await _context.Database.EnsureCreatedAsync();
            CreateLinkDatabase databaseToInsert = new CreateLinkDatabase()
            {
                DbId = Guid.NewGuid(),
                Url = inboundData.Url,
                Password = inboundData.Password,
            };

            await _context.Links.AddAsync(databaseToInsert);
            await _context.SaveChangesAsync();

            CreateLinkOutbound toReturn = new CreateLinkOutbound()
            {
                Link = $"https://AegisVault.dev/link/{databaseToInsert.DbId}"
            };

            return toReturn;
        }

        public async Task<CreateLinkOutbound> CreateDocumentPassword(IFormFile file, string password)
        {
            BlobStorageHelper blobStorageHelper = new BlobStorageHelper();
            string location = await blobStorageHelper.UploadFile(file);
            await _context.Database.EnsureCreatedAsync();
            CreateDocumentDatabase databaseToInsert = new CreateDocumentDatabase()
            {
                DbId = Guid.NewGuid(),
                Location = location,
                ContentType = file.ContentType,
                Password = password
            };

            await _context.Documents.AddAsync(databaseToInsert);
            await _context.SaveChangesAsync();

            CreateLinkOutbound toReturn = new CreateLinkOutbound()
            {
                Link = $"https://aegisvault.dev/file/{databaseToInsert.DbId}"
            };

            return toReturn;
        }
    }
}
