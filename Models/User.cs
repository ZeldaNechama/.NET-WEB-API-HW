using System.Collections.Generic;
using tasks.Models;

namespace tasks.Models;

public class User
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Password {get; set;}
    public bool IsAdmin { get; set; }
}