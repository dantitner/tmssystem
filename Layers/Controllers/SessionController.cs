using AutoMapper;
using Layers.Models;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Layers.Util;
namespace Layers.Controllers
{
    public class SessionController : Controller
    {
        ITMSService TMSService;

        public SessionController(ITMSService service)
        {
            TMSService = service;
        }

        // GET: Session
        public ActionResult Index(int id,int? page)
        {
            //редирект или ид сессии
            int? sessionId = GetSessionId();

            //получение всех сессий
            var sessions = new VMService(TMSService).GetSessionsById(id);
            Session["companyId"] = sessions.ToList()[0].CompanyId;
            
            //дополнительная информация для VM
            foreach (var item in sessions)
            {
                item.CompanyName = TMSService.GetCompany(item.CompanyId).Name;
                item.UserName = TMSService.GetUser(item.UserId).Name;
                item.WorkTime = item.EndTime - item.StartTime;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sessions.ToPagedList(pageNumber,pageSize));
        }

        //паршал для вывода одной конкретной сессии
        [HttpPost]
        public ActionResult ListSessions(SessionVM session)
        {
                return PartialView(session);
        }

        //апдейт сессии
        [HttpPost]
        public ActionResult Update(int Id, string Commentary)
        {
            //получить ДТО и изменить в ней коментарий
            var SessionDTO = TMSService.GetSession(Id);
            SessionDTO.Commentary = Commentary;
            TMSService.UpdateSession(SessionDTO);

            //получить только что измененную сесиию
            var sessions = new VMService(TMSService).GetSession(Id);
            //доваить в нее доп инфу
            sessions.CompanyName = TMSService.GetCompany(sessions.CompanyId).Name;
            sessions.UserName = TMSService.GetUser(sessions.UserId).Name;
            sessions.WorkTime = sessions.EndTime - sessions.StartTime;

            return PartialView("ListSessions", sessions);
        }

        public ActionResult Create()
        {
            //редирект или получение сессии
            int userId = (int)GetSessionId();
            int companyId; 
            companyId = (int)Session["companyId"];
            //новая сессия
            SessionDTO newSession = new SessionDTO
            {
                UserId = userId,
                CompanyId = companyId,
                StartTime = DateTime.Now
            };
            //добавление и вывод
            TMSService.MakeSession(newSession);
            return RedirectToAction("Index","Session",new {id =userId});
        }

        public ActionResult Report(int userId, int companyId)
        {
            //сумка или пинок
            GetSessionId();

            //класс данных для вывода отчета
            Result result = new Result
            {
                UserId = userId,
                CompanyId = companyId
            };
            return View(result);
        }
        //метод который выоводит сессии в определенном промежутке времени
        [HttpPost]
        public ActionResult ResultPartial(int UserID, int CompanyId, DateTime EndDate, DateTime StartDate)
        {
            //сессии в которых юзер и компания совпадают а вермя больше стартового но меньше конечного
            IEnumerable<SessionDTO> sessionDTO = TMSService.GetSessions();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SessionDTO, SessionVM>()).CreateMapper();
            var sessions = mapper.Map<IEnumerable<SessionDTO>, List<SessionVM>>(sessionDTO).
            Where(j => j.UserId == UserID && j.CompanyId == CompanyId
            && j.StartTime > StartDate && j.EndTime < EndDate);

            //доп инфа
            foreach (var item in sessions)
            {
                item.CompanyName = TMSService.GetCompany(CompanyId).Name;
                item.UserName = TMSService.GetUser(UserID).Name;
                item.WorkTime = item.EndTime - item.StartTime;
            }

            return PartialView("_Result",sessions);
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