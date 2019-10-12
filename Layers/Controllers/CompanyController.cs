using AutoMapper;
using Layers.Models;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Layers.Util;

namespace Layers.Controllers
{
    public class CompanyController : Controller
    {
        ITMSService TMSService;

        public CompanyController(ITMSService service)
        {
            TMSService = service;
        }

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCompany(int id)
        {
            //авторизация
            int? sessionId = GetSessionId();

            //получение компании
            var company = new VMService(TMSService).GetCompany(id);

            //если есть компания то редактировать ее
            if (company.Creator.Id == sessionId)
            {
                return View(company);
            }

            return RedirectToAction("Index", "TMS");
        }

        [HttpPost]
        public ActionResult EditCompany(CompanyVM companyIn, int[] employedUsers)
        {
            var company = TMSService.GetCompany(companyIn.Id);

            company.Name = companyIn.Name;
            company.EnteringPassword = companyIn.EnteringPassword;
            var users = new List<UserDTO>();
            //установка пользователей которые еще работают в компании
            if (employedUsers != null)
            {
                for (int i = 0; i < company.Users.Count; i++)
                {
                    if (employedUsers.Contains(company.Users[i].Id))
                    {
                        users.Add(company.Users[i]);
                    }
                }
            }
            company.Users = users;
            //Обновление и переход обратно
            TMSService.UpdateCompany(company);
            return RedirectToAction("Index","TMS" );
        }

        public ActionResult Details(int id)
        {
            int? sessionId = GetSessionId();

            //получение компании 
            var company = new VMService(TMSService).GetCompany(id);

            return View(company);
        }
        //Создание компании
        public ActionResult Create()
        {
            int? sessionId = GetSessionId();
            //компании созданные пользователем
            var companies = new VMService(TMSService).GetCompaniesCreatedByUserID(sessionId);

            //если может создать то вьюшка создания
            bool canCreate = false;
            if (companies.Count() == 0)
                canCreate = true;

            ViewBag.canCreate = canCreate;
            return View();
        }

        //создание компании
        [HttpPost]
        public ActionResult Create(CompanyVM company)
        {
            //получение пользователя
            var user = new VMService(TMSService).GetUser((int)Session["Id"]);
            company.Creator = user;

            //создание компании в бд
            var mapperForCompany = new MapperConfiguration(cfg => cfg.CreateMap<CompanyVM, CompanyDTO>()).CreateMapper();
            var companyDTO = mapperForCompany.Map<CompanyVM, CompanyDTO>(company);
            TMSService.MakeCompany(companyDTO);

            //получение списка компаний
            var companies = new VMService(TMSService).GetListCompaniesCreatedByUserID((int)Session["Id"]);

            //не ну мало ли
            int? companyId = null;
            if (companies.Count() == 1)
                companyId = companies[0].Id;
            else if (companies.Count() == 0)
                throw new Exception("Не удалось создать компанию!");

            return RedirectToAction("Details", new { id = companyId});
        }

        public ActionResult Enter(bool? wrongPassOrSelfEntering)
        {
            int? sessionId = GetSessionId();

            //если вхождение в функцию было вызвано редиректом то нужно выдать соответсвующее сообщение
            if (wrongPassOrSelfEntering!= null)
            {
                if ((bool)wrongPassOrSelfEntering)
                {
                    ViewBag.msg = "Неверный пароль вступлнения в компанию";
                }
                else
                {
                    ViewBag.msg = "Нельзя вступить в свою компанию";
                }
            }
            //получение списка компаний
            List<CompanyVM> Companies = new VMService(TMSService).GetListCompanies();
            //получение юзера
            var user = new VMService(TMSService).GetUser((int)Session["Id"]);

            //создание выпадающего списка компаний
            SelectList companiesSL = new SelectList(Companies,"Id","Name");
            var model = new EnterCompanyVM { Companies = companiesSL};

            for (int i = 0; i < Companies.Count; i++)
            {
                bool employed = false;
                for (int j = 0; j < Companies[i].Users.Count; j++)
                {
                    if (Companies[i].Users[j].Id == user.Id)
                    {
                        employed = true;
                    }
                }
                if (employed)
                {
                    model.AlreadyEmployed = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enter(string enteringPassword, int Companies)
        {
            //дто компании вступления
            CompanyDTO companyDTO = TMSService.GetCompany(Companies);
            //пользователь который вступает
            UserDTO userDTO = TMSService.GetUser((int)Session["Id"]);

            //если вступает в свою же компанию
            if (companyDTO.Creator.Id == userDTO.Id)
            {
                return RedirectToAction("Enter", new { redirected = false });
            }

            //если все верно то вступление и затем детали компании
            if (enteringPassword == companyDTO.EnteringPassword)
            {
                companyDTO.Users.Add(userDTO);
                TMSService.UpdateCompany(companyDTO);
                return RedirectToAction("Details",new { id = companyDTO.Id});
            }
            return RedirectToAction("Enter", new { redirected = true });
            
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