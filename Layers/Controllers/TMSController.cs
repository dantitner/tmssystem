using AutoMapper;
using Layers.Models;
using Layers.Util;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layers.Controllers
{
    public class TMSController : Controller
    {
        ITMSService TMSService;

        public TMSController(ITMSService service)
        {
            TMSService = service;
        }
        [Authorize]
        // GET: TMS
        public ActionResult Index()
        {
            //получить ид сессии или редирект
            int? id = GetSessionId();

            if (id != null)
            {
                //получить пользователя
                var user = new VMService(TMSService).GetUser((int)id);

                //получить компании
                var companiesEmployee = new VMService(TMSService).GetListCompanies();

                CompanyVM employedComp = null;
                List<CompanyVM> createdComp = new List<CompanyVM>();

                //компании в которых сотрудник устроен
                for (int i = 0; i < companiesEmployee.Count; i++)
                {
                    bool employed = false;
                    for (int j = 0; j < companiesEmployee[i].Users.Count; j++)
                    {
                        if (companiesEmployee[i].Users[j].Id == user.Id)
                        {
                            employed = true;
                        }
                    }
                    if (employed)
                    {
                        employedComp = companiesEmployee[i];
                    }
                }

                //компании которые сотрудник создал
                for (int i = 0; i < companiesEmployee.Count; i++)
                {
                    bool created = false;
                    for (int j = 0; j < companiesEmployee[i].Users.Count; j++)
                    {
                        if (companiesEmployee[i].Creator.Id == user.Id)
                        {
                            created = true;
                        }
                    }
                    if (created)
                    {
                        CompanyVM company = new CompanyVM
                        {
                            Id = companiesEmployee[i].Id,
                            Creator = companiesEmployee[i].Creator,
                            Name = companiesEmployee[i].Name,
                            Users = companiesEmployee[i].Users
                        };
                        createdComp.Add(company);
                    }
                }

                ViewBag.createdComp = createdComp;
                ViewBag.employedComp = employedComp;
                return View("Index", user);
            }
            return RedirectToAction("Login", "Account");
        }
        //тестовый метод для получения всех пользователей
        public ActionResult ListUser()
        {
            IEnumerable<UserDTO> userDtos = TMSService.GetUsers();
            var mapper = new MapperConfiguration(cfg =>cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserVM>>(userDtos);
            return View(users);
        }

        //получить детальную информацию о пользователе (тестовый метод)
        public ActionResult UserInfo(int id)
        {
            //получить пользователя
            UserDTO userDto = TMSService.GetUser(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var user = mapper.Map<UserDTO, UserVM>(userDto);


            IEnumerable<CompanyDTO> companiesDTO = TMSService.GetCompanies();
            var mapperComp = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyVM>()).CreateMapper();
            var companiesEmployee = mapper.Map<IEnumerable<CompanyDTO>, List<CompanyVM>>(companiesDTO);

            CompanyVM employedComp = null;
            List<CompanyVM> createdComp = new List<CompanyVM>();

            //компании в которых сотрудник устроен
            for (int i = 0; i < companiesEmployee.Count; i++)
            {
                bool employed = false;
                for (int j = 0; j < companiesEmployee[i].Users.Count; j++)
                {
                    if (companiesEmployee[i].Users[j].Id == user.Id)
                    {
                        employed = true;
                    }
                }
                if (employed)
                {
                    employedComp = companiesEmployee[i];
                }
            }
            // компании которые он создал
            for (int i = 0; i < companiesEmployee.Count; i++)
            {
                bool created = false;
                for (int j = 0; j < companiesEmployee[i].Users.Count; j++)
                {
                    if (companiesEmployee[i].Creator.Id == user.Id)
                    {
                        created = true;
                    }
                }
                if (created)
                {
                    CompanyVM company = new CompanyVM
                    {   Id = companiesEmployee[i].Id,
                        Creator = companiesEmployee[i].Creator,
                        Name = companiesEmployee[i].Name,
                        Users = companiesEmployee[i].Users
                    };
                    createdComp.Add(company);
                }
            }
            ViewBag.createdComp = createdComp;
            ViewBag.employedComp = employedComp;
            return View("Index", user);
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