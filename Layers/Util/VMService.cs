using AutoMapper;
using Layers.Models;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layers.Util
{
    /// <summary>
    /// Все методы здесь сделаны возвращают вьюшки в контроллер из которого были вызваны
    /// </summary>
    public class VMService
    {
        ITMSService TMSService;

        public VMService(ITMSService service)
        {
            TMSService = service;
        }

        
        public CompanyVM GetCompany(int id)
        {
            CompanyDTO companyDTO = TMSService.GetCompany(id);
            var mapperComp = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyVM>()).CreateMapper();
            var company = mapperComp.Map<CompanyDTO, CompanyVM>(companyDTO);
            return company;
        }

        public List<CompanyVM> GetListCompanies()
        {
            IEnumerable<CompanyDTO> sessionDTO = TMSService.GetCompanies();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyVM>()).CreateMapper();
            var companies = mapper.Map<IEnumerable<CompanyDTO>, List<CompanyVM>>(sessionDTO).ToList();
            return companies;
        }

        public IEnumerable<CompanyVM> GetCompaniesCreatedByUserID(int? id)
        {
            IEnumerable<CompanyDTO> sessionDTO = TMSService.GetCompanies();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyVM>()).CreateMapper();
            var company = mapper.Map<IEnumerable<CompanyDTO>, List<CompanyVM>>(sessionDTO).Where(n => n.Creator.Id == id);
            return company;
        }

        public List<CompanyVM> GetListCompaniesCreatedByUserID(int? id)
        {
            IEnumerable<CompanyDTO> sessionDTO = TMSService.GetCompanies();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, CompanyVM>()).CreateMapper();
            var company = mapper.Map<IEnumerable<CompanyDTO>, List<CompanyVM>>(sessionDTO).Where(n => n.Creator.Id == id).ToList();
            return company;
        }

        public UserVM GetUser(int id)
        {
            UserDTO userDto = TMSService.GetUser(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var user = mapper.Map<UserDTO, UserVM>(userDto);
            return user;
        }

        public IEnumerable<SessionVM> GetSessionsById(int id)
        {
            IEnumerable<SessionDTO> sessionDTO = TMSService.GetSessions();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SessionDTO, SessionVM>()).CreateMapper();
            var session = mapper.Map<IEnumerable<SessionDTO>, List<SessionVM>>(sessionDTO).Where(n => n.UserId == id).OrderByDescending(j => j.Id); 
            return session;
        }

        public SessionVM GetSession(int id)
        {
            SessionDTO sessionDTO = TMSService.GetSession(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SessionDTO, SessionVM>()).CreateMapper();
            var session = mapper.Map<SessionDTO, SessionVM>(sessionDTO);
            return session;
        }
    }
}