using AegisVault.Models.Common;
using AegisVault.Models.Inbound;

namespace AegisVault.Create.Models.Inbound;

public class CreateRedirectEmailInbound
{
    public Brand Brand { get; set; }
    public CreateLinkInbound Link { get; set; }
    public string DocumentType { get; set; }
    public string RequiredContent { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

