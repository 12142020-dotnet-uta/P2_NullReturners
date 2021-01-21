using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Linq;
using Models.DataTransfer;

namespace Models.Tests
{
    public class ModelTests
    {
        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
           // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        /// <summary>
        /// Validates the User Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateUser()
        {
            var user = new User
            {
                ID = Guid.NewGuid(),
                UserName = "jerry",
                Password = "jerryrice",
                FullName = "Jerry Rice",
                PhoneNumber = "111-111-1111",
                Email = "jerryrice@gmail.com",
                TeamID = 1,
                RoleID = 1
            };

            var errorcount = ValidateModel(user).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Makes sure the User Model doesn't accept invalid data
        /// </summary>
        [Fact]
        public void InvalidateUser()
        {
            var user = new User
            {
                ID = Guid.NewGuid(),
                UserName = "jerry",
                Password = "jerryrice",
                FullName = "Jerry Rice",
                PhoneNumber = "111-111-1111",
                Email = "1234",
                TeamID = 1,
                RoleID = 1
            };

            var results = ValidateModel(user);
            Assert.True(results.Count > 0);
            Assert.Contains(results, v => v.MemberNames.Contains("Email"));
        }

        /// <summary>
        /// Makes sure Team model works with valid data
        /// </summary>
        [Fact]
        public void ValidateTeam()
        {
            var team = new Team()
            {
                ID = 1,
                Name = "Broncos",
                Wins = 3,
                Losses = 1
            };

            var results = ValidateModel(team);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Role Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateRole()
        {
            var role = new Role()
            {
                ID = 1,
                RoleName = "Coach"
            };

            var results = ValidateModel(role);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Playbook Model works with valid data
        /// </summary>
        [Fact]
        public void ValidatePlaybook()
        {
            var playbook = new Playbook()
            {
                ID = 1,
                TeamID = 2
            };

            var results = ValidateModel(playbook);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Play Model works with valid data
        /// </summary>
        [Fact]
        public void ValidatePlay()
        {
            var play = new Play()
            {
                ID = 1,
                PlaybookId = 1,
                Name = "Tackle",
                Description = "Tackles other players",
                DrawnPlay = new byte[1]
            };

            var results = ValidateModel(play);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Message Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateMessage()
        {
            var message = new Message()
            {
                ID = Guid.NewGuid(),
                SenderID = Guid.NewGuid(),
                RecipientListID = Guid.NewGuid(),
                MessageText = "Yall ready for this?"
            };

            var results = ValidateModel(message);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Game Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateGame()
        {
            var game = new Game()
            {
                ID = 1,
                HomeTeamID = 1,
                AwayTeamID = 2,
                WinningTeam = 1,
                HomeScore = 24,
                AwayScore = 12
            };

            var results = ValidateModel(game);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Event Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEventSchedule()
        {
            var eventSchedule = new Event()
            {
                ID = 1,
                TeamID = 1,
                Description = "Training",
                EventDate = DateTime.Now,
                Location = "Local park",
                Message = "Show up to training!"
            };

            var results = ValidateModel(eventSchedule);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure EquipmentRequest Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEquipmentRequest()
        {
            var equipmentRequest = new EquipmentRequest()
            {
                ID = 1,
                UserID = Guid.Parse("ac4acf50-ad36-4b87-931d-69fe4aafc0ba"),
                TeamID = 1,
                RequestDate = DateTime.Now,
                Message = "Shoulder pads",
                ItemId = 1,
                Status = "Pending"
            };

            var results = ValidateModel(equipmentRequest);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Validates the CoachEditUserDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCoachEditUserDto()
        {
            var coachEdit = new CoachEditUserDto()
            {
                UserName = "brettfavre",
                Password = "brett123",
                FullName = "Brett Favre",
                PhoneNumber = "2222222",
                Email = "brettfavregmail.com",
                TeamID = 1,
                RoleID = 1
            };

            var errorcount = ValidateModel(coachEdit).Count;
            Assert.Equal(2, errorcount);
        }

        /// <summary>
        /// Validates the CreateEquipmentRequestDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateEquipmentRequestDto()
        {
            var equipmentRequest = new CreateEquipmentRequestDto()
            {
                UserID = Guid.NewGuid(),
                TeamID = 1,
                RequestDate = DateTime.Now,
                Message = "I need this now!",
                ItemID = 1,
                Status = "Pending"
            };

            var errorcount = ValidateModel(equipmentRequest).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the CreateGameDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateGameDto()
        {
            var game = new CreateGameDto()
            {
                HomeTeamID = 1,
                AwayTeamID = 2
            };

            var errorcount = ValidateModel(game).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the CreateUserDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateUserDto()
        {
            var user = new CreateUserDto()
            {
                UserName = "brettfavre",
                Password = "brett123",
                FullName = "Brett Favre",
                PhoneNumber = "2222222",
                Email = "brettfavregmail.com",
                TeamID = 1,
                RoleID = 1
            };

            var errorcount = ValidateModel(user).Count;
            Assert.Equal(2, errorcount);
        }

        /// <summary>
        /// Validates the EditEquipmentRequestDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditEquipmentRequestDto()
        {
            var equipmentEdit = new EditEquipmentRequestDto()
            {
                Status = "Pending"
            };

            var errorcount = ValidateModel(equipmentEdit).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the EditGameDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditGameDto()
        {
            var gameEdit = new EditGameDto()
            {
                WinningTeamID = 1,
                HomeScore = 24,
                AwayScore = 12
            };

            var errorcount = ValidateModel(gameEdit).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the EditTeamDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditTeamDto()
        {
            var teamEdit = new EditTeamDto()
            {
                Name = "little ponies",
                Wins = 0,
                Losses = 10
            };

            var errorcount = ValidateModel(teamEdit).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the EditUserDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditUserDto()
        {
            var userEdit = new EditUserDto()
            {
                FullName = "Terry Bradshaw",
                Email = "footballiscoolgmail.com",
                Password = "bradshaw123",
                PhoneNumber = "2222",
            };

            var errorcount = ValidateModel(userEdit).Count;
            Assert.Equal(2, errorcount);
        }

        /// <summary>
        /// Validates the PlayDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidatePlayDto()
        {
            var play = new PlayDto()
            {
                PlaybookID = 1,
                Name = "tackles",
                Description = "tackle other players",
                DrawnPlay = new byte[1]
            };

            var errorcount = ValidateModel(play).Count;
            Assert.Equal(0, errorcount);
        }

    }
}
