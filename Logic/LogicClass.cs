using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicClass
    {
        
        public User CreateUser()
        {
            AppContext context = new AppContext();
            User user = new User()
            {
                ID = Guid.NewGuid(),
                FullName = "Jerry Rice",
                PhoneNumber = "123-456-7890",
                Email = "asdf@fdsa.com",
                TeamID = Guid.NewGuid(),
                RoleID = 2,
                PlayerPositionID = Guid.NewGuid()
            };
            return user;
        }
        public void SaveToDb()
        {

        }
    }
}
