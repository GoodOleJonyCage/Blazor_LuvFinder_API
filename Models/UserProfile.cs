using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public string? AnswerText { get; set; }

    public DateTime Date { get; set; }

    public bool? Selected { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
