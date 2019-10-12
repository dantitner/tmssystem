using LayersDAL.EF;
using LayersDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Layers.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get { return "TMS"; } set { } }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Является ли пользователь создателем компании
        /// </summary>
        /// <param name="username"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string companyId)
        {
            bool outputResult = false;
            using (TimeManagmentSytemContex db = new TimeManagmentSytemContex("DefaultConnection"))
            {
                User user = db.Users.FirstOrDefault(u => u.Login == username);
                Company company = db.Сompanies.FirstOrDefault(u => u.Id == Convert.ToInt32(companyId));
                if (user != null && company!= null)
                {
                    if (user.Id == company.Creator.Id)
                        outputResult = true;
                }
            }
            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}