using AutoMapper;
using CVAsp.NetProject.Entities;
using CVAsp.NetProject.Helpers;
using CVAsp.NetProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CVAsp.NetProject.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;
        private readonly IMapper _mapper;

        public AccountController(DatabaseContext databaseContext, IConfiguration configuration, IHasher hasher, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
            _hasher = hasher;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashed = _hasher.DoMD5HashedString(model.Password);

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashed);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User is locked.");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? String.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));


                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }


            }

            return View(model);
        }



        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");

                    return View(model);
                }

                var user = _mapper.Map<User>(model);

                string hashed = _hasher.DoMD5HashedString(model.Password);

                user.Password = hashed;

                _databaseContext.Users.Add(user);

                var affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User cannot be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            UserInfoViewModel model = _mapper.Map<UserInfoViewModel>(user);

            return View(model);
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);


            ViewData["ProfileImage"] = user.ProfileImageFileName;
        }

        

 


        [HttpPost]
        public IActionResult ProfileAddNewProps(UserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.Find(userid);

                if (model.File != null)
                {
                    string fileName = $"p_{userid}.{model.File.ContentType.Split('/')[1]}";
                    Stream stream = new FileStream($"wwwroot/uploads/{fileName}", FileMode.OpenOrCreate);
                    model.File.CopyTo(stream);
                    stream.Close();
                    stream.Dispose();
                    if (fileName != null)
                    {
                        user.ProfileImageFileName = fileName;
                    }
                    else
                    {
                        model.ProfileImageFileName = user.ProfileImageFileName;
                    }
                }
                    
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.Github = model.Github;
                user.Linkedin = model.Linkedin;
                user.Profession = model.Profession;
                user.Languages = model.Languages;
                user.Libraries = model.Libraries;
                user.Platform = model.Platform;
                user.Versioning = model.Versioning;
                user.Summary = model.Summary;               

                _databaseContext.SaveChanges();

                ViewData["ProfileImage"] = user.ProfileImageFileName;
                return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
