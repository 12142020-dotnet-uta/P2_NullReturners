using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Repository;
using Microsoft.Extensions.Logging;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

namespace Repository.Tests
{
    public class RepoTests
    {

        //private readonly ILogger<Repo> _logger;

        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                Assert.NotEmpty(context.Users);
            }
        }

        /// <summary>
        /// Tests the GetUsers() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                var listOfUsers = r.GetUsers();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Tests the GetUserById method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                User user = new User
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
                var searchForUser = r.GetUserById(user.UserID);
                Assert.True(searchForUser.Result.Equals(user));
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = 4, // 4 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = r.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var team = new Team
                {
                    TeamID = 4, // 4 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = r.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Result.Equals(team));
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var role = new Role
                {
                    RoleID = 4, // 4 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = r.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRoleById() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var role = new Role
                {
                    RoleID = 4, // 4 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = r.GetRoleById(role.RoleID);
                Assert.True(listOfRoles.Result.Equals(role));
            }
        }

        /// <summary>
        /// Tests the GetPlaybooks() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = r.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookById() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = r.GetPlaybookById(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Result.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = r.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }


        /// <summary>
        /// Tests the GetPlayById() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = r.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Result.Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetMessages() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var message = new Message
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = r.GetMessages();
                Assert.NotNull(listOfMessages);
            }
        }


        /// <summary>
        /// Tests the GetMessageByid() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                var message = new Message
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = r.GetMessageById(message.MessageID);
                Assert.True(listOfMessages.Result.Equals(message));
            }
        }

        /// <summary>
        /// Tests the GetRecipientLists() method of Repo
        /// </summary>
        [Fact]
        public void TestForGetRecipientLists()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                var listOfRecipientLists = r.GetRecipientLists();
                Assert.NotNull(listOfRecipientLists);
            }
        }

        /// <summary>
        /// Tests the GetRecipientListById() method of Repo
        /// </summary>
        [Fact]
        public void TestForGetRecipientListById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid(),
                };

                r.recipientLists.Add(recipientList);
                var listOfRecipientList = r.GetRecipientListById(recipientList.RecipientListID);
                Assert.True(listOfRecipientList.Result.Equals(recipientList));
            }
        }

        /// <summary>
        /// Tests the GetGames() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                var listOfGames = r.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGameByid() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                var listOfGames = r.GetGameById(game.GameID);
                Assert.True(listOfGames.Result.Equals(game));
            }
        }

        /// <summary>
        /// Tests the GetEvents() method of Repo
        /// </summary>
        //[Fact]
        //public void TestForGetEvents()
        //{
        //    var options = new DbContextOptionsBuilder<ProgContext>()
        //    .UseInMemoryDatabase(databaseName: "p2newsetuptest")
        //    .Options;

        //    using (var context = new ProgContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        var eventSchedule = new Event
        //        {
        //            EventID = 1,
        //            TeamID = 1,
        //            Description = "Practice",
        //            EventDate = DateTime.Now,
        //            Location = "soccer field",
        //            Message = "make it to practice!"
        //        };

        //        r.events.Add(eventSchedule);
        //        var listOfEvents = r.GetEvents();
        //        Assert.NotNull(listOfEvents);
        //    }
        //}

        /// <summary>
        /// Tests the GetEventById() method of Repo
        /// </summary>
        //[Fact]
        //public void TestForGetEventById()
        //{
        //    var options = new DbContextOptionsBuilder<ProgContext>()
        //    .UseInMemoryDatabase(databaseName: "p2newsetuptest")
        //    .Options;

        //    using (var context = new ProgContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        var eventSchedule = new Event
        //        {
        //            EventID = 1,
        //            TeamID = 1,
        //            Description = "Practice",
        //            EventDate = DateTime.Now,
        //            Location = "soccer field",
        //            Message = "make it to practice!"
        //        };

        //        r.events.Add(eventSchedule);
        //        var listOfEvents = r.GetEventById(eventSchedule.EventID);
        //        Assert.True(listOfEvents.Result.Equals(eventSchedule));
        //    }
        //}

        /// <summary>
        /// Tests the GetEquipmentRequests() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                var listOfEquipment = r.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequestById() method of Repo
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

                Repo r = new Repo(context, new NullLogger<Repo>());
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
                var listOfEquipment = r.GetEquipmentRequestById(equipment.RequestID);
                Assert.True(listOfEquipment.Result.Equals(equipment));
            }
        }

        /*/// <summary>
        /// Tests the GetUserInbox() method of Repo
        /// </summary>
        [Fact]
        public void TestForGetUserInbox()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var userInbox = new UserInbox()
                {
                    UserID = Guid.NewGuid(),
                    MessageID = Guid.NewGuid(),
                    IsRead = true
                };

                r.userInboxes.Add(userInbox);
                var listOfUserInboxes = r.GetUserInbox();
                Assert.NotNull(listOfUserInboxes);
            }
        }

        /// <summary>
        /// Tests the GetUserInboxById() method of Repo
        /// </summary>
        [Fact]
        public void TestForGetUserInboxById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var userInbox = new UserInbox()
                {
                    UserID = Guid.NewGuid(),
                    MessageID = Guid.NewGuid(),
                    IsRead = true
                };

                r.userInboxes.Add(userInbox);
                var listOfUserInboxes = r.GetUserInboxById(userInbox.UserID, userInbox.MessageID);
                Assert.True(listOfUserInboxes.Result.Equals(userInbox));
            }
        }*/

    } // end of class
} // end of namespace
