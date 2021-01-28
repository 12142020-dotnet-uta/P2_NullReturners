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
      
        public byte[] ConvertImage(string image)
        {
            //take everything after the ,
            string base64Image1 = image.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64Image1);
            return bytes;
        }

        public PlayDto ConvertToPlayDto(Play play)
        {
            PlayDto playDto = new PlayDto
            {
                PlaybookID = play.PlayID,
                Name = play.Name,
                Description = play.Description,
                DrawnPlay = play.DrawnPlay,
                ImageString = ConvertByteArrayToJpgString(play.DrawnPlay)
            };
            return playDto;
        }

        private string ConvertByteArrayToJpgString(byte[] byteArray)
        {
            if (byteArray != null)
            {
                string imageBase64Data = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64Data}");
                return imageDataURL;
            }
            else return null;
            //Image image;
            //using (MemoryStream ms = new MemoryStream(bytes))
            //{
            //    image = Image.FromStream(ms);
            //}

            //return image;
        }

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

        public UserLoggedInDto ConvertUserToUserLoggedInDto(User user)
        {
            UserLoggedInDto convertedUser = new UserLoggedInDto()
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
