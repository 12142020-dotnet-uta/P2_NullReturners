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
        public async void TestForGetUsers()
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
                var listOfUsers = await r.GetUsers();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Tests the GetUserById method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetUserById()
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
                var searchForUser = await r.GetUserById(user.UserID);
                Assert.True(searchForUser.Equals(user));
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeams()
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
                var listOfTeams = await r.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamById()
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
                var listOfTeams = await r.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Equals(team));
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetRoles()
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
                var listOfRoles = await r.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRoleById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetRoleById()
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
                var listOfRoles = await r.GetRoleById(role.RoleID);
                Assert.True(listOfRoles.Equals(role));
            }
        }

        /// <summary>
        /// Tests the GetPlaybooks() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlaybooks()
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
                var listOfPlaybooks = await r.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlaybookById()
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
                var listOfPlaybooks = await r.GetPlaybookById(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlays()
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
                var listOfPlays = await r.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }


        /// <summary>
        /// Tests the GetPlayById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPlayById()
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
                var listOfPlays = await r.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Equals(play));
            }
        }

        /// <summary>
        /// Tests the GetMessages() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetMessages()
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
                var listOfMessages = await r.GetMessages();
                Assert.NotNull(listOfMessages);
            }
        }


        /// <summary>
        /// Tests the GetMessageByid() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetMessageById()
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
                var listOfMessages = await r.GetMessageById(message.MessageID);
                Assert.True(listOfMessages.Equals(message));
            }
        }

        /// <summary>
        /// Tests the GetRecipientLists() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetRecipientLists()
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
                var listOfRecipientLists = await r.GetRecipientLists();
                Assert.NotNull(listOfRecipientLists);
            }
        }

        /// <summary>
        /// Tests the GetRecipientListById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetRecipientListById()
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
                var listOfRecipientList = await r.GetRecipientListById(recipientList.RecipientListID, recipientList.RecipientID);
                Assert.True(listOfRecipientList.Equals(recipientList));
            }
        }

        /// <summary>
        /// Tests the GetGames() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetGames()
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
                var listOfGames = await r.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGameByid() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetGameById()
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
                var listOfGames = await r.GetGameById(game.GameID);
                Assert.True(listOfGames.Equals(game));
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequests() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequests()
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
                var listOfEquipment = await r.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequestById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequestById()
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
                var listOfEquipment = await r.GetEquipmentRequestById(equipment.RequestID);
                Assert.True(listOfEquipment.Equals(equipment));
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItems()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var equipmentItem = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipmentItem);
                var listOfEquipment = await r.GetEquipmentItems();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemById()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var equipmentItem = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipmentItem);
                var listOfEquipment = await r.GetEquipmentItemById(equipmentItem.EquipmentID);
                Assert.True(listOfEquipment.Equals(equipmentItem));
            }
        }

        /// <summary>
        /// Tests the GetUserInbox() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetUserInbox()
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
                var listOfUserInboxes = await r.GetUserInbox(userInbox.UserID);
                Assert.NotNull(listOfUserInboxes);
            }
        }

    } // end of class
} // end of namespace
