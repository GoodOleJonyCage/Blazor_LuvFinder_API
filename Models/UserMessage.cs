using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserMessage
{
    public int Id { get; set; }

    public int FromId { get; set; }

    public int ToId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? Date { get; set; }

    public bool? MessageRead { get; set; }

    public DateTime? MessageReadDate { get; set; }

    public virtual User From { get; set; } = null!;

    public virtual User To { get; set; } = null!;
}
