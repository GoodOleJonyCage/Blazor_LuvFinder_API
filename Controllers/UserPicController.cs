using LuvFinder_API.Helpers;
using LuvFinder_API.Models;
using LuvFinder_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuvFinder_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPicController : ControllerBase
    {
        private readonly LuvFinderContext db;
        private readonly IConfiguration _config;
        IWebHostEnvironment _webHostEnvironment;
        public UserPicController(LuvFinderContext _db, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost]
        [Route("upload")]
        public ActionResult Upload(List<IFormFile> files)
        {
            var username = Request.Form["username"][0];
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            if (files != null)
            {
                if (files.Count == 0)
                    return BadRequest("No Image uploaded");

                try
                {
                    //save the file
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            if (!file.IsImage())
                            {
                                return BadRequest("Has to be an image file");
                            }

                            # region add image to db
                            using (MemoryStream ms = new MemoryStream())
                            {
                                var existingEntryDB = db.UserProfilePicsDbs.Where(f => f.UserId == userID).SingleOrDefault();
                                if (existingEntryDB != null)
                                    db.UserProfilePicsDbs.Remove(existingEntryDB);
                                
                                file.CopyTo(ms);
                                db.UserProfilePicsDbs.Add(new UserProfilePicsDb()
                                {
                                    UserId = userID,
                                    ImageData = ms.ToArray()
                                });
                                db.SaveChanges();
                                
                                ms.Close();
                                ms.Dispose();
                                //add image to db
                            }
                            #endregion 

                            #region save image to file folder
                            //var fileInfo = new FileInfo(file.FileName);
                            //var fileNameWithUserID =  userID + fileInfo.Extension;
                            //var filePath = Helpers.Helpers.GetProfilePicPath(_webHostEnvironment.ContentRootPath,fileNameWithUserID);
                            ////var filePath = _webHostEnvironment.ContentRootPath
                            ////    + $"\\ClientApp\\public\\assets\\images\\userprofileimages\\{fileNameWithUserID}";

                            //if (System.IO.File.Exists(filePath))
                            //    System.IO.File.Delete(filePath);

                            //using (var stream = System.IO.File.Create(filePath))
                            //{
                            //    file.CopyTo(stream);
                            //}
                            ////save to db 

                            //var existingEntry = db.UserProfilePics.Where(f => f.UserId == userID).SingleOrDefault(); 
                            //if (existingEntry!= null)
                            //    db.UserProfilePics.Remove(existingEntry);

                            //db.UserProfilePics.Add(new UserProfilePic()
                            //{
                            //    Name = fileNameWithUserID,
                            //    UserId = userID,
                            //});
                            //db.SaveChanges();
                            #endregion 
                        }
                        break;//since we are only uploading one file 
                    }
                }
                catch (Exception exc)
                {
                    return BadRequest(exc.Message);
                }
                
            }
            return Ok("Uploaded");
        }
    }
}
