using AegisVault.Models.Common;

namespace AegisVault.Create.Models.Integration;

public class SendEmailRequest
{
    public Brand Brand { get; set; }
    public string DocumentType { get; set; }
    public string RequiredContent { get; set; }
    public string AegisLink { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string Email { get; set; }
}
