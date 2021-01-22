using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Logic;
using System;
using Xunit;
using System.Threading.Tasks;
using Models.DataTransfer;

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
                CreateUserDto cUD = new CreateUserDto()
                {
                    UserName = "jerryrice",
                    Password = "jerry123",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com"
                };
                var user = logic.CreateUser(cUD);

                Assert.NotEmpty(context.Users);

                var user2 = logic.CreateUser(cUD);
                Assert.Equal(16, context.Users.CountAsync().Result); // this is 16 because of seeding. remove when not seeding.
            }
        }

        /// <summary>
        /// Tests the RemoveUser() method of the LogicClass
        /// Tests that a user is removed from the database
        /// TODO: may add assert statement to test that a duplicate user is not added
        /// </summary>
        [Fact]
        public async void TestForDeleteUser()
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
                    UserID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };
                r.users.Add(user);
                await r.CommitSave();
                logic.DeleteUser(Guid.NewGuid()); // fails for some reason when I add await
                Assert.NotEmpty(context.Users);
                logic.DeleteUser(user.UserID); // fails for some reason when I add await
                //Assert.Empty(context.Users);
                Assert.Equal(15, context.Users.CountAsync().Result); // using this cause there are 15 normally. +1 -1 = 15.

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
                    UserID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1
                };

                r.users.Add(user);
                await r.CommitSave();
                await logic.AddUserRole(user.UserID, 1);
                Assert.Equal(1, context.Users.Find(user.UserID).RoleID);
            }
        }

        /// <summary>
        /// Tests the EditUser() method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForEditUser()
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
                    UserID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };

                r.users.Add(user);
                await r.CommitSave();

                var user2 = new EditUserDto()
                {
                    Password = "jerryrice",
                    FullName = "Tom Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com"
                };

                var editedUser = logic.EditUser(user.UserID, user2);
                Assert.Equal(editedUser.Result.FullName, context.Users.Find(user.UserID).FullName);
            }
        }

        /// <summary>
        /// Tests the CoachEditUser() method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForCoachEditUser()
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
                    UserID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };

                r.users.Add(user);
                await r.CommitSave();

                var user2 = new CoachEditUserDto
                {
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 2
                };

                var editedUser = logic.CoachEditUser(user.UserID, user2);
                Assert.Equal(editedUser.Result.RoleID, context.Users.Find(user.UserID).RoleID);
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
                    UserID = Guid.NewGuid(),
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
                    UserID = Guid.NewGuid(),
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };

                r.users.Add(user);
                var listOfUsers = logic.GetUserById(user.UserID);
                Assert.True(listOfUsers.Result.Equals(user));
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
                    TeamID = 4, // 4 for seeding
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
                    TeamID = 5, // 5 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = logic.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Result.Equals(team));
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
                    RoleID = 4, // 4 because of seeding
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
                    RoleID = 5, // 5 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = logic.GetRoleById(role.RoleID);
                Assert.True(listOfRoles.Result.Equals(role));
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
                    PlaybookID = 1,
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
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = logic.GetPlaybookById(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Result.Equals(playbook));
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
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
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
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = logic.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Result.Equals(play));
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
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
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
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = logic.GetMessageById(message.MessageID);
                Assert.True(listOfMessages.Result.Equals(message));
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
                    GameID = 1,
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
                    GameID = 1,
                    HomeTeamID = 1,
                    AwayTeamID = 2,
                    WinningTeam = 1,
                    HomeScore = 24,
                    AwayScore = 12
                };

                r.games.Add(game);
                var listOfGames = logic.GetGameById(game.GameID);
                Assert.True(listOfGames.Result.Equals(game));
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
                    EventID = 1,
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
                    EventID = 1,
                    TeamID = 1,
                    Description = "Practice",
                    EventDate = DateTime.Now,
                    Location = "soccer field",
                    Message = "make it to practice!"
                };

                r.events.Add(eventSchedule);
                var listOfEvents = logic.GetEventById(eventSchedule.EventID);
                Assert.True(listOfEvents.Result.Equals(eventSchedule));
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
                    RequestID = 4, // 4 for seeding
                    UserID = Guid.NewGuid(),
                    TeamID = 1,
                    RequestDate = DateTime.Now,
                    Message = "shoulder pads",
                    ItemId = 1,
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
                    RequestID = 3, // 3 for seeding
                    UserID = Guid.NewGuid(),
                    TeamID = 1,
                    RequestDate = DateTime.Now,
                    Message = "shoulder pads",
                    ItemId = 1,
                    Status = "pending"
                };

                r.equipmentRequests.Add(equipment);
                var listOfEquipment = logic.GetEquipmentRequestById(equipment.RequestID);
                Assert.True(listOfEquipment.Result.Equals(equipment));
            }
        }

    } // end of class
} // end of namespace
