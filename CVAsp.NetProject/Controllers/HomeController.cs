using AutoMapper;
using CVAsp.NetProject.Entities;
using CVAsp.NetProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CVAsp.NetProject.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public HomeController(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            var model = _mapper.Map<UserInfoViewModel>(user);
            return View(model);
        }

      
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetData()
        {
            return Json(new { Name = "Harun", Surname = "Tasci" });
        }
        [HttpPost]
        public IActionResult PostData([FromBody] PostDataApiModel model)
        {
            return Json(new { Error = false, Message = "Success" });
        }
    }
}