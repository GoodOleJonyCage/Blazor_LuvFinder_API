using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserGender
{
    public int Id { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<UserInfo> UserInfoGenders { get; set; } = new List<UserInfo>();

    public virtual ICollection<UserInfo> UserInfoSeekingGenders { get; set; } = new List<UserInfo>();
}
