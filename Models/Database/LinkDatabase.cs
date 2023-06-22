using System;

namespace AegisVault.Models.Database;
public class LinkDatabase
{
    public Guid DbId { get; set; }
    public string Url { get; set; }
    public string Password { get; set; }
    public string DisplayId { get; set; }

}