using System;

namespace AegisVault.Models.Database;
public class CreateLinkDatabase{
    public Guid DbId {get;set;}
    public string Url {get;set;}
    public string Password {get;set;}
}