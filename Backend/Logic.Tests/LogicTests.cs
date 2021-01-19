using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Logic;
using System;
using Xunit;

namespace Logic.Tests
{
    public class LogicTests
    {

        private readonly Mapper _mapper;
        private readonly ILogger<Repo> _logger;

        /// <summary>
        /// Tests the AddUser() method of LogicClass
        /// Tests that a user is added to the database
        /// TODO: may add assert statement to test that a duplicate user is not added
        /// </summary>
        [Fact]
        public void TestForCreateUser()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var user = logic.CreateUser("jerryrice", "jerry123", "Jerry Rice", "111-111-1111", "jerryrice@gmail.com");

                Assert.NotEmpty(context.Users);
            }
        }

        /// <summary>
        /// Tests the RemoveUser() method of the LogicClass
        /// Tests that a user is removed from the database
        /// TODO: may add assert statement to test that a duplicate user is not added
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task TestForDeleteUserAsync()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
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
                r.users.Add(user);
                await logic.DeleteUser(user.ID);
                Assert.Empty(context.Users);
            }
        }

        /// <summary>
        /// Tests the AddUserRole() method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForAddUserRole()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var user = new User
                {
                    ID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1
                };

                r.users.Add(user);
                await r.CommitSave();
                await logic.AddUserRole(user, 1);
                Assert.Equal(1, context.Users.Find(user.ID).RoleID);
            }
        }

        /// <summary>
        /// Tests the GetUsers() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetUsers()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
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

                r.users.Add(user);
                var listOfUsers = logic.GetUsers();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Tests the GetUserById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetUserById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
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

                r.users.Add(user);
                var listOfUsers = logic.GetUserById(user.ID);
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetTeams()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var team = new Team
                {
                    ID = 1,
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = logic.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetTeamById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var team = new Team
                {
                    ID = 1,
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = logic.GetTeamById(team.ID);
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetRoles()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var role = new Role
                {
                    ID = 1,
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = logic.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRoleById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetRoleById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var role = new Role
                {
                    ID = 1,
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = logic.GetRoleById(role.ID);
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetPlaybooks() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetPlaybooks()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var playbook = new Playbook
                {
                    ID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = logic.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookByid() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetPlaybookById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var playbook = new Playbook
                {
                    ID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = logic.GetPlaybookById(playbook.ID);
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetPlays()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var play = new Play
                {
                    ID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    drawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = logic.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetPlayById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetPlayById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var play = new Play
                {
                    ID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    drawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = logic.GetPlayById(play.ID);
                Assert.NotNull(listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetMessages() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetMessages()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var message = new Message
                {
                    ID = Guid.NewGuid(),
                    SenderID = 1,
                    RecipientID = 1,
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = logic.GetMessages();
                Assert.NotNull(listOfMessages);
            }
        }

        /// <summary>
        /// Tests the GetMessageById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetMessageById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var message = new Message
                {
                    ID = Guid.NewGuid(),
                    SenderID = 1,
                    RecipientID = 1,
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = logic.GetMessageById(message.ID);
                Assert.NotNull(listOfMessages);
            }
        }

        /// <summary>
        /// Tests the GetGames() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetGames()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var game = new Game
                {
                    ID = 1,
                    HomeTeamID = 1,
                    AwayTeamID = 2,
                    WinningTeam = 1,
                    HomeScore = 24,
                    AwayScore = 12
                };

                r.games.Add(game);
                var listOfGames = logic.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGameById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetGameById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var game = new Game
                {
                    ID = 1,
                    HomeTeamID = 1,
                    AwayTeamID = 2,
                    WinningTeam = 1,
                    HomeScore = 24,
                    AwayScore = 12
                };

                r.games.Add(game);
                var listOfGames = logic.GetGameById(game.ID);
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetEvents() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetEvents()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var eventSchedule = new Event
                {
                    ID = 1,
                    TeamID = 1,
                    Description = "Practice",
                    EventDate = DateTime.Now,
                    Location = "soccer field",
                    Message = "make it to practice!"
                };

                r.events.Add(eventSchedule);
                var listOfEvents = logic.GetEvents();
                Assert.NotNull(listOfEvents);
            }
        }

        /// <summary>
        /// Tests the GetEventById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetEventById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var eventSchedule = new Event
                {
                    ID = 1,
                    TeamID = 1,
                    Description = "Practice",
                    EventDate = DateTime.Now,
                    Location = "soccer field",
                    Message = "make it to practice!"
                };

                r.events.Add(eventSchedule);
                var listOfEvents = logic.GetEventById(eventSchedule.ID);
                Assert.NotNull(listOfEvents);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequests() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetEquipmentRequests()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var equipment = new EquipmentRequest
                {
                    ID = 1,
                    UserID = 1,
                    TeamID = 1,
                    RequestDate = DateTime.Now,
                    Description = "shoulder pads",
                    Status = "pending"
                };

                r.equipmentRequests.Add(equipment);
                var listOfEquipment = logic.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }
        /// <summary>
        /// Tests the GetEquipmentRequestById() method of LogicClass
        /// </summary>
        [Fact]
        public void TestForGetEquipmentRequestById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, _logger);
                LogicClass logic = new LogicClass(r, _mapper, _logger);
                var equipment = new EquipmentRequest
                {
                    ID = 1,
                    UserID = 1,
                    TeamID = 1,
                    RequestDate = DateTime.Now,
                    Description = "shoulder pads",
                    Status = "pending"
                };

                r.equipmentRequests.Add(equipment);
                var listOfEquipment = logic.GetEquipmentRequestById(equipment.ID);
                Assert.NotNull(listOfEquipment);
            }
        }

    } // end of class
} // end of namespace
