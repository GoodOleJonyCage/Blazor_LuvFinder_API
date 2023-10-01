using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class Profile
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int? Order { get; set; }

    public virtual Question Question { get; set; } = null!;
}
