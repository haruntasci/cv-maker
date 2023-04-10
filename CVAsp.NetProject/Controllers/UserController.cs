using AutoMapper;
using CVAsp.NetProject.Entities;
using CVAsp.NetProject.Helpers;
using CVAsp.NetProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CVAsp.NetProject.Controllers
{
    [Authorize(Roles = "admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public UserController(DatabaseContext databaseContext, IMapper mapper, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            List<UserModel> users =
                _databaseContext.Users.ToList()
                    .Select(x => _mapper.Map<UserModel>(x)).ToList();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists");
                    return View(model);
                }

                User user = _mapper.Map<User>(model);

                string hashed = _hasher.DoMD5HashedString(model.Password);
                user.Password = hashed;

                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public IActionResult Edit(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return View(model);
                }

                User user = _databaseContext.Users.Find(id);
                _mapper.Map(model, user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            User user = _databaseContext.Users.Find(id);

            if (user != null)
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
            }


            return RedirectToAction(nameof(Index));

        }



    }
}
