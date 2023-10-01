using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserBlog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<UserBlogComment> UserBlogComments { get; set; } = new List<UserBlogComment>();
}
