using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class City
{
    public int Id { get; set; }

    public int RegionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
