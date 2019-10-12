using AutoMapper;
using LayersDAL.Entities;
using LayersDAL.Interfaces;
using LayersDLL.BLL;
using LayersDLL.Infrastructure;
using LayersDLL.Interfaces;
using LayersDLL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace LayersDLL.Services
{

    public class TMSService : ITMSService
    {
        IUnitOfWork Database { get; set; }

        public TMSService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakeSession(SessionDTO sessionDTO)
        {
            User user = Database.Users.Get(sessionDTO.UserId);

            if (user == null)
                throw new ValidationException("Пользователь не нейден","");

            Session session = new Session
            {
                CompanyId = sessionDTO.CompanyId,
                UserId =user.Id,
                StartTime = sessionDTO.StartTime,
                EndTime = sessionDTO.EndTime,
                Commentary = sessionDTO.Commentary
            };
            Database.Sessions.Create(session);
            Database.Save();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id покупателя", "");
            var user = Database.Users.Get(id.Value);
            if (user == null)
                throw new ValidationException("Покупатель не найден", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User,UserDTO>(Database.Users.Get((int)id));
        }

        public IEnumerable<CompanyDTO> GetCompanies()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Company>, List<CompanyDTO>>(Database.Companies.GetAll());
        }

        public IEnumerable<CompanyDTO> GetEmployedCompanies(UserDTO userDTO)
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDTO>()).CreateMapper();
            var empMap =  mapper.Map<IEnumerable<Company>, List<CompanyDTO>>(Database.Companies.GetAll());
            List<CompanyDTO> result = null;

            for (int i = 0; i < empMap.Count; i++)
            {
                for (int j = 0; j < empMap[i].Users.Count; j++)
                {
                    if (empMap[i].Users[j].Id == userDTO.Id)
                    {
                        result.Add(empMap[i]);
                    }
                }
            }

            return result;
        }

        public IEnumerable<SessionDTO> GetSessions()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Session>, List<SessionDTO>>(Database.Sessions.GetAll());
        }

        public void AddUser(UserDTO userDto)
        {
            User user = new User
            {
                Login = userDto.Login,
                Password = userDto.Password,
                Email = userDto.Email,
                Name = userDto.Name,
            };
            Database.Users.Create(user);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public CompanyDTO GetCompany(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id покупателя", "");
            int companyId = (int)id;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Company, CompanyDTO>()).CreateMapper();
            return mapper.Map<Company, CompanyDTO>(Database.Companies.Get(companyId));
        }

        public void UpdateCompany(CompanyDTO company)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyDTO, Company>()).CreateMapper();
            var item =  mapper.Map<CompanyDTO, Company>(company);
            Database.Companies.Update(item);
            Database.Save();
        }

        public SessionDTO GetSession(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id покупателя", "");
            int sessionId = (int)id;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Session, CompanyDTO>()).CreateMapper();
            return mapper.Map<Session, SessionDTO>(Database.Sessions.Get(sessionId));
        }

        public void UpdateSession(SessionDTO session)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SessionDTO, Session>()).CreateMapper();
            var item = mapper.Map<SessionDTO, Session>(session);
            Database.Sessions.Update(item);
            Database.Save();
        }

        public void MakeCompany(CompanyDTO companyDTO)
        {
            User user = Database.Users.Get(companyDTO.Creator.Id);
            Company company = new Company
            {
                Creator = user,
                Name = companyDTO.Name,
                EnteringPassword = companyDTO.EnteringPassword
            };
            Database.Companies.Create(company);
            Database.Save();
        }

        public void UpdateUser(UserDTO user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            var item = mapper.Map<UserDTO, User>(user);
            Database.Users.Update(item);
            Database.Save();
        }
    }
}