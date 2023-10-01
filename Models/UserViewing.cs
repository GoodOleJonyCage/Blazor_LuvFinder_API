using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserViewing
{
    public int Id { get; set; }

    public int UserViewingId { get; set; }

    public int UserViewedId { get; set; }

    public DateTime Date { get; set; }

    public virtual User UserViewed { get; set; } = null!;

    public virtual User UserViewingNavigation { get; set; } = null!;
}
