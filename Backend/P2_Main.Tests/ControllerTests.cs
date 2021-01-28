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

namespace P2_Main.Tests
{
    public class ControllerTests
    {

        private readonly Mapper _mapper;
        private readonly ITokenService _token;
        /*private readonly ILogger<Repo> _repoLogger;
        private readonly ILogger<UsersController> _userLogger;
        private readonly ILogger<GamesController> _gamesLogger;
        private readonly ILogger<PlaybooksController> _playbooksLogger;
        private readonly ILogger<EquipmentController> _equipmentLogger;
        private readonly ILogger<TeamsController> _teamsLogger;*/

        //----------------------UserContollerTests----------------------------------

        /// <summary>
        /// Test for the GetUsers() method of UsersController
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
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
                var listOfUsers = userController.GetUsers();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// Test for the CreateUser() method of UsersController
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
                var user = new CreateUserDto()
                {
                    UserName = "jerry",
                    Password = "jerryrice",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com",
                    TeamID = 1,
                    RoleID = 1
                };

                var listOfUsers = userController.CreateUser(user);
                //Assert.NotEmpty(context.Users);
                Assert.Contains<User>(listOfUsers.Result.Value, context.Users);

                var user2 = logic.CreateUser(user);
                //Assert.Equal(1, context.Users.CountAsync().Result);
                var countUsers = from u in context.Users
                                 where u.Email == user.Email
                                 select u;
                int count = 0;
                foreach (User userMail in countUsers)
                {
                    count++;
                }
                Assert.Equal(1, count);
            }
        }

        /// <summary>
        /// Tests the GetUser() method of UserController
        /// </summary>
        /// 
        //THIS NOW RETURNS A UserDto not a User - Daniel
       [Fact]
        public void TestForGetUser()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
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
                //context.SaveChanges();
                var listOfUsers = userController.GetUser(user.UserID);
                var convertUser = Mapper.ConvertUserToUserDto(user);
                //Assert.True(listOfUsers.Result.Value.Equals(convertUser));
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of UserController
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
                var role = new Role
                {
                    RoleID = 4, // 4 because of seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = userController.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRole() method of UserController
        /// </summary>
        [Fact]
        public void TestForGetRole()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
                var role = new Role
                {
                    RoleID = 5, // 5 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = userController.GetRole(role.RoleID);
                Assert.True(listOfRoles.Result.Value.Equals(role));
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
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

                var editedUser = userController.EditUser(user.UserID, user2);
                Assert.Equal(editedUser.Result.Value.FullName, context.Users.Find(user.UserID).FullName);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
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

                var editedUser = userController.CoachEditUser(user.UserID, user2);
                Assert.Equal(editedUser.Result.Value.RoleID, context.Users.Find(user.UserID).RoleID);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                UsersController userController = new UsersController(logic, _mapper, new NullLogger<UsersController>());
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
                userController.DeleteUser(Guid.NewGuid());
                //Assert.NotEmpty(context.Users);
                Assert.Contains<User>(user, context.Users);
                userController.DeleteUser(user.UserID);
                //Assert.Equal(0, context.Users.CountAsync().Result);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfGames = gamesController.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGame() method of GamesController
        /// </summary>
        [Fact]
        public void TestForGetGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfGames = gamesController.GetGame(game.GameID);
                Assert.True(listOfGames.Result.Value.Equals(game));
            }
        }

        /// <summary>
        /// Tests the CreateGame() method of GamesController
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public void TestForCreateGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                GamesController gamesController = new GamesController(logic, new NullLogger<GamesController>());
                CreateGameDto game = new CreateGameDto()
                {
                    HomeTeamID = 1,
                    AwayTeamID = 2
                };
                var createGame = gamesController.CreateGame(game);
                //Assert.Equal(1, context.Games.CountAsync().Result);
                Assert.Contains<Game>(createGame.Result.Value, context.Games);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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

                var editedGame = gamesController.EditGame(game.GameID, game2);
                Assert.Equal(editedGame.Result.Value.HomeScore, context.Games.Find(game.GameID).HomeScore);
            }
        }
        //------------------End of GamesController Tests----------------------------

        //---------------Start of PlaybooksController Tests-------------------------

        /// <summary>
        /// Tests the GetPlaybooks() method of PlaybooksController 
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = playbooksController.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybook() method of PlaybooksController
        /// </summary>
        [Fact]
        public void TestForGetPlaybook()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = playbooksController.GetPlaybook(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Result.Value.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the GetPlays() method of PlaybookController
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfPlays = playbooksController.GetPlays();
                Assert.NotNull(listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetPlay() method of PlaybookController
        /// </summary>
        [Fact]
        public void TestForGetPlay()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfPlays = playbooksController.GetPlay(play.PlayID);
                Assert.True(listOfPlays.Result.Value.Equals(play));
            }
        }

        /// <summary>
        /// Tests the CreatePlaybook() method of PlaybookController
        /// Tests that a playbook is added to the database
        /// </summary>
        [Fact]
        public void TestForCreatePlaybook()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                Team team = new Team()
                {
                    TeamID = 1,
                    Name = "Broncoes",
                    Wins = 2,
                    Losses = 0
                };
                var createPlaybook = playbooksController.CreatePlaybook(team.TeamID);

                //Assert.Equal(1, context.Playbooks.CountAsync().Result);
                Assert.Contains<Playbook>(createPlaybook.Result.Value, context.Playbooks);
            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of PlaybookController
        /// Tests that a play is added to the database
        /// </summary>
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                PlayDto play = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    ImageString = "football, football, football"
                };
                var createPlay = await playbooksController.CreatePlay(play);
                //Assert.Equal(1, context.Plays.CountAsync().Result);
                Assert.Contains<Play>(createPlay.Value, context.Plays);
            }
        }

        /// <summary>
        /// Tests the EditPlays() method of PlaybookController
        /// </summary>
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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

                var editedPlay = playbooksController.EditPlay(play.PlayID, play2);
                Assert.Equal(editedPlay.Result.Value.Description, context.Plays.Find(play.PlayID).Description);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                playbooksController.DeletePlay(3); // fails for some reason when I add await
                //Assert.NotEmpty(context.Plays);
                Assert.Contains<Play>(play, context.Plays);
                playbooksController.DeletePlay(play.PlayID); // fails for some reason when I add await
                //Assert.Equal(0, context.Plays.CountAsync().Result);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                PlaybooksController playbooksController = new PlaybooksController(logic, new NullLogger<PlaybooksController>());
                var playbook = new Playbook()
                {
                    PlaybookID = 1,
                    TeamID = 1
                };
                r.playbooks.Add(playbook);
                await r.CommitSave();
                playbooksController.DeletePlaybook(3); // fails for some reason when I add await
                //Assert.NotEmpty(context.Playbooks);
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                playbooksController.DeletePlaybook(playbook.PlaybookID); // fails for some reason when I add await
                //Assert.Equal(0, context.Playbooks.CountAsync().Result);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfEquipment = equipmentController.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequest() method of EquipmentController
        /// </summary>
        [Fact]
        public void TestForGetEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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
                var listOfEquipment = equipmentController.GetEquipmentRequest(equipment.RequestID);
                Assert.True(listOfEquipment.Result.Value.Equals(equipment));
            }
        }

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of EquipmentController
        /// Tests that an equipment request is added to the database
        /// </summary>
        [Fact]
        public void TestForCreateEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                EquipmentController equipmentController = new EquipmentController(logic, new NullLogger<EquipmentController>());
                CreateEquipmentRequestDto equipmentRequest = new CreateEquipmentRequestDto()
                {
                    TeamID = 1,
                    UserID = Guid.NewGuid(),
                    ItemID = 2,
                    RequestDate = DateTime.Now,
                    Message = "Need this equipment",
                    Status = "Pending"
                };
                var createEquipmentRequest = equipmentController.CreateEquipmentRequest(equipmentRequest);
                //Assert.Equal(1, context.EquipmentRequests.CountAsync().Result);
                Assert.Contains<EquipmentRequest>(createEquipmentRequest.Result.Value, context.EquipmentRequests);
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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

                var editedEquipmentRequest = equipmentController.EditEquipmentRequest(equipmentRequest.RequestID, equipmentRequest2);
                Assert.Equal(editedEquipmentRequest.Result.Value.Status, context.EquipmentRequests.Find(equipmentRequest.RequestID).Status);
            }
        }
        //-----------------End of EquipmentController Tests-------------------------

        //------------------Start of TeamsController Tests--------------------------

        /// <summary>
        /// Tests the GetTeams() method of TeamsController
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                TeamsController teamsController = new TeamsController(logic, new NullLogger<TeamsController>());
                var team = new Team
                {
                    TeamID = 4, // 4 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = teamsController.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeam() method of TeamsController
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
                TeamsController teamsController = new TeamsController(logic, new NullLogger<TeamsController>());
                var team = new Team
                {
                    TeamID = 5, // 5 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = teamsController.GetTeam(team.TeamID);
                Assert.True(listOfTeams.Result.Value.Equals(team));
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
                LogicClass logic = new LogicClass(r, _mapper, _token, new NullLogger<Repo>());
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

                var editedTeam = teamsController.EditTeam(team.TeamID, team2);
                Assert.Equal(editedTeam.Result.Value.Name, context.Teams.Find(team.TeamID).Name);
            }
        }
        //-------------------End of TeamsController Tests---------------------------

        //-----------------Start of AccountController Tests-------------------------


        //------------------End of AccountController Tests--------------------------
    }
}
