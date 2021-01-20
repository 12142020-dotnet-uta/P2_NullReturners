using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Linq;

namespace Models.Tests
{
    public class ModelTests
    {

        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
           // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

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

        [Fact]
        public void ValidatePlay()
        {
            var play = new Play()
            {
                ID = 1,
                PlaybookId = 1,
                Name = "Tackle",
                Description = "Tackles other players",
                drawnPlay = new byte[1]
            };

            var results = ValidateModel(play);
            Assert.True(results.Count == 0);
        }

        [Fact]
        public void ValidateMessage()
        {
            var message = new Message()
            {
                ID = Guid.NewGuid(),
                SenderID = 1,
                RecipientID = 1,
                MessageText = "Yall ready for this?"
            };

            var results = ValidateModel(message);
            Assert.True(results.Count == 0);
        }

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
                Status = "Pending"
            };

            var results = ValidateModel(equipmentRequest);
            Assert.True(results.Count == 0);
        }
    }
}
