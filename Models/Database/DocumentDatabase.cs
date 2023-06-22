using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Models.Database
{
    public class DocumentDatabase
    {
        public Guid DbId { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
        public string ContentType { get; set; }
        public string DisplayId { get; set; }
    }
}
