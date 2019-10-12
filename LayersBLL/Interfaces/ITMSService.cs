using LayersDLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.Interfaces
{
    public interface ITMSService
    {
        void MakeSession(SessionDTO sessionDTO);
        void MakeCompany(CompanyDTO companyDTO);
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<CompanyDTO> GetCompanies();
        IEnumerable<CompanyDTO> GetEmployedCompanies(UserDTO userDTO);
        IEnumerable<SessionDTO> GetSessions();
        void AddUser(UserDTO userDto);
        UserDTO GetUser(int? id);
        CompanyDTO GetCompany(int? id);
        SessionDTO GetSession(int? id);
        void UpdateCompany(CompanyDTO company);
        void UpdateSession(SessionDTO session);
        void UpdateUser(UserDTO user);
        void Dispose();
    }
}
