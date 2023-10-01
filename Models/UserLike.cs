using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserLike
{
    public int Id { get; set; }

    public int FromId { get; set; }

    public int ToId { get; set; }

    public DateTime Date { get; set; }

    public bool? LikeAccepted { get; set; }

    public DateTime? LikeAcceptedDate { get; set; }

    public virtual User From { get; set; } = null!;

    public virtual User To { get; set; } = null!;
}
