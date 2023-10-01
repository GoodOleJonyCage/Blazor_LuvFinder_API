using LuvFinder_API.Models;
using LuvFinder_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class SearchController : Controller
    {
        private readonly LuvFinderContext db;
        private readonly IConfiguration _config;
        IWebHostEnvironment _webHostEnvironment;
        public SearchController(LuvFinderContext _db, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("getsearchcriteria")]
        public ActionResult GetSearchCriteria()
        {
            SearchCriteria vm = new SearchCriteria();
            try
            {
                vm.Genders = new ProfileController(db, _config, _webHostEnvironment).LoadGenders();
                vm.SeekingGenders = new ProfileController(db, _config, _webHostEnvironment).LoadGenders();
                for (int i = 1; i < 100; i++)
                {
                    vm.MinAge.Add(i);
                    vm.MaxAge.Add(i);
                }
            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }
            

            return Ok(vm);
        }

        [HttpPost]
        [Route("search")]
        public ActionResult GetSearchCriteria([FromBody] System.Text.Json.JsonElement param)
        {
            var vm = JsonConvert.DeserializeObject<SearchCriteria>(param.GetProperty("vm").ToString());
            vm.Results = new List<ViewModels.UserInfo>();//clear search results
            var lstErrors = new List<string>();
            
            try
            {
                //if (vm.SelectedGender == 0)
                //    lstErrors.Add("Gender required");

                //if (vm.SelectedSeekingGender == 0)
                //    lstErrors.Add("Seeking Gender required");

                //if (vm.SelectedMinAge == 0)
                //    lstErrors.Add("Min age required");

                //if (vm.SelectedMaxAge == 0)
                //    lstErrors.Add("Max age required");

                //if (vm.CountryID == 0)
                //    lstErrors.Add("Country required");

                //if (vm.RegionID == 0)
                //    lstErrors.Add("Region required");

                //if (vm.CityID == 0)
                //    lstErrors.Add("City required");


                if (lstErrors.Count > 0)
                    return BadRequest(lstErrors);

                var lst = (from u in db.Users
                           join i in db.UserInfos on u.Id equals i.UserId
                           where
                           i.GenderId == (vm.SelectedGender == 0 ? i.GenderId : vm.SelectedGender) &&
                           i.SeekingGenderId == (vm.SelectedSeekingGender == 0 ? i.SeekingGenderId : vm.SelectedSeekingGender) &&
                           i.CountryId == (vm.CountryID > 0 ? vm.CountryID : i.CountryId) &&
                           i.RegionId == (vm.RegionID > 0 ? vm.RegionID : i.RegionId ) &&
                           i.CityId == (vm.CityID > 0 ? vm.CityID : i.CityId ) 
                           select new ViewModels.UserInfo()
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
                           }).ToList();

                lst.ForEach(user =>
                {
                    new ProfileController(db, _config, _webHostEnvironment).LoadUserDetails(user);
                });

                if (vm.SelectedMinAge!=0)
                    lst = lst.Where( i => i.Age >= vm.SelectedMinAge).ToList();
                
                if (vm.SelectedMaxAge != 0)
                    lst = lst.Where(i => i.Age <= vm.SelectedMaxAge).ToList();


                //for (int i = 0; i < 5; i++)
                //    lst.AddRange(lst);

                vm.Results = lst;

            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }


            return Ok(vm);
        }
   }
}
