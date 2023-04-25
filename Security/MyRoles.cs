using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SevvalImre_Proje.Models;

namespace SevvalImre_Proje.Security
{
    public class MyRoles : RoleProvider
    {
        public override string ApplicationName {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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
            using (RestoranRezervasyonEntities restoran = new RestoranRezervasyonEntities())
            {
                var userRoles = (from k in restoran.Kullanici
                                 join rl in restoran.Roller
                                 on k.KullaniciID equals rl.KullaniciID
                                 join kr in restoran.KullaniciRol
                                 on rl.RolID equals kr.RolID
                                 where k.Eposta == username
                                 select kr.RolAdi).ToArray();
                return userRoles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
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