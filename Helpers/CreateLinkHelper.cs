using AegisVault.Models.Database;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Helpers
{
    public class CreateLinkHelper
    {
        private readonly AegisVaultContext _context;
        public CreateLinkHelper(AegisVaultContext context)
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

            _context.Links.Add(databaseToInsert);
            await _context.SaveChangesAsync();

            CreateLinkOutbound toReturn = new CreateLinkOutbound()
            {
                Link = $"https://AegisVault.dev/link/{databaseToInsert.DbId}"
            };

            return toReturn;
        }
    }
}
