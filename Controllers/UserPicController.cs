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
 
    }
}
