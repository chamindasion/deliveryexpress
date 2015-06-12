using AP.PD.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace AP.PD.Data
{
    public class AuthRepository : IAuthRepository
    {
        public UserDomain FindUser(string userName, string password)
        {
            //TODO: Add password logic
            using (var apContext = new ApContext())
            {
                var user =
                    apContext.Users.Where(p => p.LoginId.Equals(userName, StringComparison.OrdinalIgnoreCase)
                                                         && p.Password.Equals(password)).Include(p => p.Role).FirstOrDefault();
                return user;
            }
        }
    }
}