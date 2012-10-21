using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;

namespace DomainModel.Extensions
{
    public static class UserDataExtensions
    {
        public static User ConvertToUser(this UsersData data)
        {
            User result = new User()
            {
                UserID = data.UserID,
                Name = data.Name,
                Address = data.Address,
                Phone = data.Phone,
                Surname = data.Surname
            };
            return result;
        }
    }
}
