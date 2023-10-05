using LuvFinder_API.Helpers;
using LuvFinder_API.Models;
 
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LuvFinder_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly LuvFinderContext db;
        private readonly IConfiguration _config;
        IWebHostEnvironment _webHostEnvironment;
        public BlogController(LuvFinderContext _db, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("blogs")]
        public ActionResult Blogs([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var username = userParams.GetProperty("username").ToString();
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);
            var lst = new List<LuvFinder_ViewModels.Blog>();
            try
            {
                lst = GetUserBlogs(userID);
            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }
             
            return Ok(lst);
        }

        [HttpPost]
        [Route("blog")]
        public ActionResult Blog([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var username = userParams.GetProperty("username").ToString();
            var blogid = Int32.Parse(userParams.GetProperty("blogid").ToString());
            
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            LuvFinder_ViewModels.Blog blog = new LuvFinder_ViewModels.Blog();
            try
            {
                var lst = GetUserBlogs(userID);
                blog = lst.Where(b => b.ID == blogid).SingleOrDefault();
            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }

            return Ok(blog);
        }
        [NonActionAttribute]
        private List<LuvFinder_ViewModels.Blog> GetUserBlogs(int userID)
        {
            var lst = new List<LuvFinder_ViewModels.Blog>();
            lst = db.UserBlogs
                .Where(b => b.UserId == userID)
                .Select(b => new LuvFinder_ViewModels.Blog()
                {
                    ID = b.Id,
                    UserID = b.UserId ?? 0,
                    Title = b.Title ?? string.Empty,
                    Body = b.Body ?? string.Empty,
                    Image = Helpers.Helpers.GetProfilePicDB(b.Image),
                    CreateDate = b.CreateDate ?? null,
                    UpdateDate = b.UpdateDate ?? null
                }).ToList();

            LuvFinder_ViewModels.UserInfo user = new ProfileController(db, _config, _webHostEnvironment)
                                        .GetUserInfo(userID);

            //get blog comments
            lst.ForEach(blog =>
            {
                blog.user = user;
                blog.Comments = db.UserBlogComments
                                .Where(b => b.BlogId == blog.ID && !b.ReplyTo.HasValue)
                                .Select(b => new LuvFinder_ViewModels.BlogComment()
                                {
                                    ID = b.Id,
                                    BlogID = b.BlogId ?? 0,
                                    Date = b.Date,
                                    UserID = b.UserId ?? 0,
                                    Comment = b.Comment ?? string.Empty
                                }).ToList();

            });

            
            lst.ForEach(blog =>
            {
                blog.Comments.ForEach(c =>
                {
                    c.PostedBy = new ProfileController(db, _config, _webHostEnvironment)
                                   .GetUserInfo(c.UserID) ;

                    //get blog comment replies
                    c.Reply = db.UserBlogComments
                                .Where(b => b.BlogId == blog.ID &&
                                            (b.ReplyTo.HasValue && b.ReplyTo == c.ID))
                                .Select(b => new LuvFinder_ViewModels.BlogComment()
                                {
                                    Date = b.Date,
                                    BlogID = b.BlogId??0,
                                    UserID = b.UserId ?? 0,
                                    Comment = b.Comment ?? string.Empty,
                                }).SingleOrDefault();
                    
                    if (c.Reply != null)
                    {
                        c.ReplyTo = c.ID;
                        c.Reply.PostedBy = new ProfileController(db, _config, _webHostEnvironment)
                                            .GetUserInfo(c.Reply.UserID);
                    }

                });
            });
            return lst;
        }

        [HttpPost]
        [Route("blogcount")]
        public ActionResult BlogCount([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var username = userParams.GetProperty("username").ToString();
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            var count = 0;

            try
            {
                count = db.UserBlogs.Count(b => b.UserId == userID);

            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }

            return Ok(count);
        }

        [HttpPost]
        [Route("addblogcomment")]
        public ActionResult AddBlogComment([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var username = userParams.GetProperty("username").ToString();
            var blogid = Int32.Parse(userParams.GetProperty("blogid").ToString());
            var comment = userParams.GetProperty("comment").ToString();

            var replyto = userParams.GetProperty("replyto").ToString();
            int replytoID = Int32.Parse(replyto);

            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            try
            {
                db.UserBlogComments
                        .Add(new UserBlogComment()
                        {
                            BlogId = blogid,
                            Comment =comment,
                            UserId=userID,
                            ReplyTo = replytoID == 0 ? null : replytoID
                        });
               
                db.SaveChanges();
            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("createblog")]
        public ActionResult CreateBlog(List<IFormFile> files)
        {
            var title = Request.Form["title"][0];
            var body = Request.Form["body"][0];
            var username = Request.Form["username"][0];
            
            if (string.IsNullOrEmpty(title))
                return BadRequest("Title required");

            if (string.IsNullOrEmpty(body))
                return BadRequest("Body required");

            if(string.IsNullOrEmpty(username))
                return BadRequest("UserName required");

            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            if (files != null)
            {
                if (files.Count == 0)
                    return BadRequest("No Image uploaded");

                try
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            if (!file.IsImage())
                            {
                                return BadRequest("Has to be an image file");
                            }

                            byte[] imgArray;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                imgArray = ms.ToArray();
                                ms.Close();
                                ms.Dispose();
                            }
                            
                            db.UserBlogs.Add(new UserBlog()
                            {
                                Title = title,
                                Image = imgArray,
                                Body = body,
                                UserId = userID
                            });
                            db.SaveChanges();

                        }
                        break;//since we are only uploading one file 
                    }
                }
                catch (Exception exc)
                {
                    return BadRequest(exc.Message);
                }

            }
            return Ok("Blog Created successfully");
        }

        [HttpPost]
        [Route("editblog")]
        public ActionResult EditBlog(List<IFormFile> files)
        {
            var blogidStr = Request.Form["blogid"][0];
            var title = Request.Form["title"][0];
            var body = Request.Form["body"][0];
            var username = Request.Form["username"][0];
           
            if (string.IsNullOrEmpty(blogidStr))
                return BadRequest("Blog ID required");

            if (string.IsNullOrEmpty(title))
                return BadRequest("Title required");

            if (string.IsNullOrEmpty(body))
                return BadRequest("Body required");

            if (string.IsNullOrEmpty(username))
                return BadRequest("UserName required");

            int blogid = Int32.Parse(blogidStr);
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            try
            {
                byte[] imgArray = null;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //NOTE The following doesnt work in blazor but does in react
                        //diff being the way we upload the image
                        //if (!file.IsImage())
                        //{
                        //    return BadRequest("Has to be an image file");
                        //}
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            imgArray = ms.ToArray();
                            ms.Close();
                            ms.Dispose();
                        }
                    }
                    break;//since we are only uploading one file 
                }

                var blog = db.UserBlogs.Where(b => b.Id == blogid).SingleOrDefault();
                if (blog != null)
                {
                    blog.Title = title;
                    blog.Image = imgArray?.Length > 0 ? imgArray : blog.Image;
                    blog.Body = body;
                }
                db.SaveChanges();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }

            return Ok("Blog Updated successfully");
        }
    }
}
