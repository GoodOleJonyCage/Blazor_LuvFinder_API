using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserBlogComment
{
    public int Id { get; set; }

    public int? BlogId { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public DateTime? Date { get; set; }

    public int? ReplyTo { get; set; }

    public virtual UserBlog? Blog { get; set; }

    public virtual ICollection<UserBlogComment> InverseReplyToNavigation { get; set; } = new List<UserBlogComment>();

    public virtual UserBlogComment? ReplyToNavigation { get; set; }

    public virtual User? User { get; set; }
}
