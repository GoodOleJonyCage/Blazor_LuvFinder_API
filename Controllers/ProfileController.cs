using LuvFinder_API.Models;
 
using LuvFinder_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LuvFinder_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly LuvFinderContext db;
        private readonly IConfiguration _config;
        IWebHostEnvironment _webHostEnvironment;
        public ProfileController(LuvFinderContext _db, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        //[NonActionAttribute]
        //private List<ProfileQuestion> GetProfileQuestions()
        //{
        //    List<LuvFinder_ViewModels.ProfileQuestion> lst = new List<LuvFinder_ViewModels.ProfileQuestion>();

        //    lst = db.Profiles
        //        .OrderBy(o => o.Order)
        //        .Select(profilequestion =>
        //    new LuvFinder_ViewModels.ProfileQuestion()
        //    {
        //        ID = profilequestion.Id,
        //        Question = new LuvFinder_ViewModels.Question()
        //        {
        //            QuestionID = profilequestion.QuestionId,
        //            Text = profilequestion.Question.Text,
        //            ShortDesc = profilequestion.Question.ShortDesc,
        //            QuestionTypeID = profilequestion.Question.QuestionType,
        //            QuestionType = (LuvFinder_ViewModels.QuestionType)profilequestion.Question.QuestionType,
        //            Answers = profilequestion.Question.Answers
        //                .Select(answer => new LuvFinder_ViewModels.Answer()
        //                {
        //                    ID = answer.Id,
        //                    Text = answer.Text,
        //                })
        //                .ToList()
        //        }
        //    })
        //    .ToList();
        //    return lst;
        //}

        //[NonActionAttribute]
        //private List<UserProfileQuestion> GetUserProfileQuestions(int userID)
        //{
        //    var lst = new List<LuvFinder_ViewModels.UserProfileQuestion>();

        //    lst = db.UserProfiles
        //        .Where(userProfile => userProfile.UserId == userID)
        //        .Select(profilequestion =>
        //        new LuvFinder_ViewModels.UserProfileQuestion()
        //        {
        //            ID = profilequestion.Id,
        //            UserID = profilequestion.UserId,
        //            QuestionID = profilequestion.QuestionId,
        //            AnswerID = profilequestion.AnswerId ?? 0,
        //            AnswerText = profilequestion.AnswerText ?? string.Empty,
        //            Date = profilequestion.Date,
        //            Selected = profilequestion.Selected ?? false
        //        })
        //        .ToList();

        //    return lst;
        //}

        //[HttpGet]
        //[Route("profilequestionnaire")]
        //public ActionResult ProfileQuestionnaire()
        //{
        //    List<ProfileQuestion> lst = GetProfileQuestions();
        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("userprofile")]
        //public ActionResult UserProfile([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var username = userParams.GetProperty("username").ToString();
        //    var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

        //    List<ProfileQuestion> lstProfileQuestions = GetProfileQuestions();
        //    List<UserProfileQuestion> lstUserProfile = GetUserProfileQuestions(userID);
        //    if(lstUserProfile.Count == 0) //profile not created
        //    {
        //        return BadRequest("User profile not found");
        //    }

        //    lstProfileQuestions.ForEach(question =>
        //    {
        //        if (question.Question.QuestionType == LuvFinder_ViewModels.QuestionType.OpenText)
        //        {
        //            question.AnswerText = lstUserProfile?
        //                                        .AsQueryable()?
        //                                        .Where(entry => entry.QuestionID == question.Question.QuestionID &&
        //                                                         entry.AnswerID == 0)
        //                                        .SingleOrDefault()?
        //                                        .AnswerText ?? string.Empty;
        //        }
        //        else
        //        {
        //            question?.Question.Answers.ForEach(answer =>
        //            {
        //                answer.Selected = lstUserProfile?
        //                                        .AsQueryable()?
        //                                        .Where(entry => entry.QuestionID == question.Question.QuestionID &&
        //                                                         entry.AnswerID == answer.ID)
        //                                        .SingleOrDefault()?.Selected ?? false;
        //            });
        //        }
        //    });

        //    return Ok(lstProfileQuestions);
        //}

        //[HttpGet]
        //[Route("countries")]
        //public ActionResult Countries()
        //{

        //    var lst = db.Countries.Select(c => new LuvFinder_ViewModels.Country()
        //    {
        //        ID = c.Id,
        //        Name = c.Name
        //    }).ToList();

        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("regions")]
        //public ActionResult Regions([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var countryid = Int32.Parse(userParams.GetProperty("countryid").ToString());
        //    var lst = db.Regions
        //        .Where( r => r.CountryId == countryid)
        //        .Select(c => new LuvFinder_ViewModels.Region()
        //    {
        //        ID = c.Id,
        //        Name = c.Name
        //    }).ToList();

        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("citiesbyname")]
        //public ActionResult CitiesByName([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var regionid = Int32.Parse(userParams.GetProperty("regionid").ToString());
        //    var cityname = userParams.GetProperty("cityname").ToString();
        //    var lst = new List<LuvFinder_ViewModels.City>();
        //    try
        //    {
        //         lst = db.Cities.Where(c =>
        //                        c.RegionId == regionid &&
        //                        c.Name.StartsWith(cityname))
        //                .Select(c => new LuvFinder_ViewModels.City()
        //                {
        //                    ID = c.Id,
        //                    Name = c.Name
        //                }).ToList();

        //    }
        //    catch (Exception)
        //    {


        //    }            

        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("cities")]
        //public ActionResult Cities([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var regionid = Int32.Parse(userParams.GetProperty("regionid").ToString());
        //    var lst = new List<LuvFinder_ViewModels.City>();
        //    try
        //    {
        //        lst = db.Cities.Where(c => c.RegionId == regionid)
        //               .Select(c => new LuvFinder_ViewModels.City()
        //               {
        //                   ID = c.Id,
        //                   Name = c.Name
        //               }).ToList();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Ok(lst);
        //}

        [HttpPost]
        [Route("userinfo")]
        public ActionResult UserInfo([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var username = userParams.GetProperty("username").ToString();
            var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

            LuvFinder_ViewModels.UserInfo? userinfo = GetUserInfo(userID);

            if (userinfo == null)
                return BadRequest("User profile not found"); 

            return Ok(userinfo);
        }

        [NonActionAttribute]
        public LuvFinder_ViewModels.UserInfo? GetUserInfo(int userID)
        {
            var user = db.UserInfos.Where(u => u.UserId == userID)
                .Select(info => new LuvFinder_ViewModels.UserInfo()
                {
                    UserID = info.UserId ?? 0,
                    UserName = info.User.Username ?? string.Empty,
                    LastName = info.LastName ?? string.Empty,
                    FirstName = info.FirstName ?? string.Empty,
                    GenderID = info.GenderId ?? 0,
                    MaritalStatusID = info.MaritalStatusId ?? 0,
                    DOB = info.Dob ?? DateTime.MinValue,
                    SeekingGenderID = info.SeekingGenderId ?? 0,
                    CountryID = info.CountryId,
                    CityID = info.CityId,
                    RegionID = info.RegionId,
                }).SingleOrDefault();

            if (user != null)
            {
                LoadUserDetails(user);
            }
            return user;
        }

        //[HttpGet]
        //[Route("initializeduserinfo")]
        //public ActionResult InitializedUserInfo()
        //{
        //    //return an initialized object to instantiate the vm
        //    return Ok(new LuvFinder_ViewModels.UserInfo());
        //}

        //[HttpPost]
        //[Route("saveprofile")]
        //public ActionResult SaveProfile([FromBody] System.Text.Json.JsonElement param)
        //{
        //    var username = param.GetProperty("username").ToString();
        //    var vm = JsonConvert.DeserializeObject<List<LuvFinder_ViewModels.ProfileQuestion>>(param.GetProperty("vm").ToString());
        //    var vminfo = JsonConvert.DeserializeObject<LuvFinder_ViewModels.UserInfo>(param.GetProperty("info").ToString());
        //    var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

        //    List<string> lstErrors = new List<string>();

        //    if (userID == 0 )
        //        return BadRequest("User Not found");

        //    if (string.IsNullOrEmpty(vminfo?.FirstName))
        //    {
        //        lstErrors.Add($"First name required");
        //    }
        //    if (string.IsNullOrEmpty(vminfo?.LastName))
        //    {
        //        lstErrors.Add($"Last name required");
        //    }
        //    if (!vminfo.DOB.HasValue)
        //    {
        //        lstErrors.Add($"Date of birth required");
        //    }

        //    if (vminfo?.GenderID == 0)
        //    {
        //        lstErrors.Add($"Gender required");
        //    }
        //    if (vminfo?.SeekingGenderID == 0)
        //    {
        //        lstErrors.Add($"Seeking Gender required");
        //    }
        //    if (vminfo?.MaritalStatusID == 0)
        //    {
        //        lstErrors.Add($"Marital Status required");
        //    }
        //    if (vminfo?.CountryID == 0)
        //    {
        //        lstErrors.Add($"Country required");
        //    }
        //    if (vminfo?.RegionID == 0)
        //    {
        //        lstErrors.Add($"Region required");
        //    }
        //    if (vminfo?.CityID == 0)
        //    {
        //        lstErrors.Add($"City required");
        //    }

        //    if (lstErrors.Count > 0)
        //    {
        //        return BadRequest(lstErrors);
        //    }

        //    try
        //    {
        //        var infotodelete = db.UserInfos.Where(u => u.UserId == userID).SingleOrDefault();
        //        if (infotodelete != null)
        //            db.UserInfos.Remove(infotodelete);

        //        db.UserInfos.Add(new Models.UserInfo()
        //        {
        //            FirstName = vminfo?.FirstName,
        //            LastName = vminfo?.LastName,
        //            CityId = vminfo?.CityID??0,
        //            CountryId = short.Parse((vminfo?.CountryID??0).ToString()),
        //            RegionId = vminfo?.RegionID??0,
        //            Dob = vminfo?.DOB,
        //            GenderId = vminfo?.GenderID,
        //            SeekingGenderId = vminfo?.SeekingGenderID,
        //            MaritalStatusId = vminfo?.MaritalStatusID,
        //            UserId = userID
        //        });
        //    }
        //    catch (Exception exc)
        //    {
        //        lstErrors.Add($"Error while updating basic info {exc.Message}");
        //        return BadRequest(lstErrors);
        //    }

        //    if (vm != null)
        //    {
        //        //clearing all validation errors
        //        vm.ToList().ForEach(question =>
        //        {
        //            question.Question.InvalidResponse = false;
        //            question.Question.Error = string.Empty;
        //        });

        //        vm.ToList().ForEach(question =>
        //        {
        //            if (question.Question.QuestionType == LuvFinder_ViewModels.QuestionType.OpenText)
        //            {
        //                if (string.IsNullOrEmpty(question.AnswerText))
        //                    question.Question.InvalidResponse = true;
        //            }
        //            else if(question.Question.QuestionType == LuvFinder_ViewModels.QuestionType.Check)
        //            {
        //                if (question.Question.Answers.All(ans => ans.Selected == false))
        //                {
        //                    question.Question.InvalidResponse = true;
        //                }
        //            }
        //            else if (question.Question.QuestionType == LuvFinder_ViewModels.QuestionType.Radio)
        //            {
        //                if (!question.Question.Answers.Any(ans => ans.Selected == true))
        //                {
        //                    question.Question.InvalidResponse = true;
        //                }
        //            }

        //            if (question.Question.InvalidResponse)
        //            {
        //                question.Question.Error = $"Value required for {question.Question.ShortDesc}";
        //            }

        //        });

        //        if (!vm.Any( q => q.Question.Error.Length > 0 ))//no errors found
        //        {
        //            db.UserProfiles.RemoveRange(db.UserProfiles.Where(u => u.UserId == userID).ToList());

        //            vm.ToList().ForEach(question =>
        //            {
        //                if (question.Question.Answers.Count == 0)
        //                {

        //                    db.UserProfiles.Add(new UserProfile()
        //                    {
        //                        UserId = userID,
        //                        QuestionId = question.Question.QuestionID,
        //                        AnswerText = string.IsNullOrEmpty(question.AnswerText) ? string.Empty : question.AnswerText,
        //                        Selected = false
        //                    });
        //                }
        //                else
        //                    question.Question.Answers.ForEach(answer =>
        //                    {
        //                        db.UserProfiles.Add(new UserProfile()
        //                        {
        //                            UserId = userID,
        //                            QuestionId = question.Question.QuestionID,
        //                            AnswerId = answer.ID,
        //                            AnswerText = string.Empty,
        //                            Selected = answer.Selected
        //                        });
        //                    });

        //            });

        //            db.SaveChanges();
        //        }
        //        else
        //            return BadRequest(vm);
        //    }

        //    return Ok(true);
        //}

        //[HttpGet]
        //[Route("maritalstatuses")]
        //public ActionResult MaritalStatuses()
        //{
        //    var lst = db.UserMaritalStatuses
        //                .Select(m => new LuvFinder_ViewModels.MaritalStatus()
        //                {
        //                    Id = m.Id,
        //                    Name = m.Status??string.Empty,
        //                }).ToList();

        //    return Ok(lst);
        //}

        //[HttpGet]
        //[Route("genders")]
        //public ActionResult Genders()
        //{
        //    List<Gender> lst = LoadGenders();

        //    return Ok(lst);
        //}

        //public List<Gender> LoadGenders()
        //{
        //    return db.UserGenders
        //                            .Select(m => new LuvFinder_ViewModels.Gender()
        //                            {
        //                                Id = m.Id,
        //                                Name = m.Gender ?? string.Empty,
        //                            }).ToList();
        //}

        [HttpPost]
        [Route("profiles")]
        public async Task<ActionResult> GetProfiles([FromBody] System.Text.Json.JsonElement param)
        {
            var username = param.GetProperty("username").ToString();
            var lst = await ProfileListAsync(username, null);

            return Ok(lst);
        }

        [NonActionAttribute]
        private async Task<List<LuvFinder_ViewModels.UserInfo>> ProfileListAsync(string username, List<int>? lstSelectFrom)
        {
            //select all profiles except the user if logged in
            var lst = await (from u in db.Users
                       join i in db.UserInfos on u.Id equals i.UserId
                       where u.Username != (string.IsNullOrEmpty(username) ? string.Empty : username) &&
                             (lstSelectFrom != null ? lstSelectFrom.Contains(u.Id) : true)
                       select new LuvFinder_ViewModels.UserInfo()
                       {
                           UserID = u.Id,
                           UserName = u.Username,
                           LastName = i.LastName ?? string.Empty,
                           FirstName = i.FirstName ?? string.Empty,
                           GenderID = i.GenderId ?? 0,
                           SeekingGenderID = i.SeekingGenderId ?? 0,
                           MaritalStatusID = i.MaritalStatusId ?? 0,
                           CountryID = i.CountryId,
                           CityID = i.CityId,
                           RegionID = i.RegionId,
                           DOB = i.Dob ?? DateTime.MinValue,
                       }).ToListAsync();

            lst.ForEach(user =>
            {
                LoadUserDetails(user);
            });
            return lst;
        }

        //[NonActionAttribute]
        //private List<LuvFinder_ViewModels.UserInfo> ProfileList(string username, List<int>? lstSelectFrom)
        //{
        //    //select all profiles except the user if logged in
        //    var lst =   (from u in db.Users
        //                     join i in db.UserInfos on u.Id equals i.UserId
        //                     where u.Username != (string.IsNullOrEmpty(username) ? string.Empty : username) &&
        //                           (lstSelectFrom != null ? lstSelectFrom.Contains(u.Id) : true)
        //                     select new LuvFinder_ViewModels.UserInfo()
        //                     {
        //                         UserID = u.Id,
        //                         UserName = u.Username,
        //                         LastName = i.LastName ?? string.Empty,
        //                         FirstName = i.FirstName ?? string.Empty,
        //                         GenderID = i.GenderId ?? 0,
        //                         SeekingGenderID = i.SeekingGenderId ?? 0,
        //                         MaritalStatusID = i.MaritalStatusId ?? 0,
        //                         CountryID = i.CountryId,
        //                         CityID = i.CityId,
        //                         RegionID = i.RegionId,
        //                         DOB = i.Dob ?? DateTime.MinValue,
        //                     }).ToList();

        //    lst.ForEach(user =>
        //    {
        //        LoadUserDetails(user);
        //    });
        //    return lst;
        //}

        [NonActionAttribute]
        public void LoadUserDetails(LuvFinder_ViewModels.UserInfo user)
        {
            user.Age = user.DOB.HasValue ? Helpers.Helpers.CalculateAge(user.DOB.Value) : 0;
            user.Gender = db.UserGenders.Where(g => g.Id == user.GenderID)?.SingleOrDefault()?.Gender ?? string.Empty;
            user.SeekingGender = db.UserGenders.Where(g => g.Id == user.SeekingGenderID).SingleOrDefault()?.Gender ?? string.Empty;
            user.MaritalStatus = db.UserMaritalStatuses.Where(m => m.Id == user.MaritalStatusID).SingleOrDefault()?.Status ?? string.Empty;
            user.CountryName = db.Countries.Where(c => c.Id == user.CountryID).SingleOrDefault()?.Name ?? string.Empty;
            user.CityName = db.Cities.Where(c => c.Id == user.CityID).SingleOrDefault()?.Name ?? string.Empty;
            user.RegionName = db.Regions.Where(r => r.Id == user.RegionID).SingleOrDefault()?.Name ?? string.Empty;
            //load images from file structure
            //user.ProfilePic =
            //Helpers.Helpers.GetProfilePicUrl(db.UserProfilePics.Where(p => p.UserId == user.UserID).SingleOrDefault()?.Name ?? string.Empty);
            //load images from db
            user.ProfilePic =
            Helpers.Helpers.GetProfilePicDB(db.UserProfilePicsDbs.Where(p => p.UserId == user.UserID).SingleOrDefault()?.ImageData ?? null);
        }

        //[HttpPost]
        //[Route("friendrequestcount")]
        //public ActionResult GetFriendRequestCount([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var usernameTo = userParams.GetProperty("usernameto").ToString();
        //    var userIDTo = (new UserController(new LuvFinderContext(), _config)).UserIDByName(usernameTo);
        //    var count = 0;
        //    try
        //    {
        //        count = db.UserLikes.Count(l => l.ToId == userIDTo && 
        //                                        l.LikeAccepted == false );
        //    }
        //    catch (Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }

        //    return Ok(count);
        //}

        [HttpPost]
        [Route("likeuser")]
        public async Task<ActionResult> LikeUser([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var usernameFrom = userParams.GetProperty("usernamefrom").ToString();
            var usernameTo = userParams.GetProperty("usernameto").ToString();

            var userIDFrom = await (new UserController(new LuvFinderContext(), _config)).UserIDByNameAsync(usernameFrom);
            var userIDTo = await (new UserController(new LuvFinderContext(), _config)).UserIDByNameAsync(usernameTo);

            try
            {
                if (!db.UserLikes.Any(l => l.FromId == userIDFrom && l.ToId == userIDTo))
                {
                    await db.UserLikes.AddAsync(new UserLike()
                    {
                        FromId = userIDFrom,
                        ToId = userIDTo,
                    });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("likeuserstatus")]
        public async Task<ActionResult> LikeUserStatus([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        {
            var usernameFrom = userParams.GetProperty("usernamefrom").ToString();
            var usernameTo = userParams.GetProperty("usernameto").ToString();

            var userIDFrom = await (new UserController(new LuvFinderContext(), _config)).UserIDByNameAsync(usernameFrom);
            var userIDTo = await (new UserController(new LuvFinderContext(), _config)).UserIDByNameAsync(usernameTo);

            var hasLiked = false;

            try
            {
                hasLiked = await db.UserLikes.AnyAsync(l => l.FromId == userIDFrom && l.ToId == userIDTo);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }

            return Ok(hasLiked);
        }

        //[HttpPost]
        //[Route("activityfriends")]
        //public ActionResult ActivityFriends([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var usernameTo = userParams.GetProperty("usernameto").ToString();
        //    var userIDTo = (new UserController(new LuvFinderContext(), _config)).UserIDByName(usernameTo);

        //    var lst = new List<FriendActivity>();

        //    try
        //    {
        //        lst = db.UserLikes
        //                  .Where(l => l.ToId == userIDTo)
        //                  .OrderBy( l => l.Date)
        //                  .Select(l => new FriendActivity()
        //                  {
        //                      FromID = l.FromId,
        //                      ToID = l.ToId,
        //                      Date = l.Date,
        //                      LikeAccepted = l.LikeAccepted??false,
        //                      LikeAcceptedDate = l.LikeAcceptedDate 

        //                  }).ToList();

        //        lst.ForEach(l =>
        //        {
        //            l.FromUserInfo = GetUserInfo(l.FromID);
        //            l.ToUserInfo = GetUserInfo(l.ToID);
        //         });

        //    }
        //    catch (Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }

        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("startfriendship")]
        //public ActionResult StartFriendShip([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var usernameFrom = userParams.GetProperty("usernamefrom").ToString();
        //    var usernameTo = userParams.GetProperty("usernameto").ToString();

        //    var userIDFrom = (new UserController(new LuvFinderContext(), _config)).UserIDByName(usernameFrom);
        //    var userIDTo = (new UserController(new LuvFinderContext(), _config)).UserIDByName(usernameTo);

        //    var activity = new FriendActivity();

        //    try
        //    {
        //        var entry  = db.UserLikes.Where(l => l.FromId == userIDFrom && l.ToId == userIDTo)
        //                    .SingleOrDefault();

        //        if (entry != null)
        //        {
        //            entry.LikeAccepted = true;
        //            entry.LikeAcceptedDate = DateTime.Now;

        //            //update or add corresponding mirror like since friendship started 
        //            var mirrorentry = db.UserLikes.Where(l => l.FromId == userIDTo && l.ToId == userIDFrom).SingleOrDefault();
        //            if (mirrorentry == null)
        //            {
        //                db.UserLikes.Add(new UserLike()
        //                {
        //                    FromId = userIDTo,
        //                    ToId = userIDFrom,
        //                    LikeAccepted = true,
        //                    LikeAcceptedDate = DateTime.Now
        //                });
        //            }
        //            else
        //            {
        //                mirrorentry.LikeAccepted = true;
        //                mirrorentry.LikeAcceptedDate = DateTime.Now;
        //            }

        //            db.SaveChanges();
        //        }

        //          activity = db.UserLikes.Where(l => l.FromId == userIDFrom && l.ToId == userIDTo)
        //                .Select(l => new FriendActivity()
        //                 {
        //                     FromID = l.FromId,
        //                     ToID = l.ToId,
        //                     Date = l.Date,
        //                     LikeAccepted = l.LikeAccepted ?? false,
        //                     LikeAcceptedDate = l.LikeAcceptedDate
        //                 })
        //                .SingleOrDefault();

        //        if(activity!= null)
        //            activity.FromUserInfo = GetUserInfo(activity.FromID);
        //    }
        //    catch (Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }

        //    return Ok(activity);
        //}


        //[HttpPost]
        //[Route("friends")]
        //public ActionResult GetFriends([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var username = userParams.GetProperty("username").ToString();
        //    var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

        //    List<LuvFinder_ViewModels.UserInfo> lst = new List<LuvFinder_ViewModels.UserInfo>();

        //    try
        //    {
        //        var lstFriendIds = db.UserLikes
        //                        .Where(l => l.FromId == userID &&
        //                                    l.LikeAccepted.HasValue && l.LikeAccepted == true)
        //                        .Select(l => l.ToId)
        //                        .ToList();

        //            lst = ProfileList(username, lstFriendIds);
        //    }
        //    catch (Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }

        //    return Ok(lst);
        //}

        //[HttpPost]
        //[Route("friendcount")]
        //public ActionResult GetFriendCount([Microsoft.AspNetCore.Mvc.FromBody] System.Text.Json.JsonElement userParams)
        //{
        //    var username = userParams.GetProperty("username").ToString();
        //    var userID = (new UserController(new LuvFinderContext(), _config)).UserIDByName(username);

        //    var count = 0;
        //    try
        //    {
        //        count = db.UserLikes
        //                   .Count(m => m.FromId == userID && (m.LikeAccepted.HasValue && m.LikeAccepted == true));
        //    }
        //    catch (Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }
        //    return Ok(count);
        //}

    }
}
