using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserProfilePic
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public virtual User? User { get; set; }
}
