using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserMaritalStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
