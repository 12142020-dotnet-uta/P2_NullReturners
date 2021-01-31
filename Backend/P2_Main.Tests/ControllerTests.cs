using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Logic;
using Models.DataTransfer;
using System;
using Xunit;
using P2_Main.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using Logic.Interfaces;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;

namespace P2_Main.Tests
{
    public class ControllerTests
    {
        // needs to be instantiated
        private readonly ITokenService _token;

        //----------------------UserContollerTests----------------------------------

        /// <summary>
        /// Test for the GetUsers() method of UsersController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
                var user = new User()
                {
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };

                r.users.Add(user);
                var listOfUsers = await userController.GetUsers();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Test for the CreateUser() method of UsersController
        /// </summary>
        //[Fact]
        //public async void TestForCreateUser()
        //{
        //    var options = new DbContextOptionsBuilder<ProgContext>()
        //    .UseInMemoryDatabase(databaseName: "p2newsetuptest")
        //    .Options;

        //    using (var context = new ProgContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Mapper mapper = new Mapper();
        //        LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
        //        UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
        //        var user = new CreateUserDto()
        //        {
        //            UserName = "jerry",
        //            Password = "jerryrice",
        //            FullName = "Jerry Rice",
        //            PhoneNumber = "111-111-1111",
        //            Email = "jerryrice@gmail.com",
        //            TeamID = 1,
        //            RoleID = 1
        //        };

        //        var listOfUsers = await userController.CreateUser(user);
        //        //Assert.NotEmpty(context.Users);
        //        Assert.Contains<User>(listOfUsers.Value, context.Users);

        //        var user2 = await logic.CreateUser(user);
        //        //Assert.Equal(1, context.Users.CountAsync().Result);
        //        var countUsers = from u in context.Users
        //                         where u.Email == user.Email
        //                         select u;
        //        int count = 0;
        //        foreach (User userMail in countUsers)
        //        {
        //            count++;
        //        }
        //        Assert.Equal(1, count);
        //    }
        //}

        /// <summary>
        /// Tests the GetUser() method of UserController
        /// </summary>
        /// 
        [Fact]
        public async void TestForGetUser()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());

                var user = new User()
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
                var getUser = await userController.GetUser(user.UserID);
                Assert.True(getUser.Value.UserID.Equals(mapper.ConvertUserToUserDto(user).UserID));
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of UserController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
                var role = new Role
                {
                    RoleID = 4, // 4 because of seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = await userController.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRole() method of UserController
        /// </summary>
        [Fact]
        public async void TestForGetRole()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
                var role = new Role
                {
                    RoleID = 5, // 5 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = await userController.GetRole(role.RoleID);
                Assert.True(listOfRoles.Value.Equals(role));
                //Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the EditUser() method of UserContoller
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
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

                var editedUser = await userController.EditUser(user.UserID, user2);
                Assert.Equal(editedUser.Value.FullName, context.Users.Find(user.UserID).FullName);
            }
        }

        /// <summary>
        /// Tests the CoachEditUser() method of UserContoller
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
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

                var editedUser = await userController.CoachEditUser(user.UserID, user2);
                Assert.Equal(editedUser.Value.RoleID, context.Users.Find(user.UserID).RoleID);
            }
        }

        /// <summary>
        /// Tests the DeleteUser() method of the UserContoller
        /// Tests that a user is removed from the database
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, mapper, new NullLogger<UsersController>());
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
                await userController.DeleteUser(Guid.NewGuid());
                Assert.Contains<User>(user, context.Users);
                await userController.DeleteUser(user.UserID);
                var countUsers = from u in context.Users
                                 where u.Email == user.Email
                                 select u;
                int count = 0;
                foreach (User userMail in countUsers)
                {
                    count++;
                }
                Assert.Equal(0, count);
            }
        }

        //------------------End of UserController Tests-----------------------------

        //----------------Start of GamesController Tests----------------------------

        /// <summary>
        /// Tests the GetGames() method of GamesController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                GamesController gamesController = new GamesController(logic, new NullLogger<GamesController>());
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
                var listOfGames = await gamesController.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGame() method of GamesController
        /// </summary>
        [Fact]
        public async void TestForGetGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                GamesController gamesController = new GamesController(logic, new NullLogger<GamesController>());
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
                var listOfGames = await gamesController.GetGame(game.GameID);
                Assert.True(listOfGames.Value.Equals(game));
            }
        }

        /// <summary>
        /// Tests the CreateGame() method of GamesController
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreateGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                GamesController gamesController = new GamesController(logic, new NullLogger<GamesController>());
                CreateGameDto game = new CreateGameDto()
                {
                    HomeTeamID = 1,
                    AwayTeamID = 2
                };
                var createGame = await gamesController.CreateGame(game);
                Assert.Contains<Game>(createGame.Value, context.Games);
            }
        }

        /// <summary>
        /// Tests the EditGame() method of GamesController
        /// </summary>
        [Fact]
        public async void TestForEditGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                GamesController gamesController = new GamesController(logic, new NullLogger<GamesController>());
                var game = new Game()
                {
                    GameID = 1,
                    HomeTeamID = 1,
                    AwayTeamID = 1,
                    WinningTeam = 1,
                    HomeScore = 20,
                    AwayScore = 8
                };

                r.games.Add(game);
                await r.CommitSave();

                var game2 = new EditGameDto()
                {
                    WinningTeamID = 2,
                    HomeScore = 12,
                    AwayScore = 24
                };

                var editedGame = await gamesController.EditGame(game.GameID, game2);
                Assert.Equal(editedGame.Value.HomeScore, context.Games.Find(game.GameID).HomeScore);
            }
        }
        //------------------End of GamesController Tests----------------------------

        //---------------Start of PlaybooksController Tests-------------------------

        /// <summary>
        /// Tests the GetPlaybooks() method of PlaybooksController 
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = await playbooksController.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybook() method of PlaybooksController
        /// </summary>
        [Fact]
        public async void TestForGetPlaybook()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = await playbooksController.GetPlaybook(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Value.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of PlaybookController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = await playbooksController.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetPlayDto() method of PlaybookController
        /// </summary>
        /// 
        [Fact]
        public async void TestForGetPlayDto()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                await r.CommitSave();
                var playDto = await playbooksController.GetPlayDto(play.PlayID);
                Assert.True(playDto.Value.Name == mapper.ConvertToPlayDto(play).Name);
            }
        }

        /// <summary>
        /// Tests the CreatePlaybook() method of PlaybookController
        /// Tests that a playbook is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreatePlaybook()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                Team team = new Team()
                {
                    TeamID = 1,
                    Name = "Broncoes",
                    Wins = 2,
                    Losses = 0
                };
                var createPlaybook = await playbooksController.CreatePlaybook(team.TeamID);
                Assert.Contains<Playbook>(createPlaybook.Value, context.Playbooks);
            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of PlaybookController
        /// Tests that a play is added to the database
        /// </summary>
        /// 
        [Fact]
        public async void TestForCreatePlay()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                PlayDto play = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    ImageString = "football, football, football"
                };
                var createPlay = await playbooksController.CreatePlay(play);
                Assert.Contains<Play>(createPlay.Value, context.Plays);
            }
        }


        /// <summary>
        /// Tests the EditPlays() method of PlaybookController
        /// </summary>
        /// 
        [Fact]
        public async void TestForEditPlay()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var play = new Play()
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle the player",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                await r.CommitSave();

                var play2 = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle the quarterback",
                    DrawnPlay = new byte[1]
                };

                var editedPlay = await playbooksController.EditPlay(play.PlayID, play2);
                Assert.Equal(editedPlay.Value.Description, context.Plays.Find(play.PlayID).Description);
            }
        }

        /// <summary>
        /// Tests the DeletePlay() method of the PlaybookController
        /// Tests that a play is removed from the database
        /// </summary>
        [Fact]
        public async void TestForDeletePlay()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var play = new Play()
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Run",
                    Description = "Run to endzone",
                    DrawnPlay = new byte[1]
                };
                r.plays.Add(play);
                await r.CommitSave();
                await playbooksController.DeletePlay(3); 
                Assert.Contains<Play>(play, context.Plays);
                await playbooksController.DeletePlay(play.PlayID);
                var countPlays = from p in context.Plays
                                 where p.Name == play.Name
                                 select p;
                int count = 0;
                foreach (Play plays in countPlays)
                {
                    count++;
                }
                Assert.Equal(0, count);
            }
        }

        /// <summary>
        /// Tests the DeletePlaybook() method of the PlaybookController
        /// Tests that a playbook is removed from the database
        /// </summary>
        [Fact]
        public async void TestForDeletePlaybook()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook()
                {
                    PlaybookID = 1,
                    TeamID = 1
                };
                r.playbooks.Add(playbook);
                await r.CommitSave();
                await playbooksController.DeletePlaybook(3);
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                await playbooksController.DeletePlaybook(playbook.PlaybookID);
                var countPlaybooks = from p in context.Playbooks
                                     where p.PlaybookID == playbook.PlaybookID
                                     select p;
                int count = 0;
                foreach (Playbook playbooks in countPlaybooks)
                {
                    count++;
                }
                Assert.Equal(0, count);
            }
        }
        //-----------------End of PlaybooksController Tests-------------------------

        //----------------Start of EquipmentController Tests------------------------

        /// <summary>
        /// Tests the GetEquipmentRequests() method of EquipmentController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
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
                var listOfEquipment = await equipmentController.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequest() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
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
                var listOfEquipment = await equipmentController.GetEquipmentRequest(equipment.RequestID);
                Assert.True(listOfEquipment.Value.Equals(equipment));
            }
        }

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of EquipmentController
        /// Tests that an equipment request is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreateEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
                CreateEquipmentRequestDto equipmentRequest = new CreateEquipmentRequestDto()
                {
                    TeamID = 1,
                    UserID = Guid.NewGuid(),
                    ItemID = 2,
                    Message = "Need this equipment",
                    Status = "Pending"
                };
                var createEquipmentRequest = await equipmentController.CreateEquipmentRequest(equipmentRequest);
                Assert.Contains<EquipmentRequest>(createEquipmentRequest.Value, context.EquipmentRequests);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of EquipmentController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
                var equipment = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipment);
                var listOfEquipment = await equipmentController.GetEquipmentItems();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemById() method of EquipmentController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
                var equipment = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipment);
                var listOfEquipment = await equipmentController.GetEquipmentItemById(equipment.EquipmentID);
                Assert.True(listOfEquipment.Value.Equals(equipment));
            }
        }

        /// <summary>
        /// Tests the EditEquipmentRequest method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForEditEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
                var equipmentRequest = new EquipmentRequest()
                {
                    TeamID = 1,
                    UserID = Guid.NewGuid(),
                    ItemId = 1,
                    RequestID = 1,
                    RequestDate = DateTime.Now,
                    Message = "Need this equipment",
                    Status = "Pending"
                };

                r.equipmentRequests.Add(equipmentRequest);
                await r.CommitSave();

                var equipmentRequest2 = new EditEquipmentRequestDto()
                {
                    Status = "Accepted"
                };

                var editedEquipmentRequest = await equipmentController.EditEquipmentRequest(equipmentRequest.RequestID, equipmentRequest2);
                Assert.Equal(editedEquipmentRequest.Value.Status, context.EquipmentRequests.Find(equipmentRequest.RequestID).Status);
            }
        }
        //-----------------End of EquipmentController Tests-------------------------

        //------------------Start of TeamsController Tests--------------------------

        /// <summary>
        /// Tests the GetTeams() method of TeamsController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                TeamsController teamsController = new TeamsController(logic, new NullLogger<TeamsController>());
                var team = new Team
                {
                    TeamID = 4, // 4 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = await teamsController.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeam() method of TeamsController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                TeamsController teamsController = new TeamsController(logic, new NullLogger<TeamsController>());
                var team = new Team
                {
                    TeamID = 5, // 5 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = await teamsController.GetTeam(team.TeamID);
                Assert.True(listOfTeams.Value.Equals(team));
            }
        }

        /// <summary>
        /// Tests the EditTeam method of TeamsController
        /// </summary>
        [Fact]
        public async void TestForEditTeam()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                TeamsController teamsController = new TeamsController(logic, new NullLogger<TeamsController>());
                var team = new Team()
                {
                    TeamID = 1,
                    Name = "Dirty Donkies",
                    Wins = 0,
                    Losses = 5
                };

                r.teams.Add(team);
                await r.CommitSave();

                var team2 = new EditTeamDto()
                {
                    Name = "Bad Broncoes",
                    Wins = 0,
                    Losses = 5
                };

                var editedTeam = await teamsController.EditTeam(team.TeamID, team2);
                Assert.Equal(editedTeam.Value.Name, context.Teams.Find(team.TeamID).Name);
            }
        }
        //-------------------End of TeamsController Tests---------------------------

        //-----------------Start of AccountController Tests-------------------------

        /// <summary>
        /// Tests the Register() method of AccountController
        /// </summary>
        /// 
        [Fact]
        public async void TestForRegister()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                AccountController accountController = new AccountController(logic, mapper, new NullLogger<AccountController>());

                var user = new User()
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

                var createUserDto = new CreateUserDto()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    TeamID = user.TeamID,
                    RoleID = user.RoleID
                };
                
                //var userExists = await accountController.GetUserById(user.UserID);
                //Assert.True(getUser.Value.UserID.Equals(mapper.ConvertUserToUserDto(user).UserID));
            }
        }

        //------------------End of AccountController Tests--------------------------

        //-----------------Start of MessagesController Tests------------------------

        /// <summary>
        /// Test for the GetMessages() method of MessagesController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                MessagesController messagesController = new MessagesController(logic, new NullLogger<UsersController>());
                var message = new Message()
                {
                    MessageID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    SentDate = DateTime.Now,
                    MessageText = "How you doin'?"
                };

                r.messages.Add(message);
                await r.CommitSave();
                var listOfMessages = await messagesController.GetMessages();
                Assert.NotNull(listOfMessages);
            }
        }

        /// <summary>
        /// Test for the SendMessage() method of MessagesController
        /// </summary>
        [Fact]
        public async void TestForSendMessage()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                MessagesController messagesController = new MessagesController(logic, new NullLogger<UsersController>());
                List<Guid> listOfGuids = new List<Guid>();
                var messageDto = new NewMessageDto()
                {
                    SenderID = Guid.NewGuid(),
                    RecipientList = listOfGuids,
                    MessageText = "What's up?!"
                };

                var sendMessage = await messagesController.SendMessage(messageDto);

                Assert.True(sendMessage.Value.SenderID.Equals(messageDto.SenderID));
            }
        }

        /// <summary>
        /// Test for the GetRecipientList() method of MessagesController
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                MessagesController messagesController = new MessagesController(logic, new NullLogger<UsersController>());
                var recipientList = new RecipientList()
                {
                    RecipientID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                await r.CommitSave();
                var recipientList1 = await messagesController.GetRecipientList(recipientList.RecipientListID);
                Assert.NotNull(recipientList1);
            }
        }

        /// <summary>
        /// Test for the GetRecipientList() method of MessagesController
        /// </summary>
        [Fact]
        public async void TestForGetRecipientList()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                MessagesController messagesController = new MessagesController(logic, new NullLogger<UsersController>());
                var recipientList = new RecipientList()
                {
                    RecipientID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                await r.CommitSave();
                var recipientLists = await messagesController.GetRecipientLists();
                Assert.NotNull(recipientLists);
            }
        }
        //------------------End of MessagesController Tests-------------------------

        //------------------Start of CalendarController Tests-----------------------

        /// <summary>
        /// Test for the GetCalendar() method of CalendarController
        /// </summary>
        [Fact]
        public async void TestForGetCalendar()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                CalendarController calendarController = new CalendarController(new NullLogger<CalendarController>());

                await calendarController.GetCalendar();
            }
        }

        /// <summary>
        /// Test for the GetMyEvents() method of CalendarController
        /// </summary>
        [Fact]
        public async void TestForMyEvents()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                CalendarController calendarController = new CalendarController(new NullLogger<CalendarController>());

                await calendarController.GetMyEvents();
            }
        }

        /// <summary>
        /// Test for the CreateEvent() method of CalendarController
        /// </summary>
        [Fact]
        public async void TestForCreateEvent()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                CalendarController calendarController = new CalendarController(new NullLogger<CalendarController>());
                var createEvent = new EventDto()
                {
                    Description = "Practice",
                    Location = "Football field",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Message = "Don't miss it!!"
                };

                await calendarController.CreateEvent(createEvent);
            }
        }

        //------------------End of CalendarController Tests-------------------------
    }
}
