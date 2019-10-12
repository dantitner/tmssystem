using AutoMapper;
using Layers.Models;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Layers.Controllers
{
    public class AccountController : Controller
    {
        ITMSService TMSService;

        public AccountController(ITMSService service)
        {
            TMSService = service;
        }

        public ActionResult Login()
        { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            { // поиск пользователя в бд
                UserVM user = null;
                var users = GetUser();
                user = users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    //Создание сессии
                    Session["Name"] = user.Name;
                    Session["Id"] = user.Id;
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "TMS");
                }
                else { ModelState.AddModelError("", "Пользователя с таким логином и паролем нет"); }
            }
            return View(model);
        }

        // получение пользователей из базы данных
        private List<UserVM> GetUser()
        {
            
            IEnumerable<UserDTO> userDtos =

            TMSService.GetUsers();

            var mapper = new MapperConfiguration(cfg =>

            cfg.CreateMap<UserDTO,
            UserVM>()).CreateMapper();
            return mapper.Map<IEnumerable<UserDTO>,

            List<UserVM>>(userDtos);

        }

        public ActionResult Register()
        { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // получение пользоваетелей
                UserVM user = null;
                var users = GetUser();
                user = users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    var userdto = new UserDTO
                    {
                        Name = model.Name,
                        Login = model.Login,
                        Password = model.Password,
                    };

                    TMSService.AddUser(userdto);
                    users = GetUser();
                    user = users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        Session["Name"] = user.Name; Session["Id"] = user.Id;
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "TMS");
                    }
                }
                else { ModelState.AddModelError("", "Пользователь с таким логином уже существует"); }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            //выход из сессии
            Session["Name"] = null;
            Session["Id"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            TMSService.Dispose();
            base.Dispose(disposing);
        }
        //редактирование пользователя
        public ActionResult Edit(bool? wrongPassword)
        {
            //
            int? sessionId = GetSessionId();

            if (wrongPassword != null)
            {
                if ((bool)wrongPassword)
                {
                    ViewBag.msg = "Неверный пароль вступлнения в компанию";
                }
                else
                {
                    ViewBag.msg = "Нельзя вступить в свою компанию";
                }
            }


            UserDTO userDto = TMSService.GetUser((int)Session["Id"]);

            var mapperForUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var user = mapperForUser.Map<UserDTO, UserVM>(userDto);

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserVM user, string currentPassword)
        {
            var mapperForUser = new MapperConfiguration(cfg => cfg.CreateMap<UserVM, UserDTO>()).CreateMapper();
            var userDTO = mapperForUser.Map<UserVM, UserDTO>(user);

            if (currentPassword != TMSService.GetUser(user.Id).Password)
                return RedirectToAction("Edit", new { wrongPassword = true });
            else
            {
                TMSService.UpdateUser(userDTO);
            }


            return RedirectToAction("Index", "TMS");
        }

        public int? GetSessionId()
        {
            int? result = null;
            if (Session["Id"] != null)
                result = (int)Session["Id"];
            else
                RedirectToAction("Login", "Account");
            return result;
        }
    }
}
    
