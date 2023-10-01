using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int QuestionType { get; set; }

    public string? ShortDesc { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual QuestionType QuestionTypeNavigation { get; set; } = null!;

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
