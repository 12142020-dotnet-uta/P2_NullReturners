using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.DataTransfer;

namespace Logic
{
    public class Mapper
    {
        public UserDto ConvertUserToUserDto(User user)
        {
            UserDto convertedUser = new UserDto()
            {
                UserID = user.UserID,
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                TeamID = user.TeamID,
                RoleID = user.RoleID
            };
            return convertedUser;
        }
    }
}
