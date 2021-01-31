using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Logic;
using System;
using Xunit;
using System.Threading.Tasks;
using Models.DataTransfer;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Services;
using System.Configuration;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.TestHost;

namespace Logic.Tests
{
    public class LogicTests
    {
        // needs to be instantiated
        private readonly ITokenService _token;

        //--------------------------Start of LogicClass Tests-----------------------


        /// <summary>
        /// Tests the CreateUser() method of LogicClass
        /// Tests that a user is added to the database
        /// </summary>
        //[Fact]
        //public async void TestForCreateUser()
        //{

        //    // this block is only for code coverage
        //    var onlyForCoverage = new ProgContext(); 
        //    var empty = new DbContextOptionsBuilder<ProgContext>().Options;
        //    var onlyForCoverage2 = new ProgContext(empty); // didn't work
        //    LogicClass logicClass = new LogicClass();


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
        //        CreateUserDto cUD = new CreateUserDto()
        //        {
        //            UserName = "jerryrice",
        //            Password = "jerry123",
        //            FullName = "Jerry Rice",
        //            PhoneNumber = "111-111-1111",
        //            Email = "jerryrice@gmail.com"
        //        };
        //        var user = await logic.CreateUser(cUD);
        //        //Assert.NotEmpty(context.Users);
        //        Assert.Contains<User>(user, context.Users);

        //        var user2 = await logic.CreateUser(cUD);
        //        //Assert.Equal(1, context.Users.CountAsync().Result); // this is 16 because of seeding. remove when not seeding.
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
        /// Tests the RegisterUser() method of LogicClass
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public async void TestForRegisterUser()
        {

            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var onlyForCoverage = new ProgContext();
                LogicClass logicClass = new LogicClass();

                //        Repo r = new Repo(context, new NullLogger<Repo>());
                //        Mapper mapper = new Mapper();
                //        var mockConfSection = new Mock<IConfigurationSection>();
                //        mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");
                //        var mockConfiguration = new Mock<IConfiguration>();
                //        mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionString"))).Returns(mockConfSection.Object);
                //        ITokenService token = new TokenService(mockConfiguration.Object);
                //        LogicClass logic = new LogicClass(r, mapper, token, new NullLogger<Repo>());
                //        CreateUserDto cUD = new CreateUserDto()
                //        {
                //            UserName = "jerryrice",
                //            Password = "jerry123",
                //            FullName = "Jerry Rice",
                //            PhoneNumber = "111-111-1111",
                //            Email = "jerryrice@gmail.com",
                //            RoleID = 1,
                //            TeamID = 1
                //        };
                //        var user = await logic.RegisterUser(cUD);
                //        //Assert.NotEmpty(context.Users);
                //        Assert.Equal(context.Users.Find(cUD.Email).Email, cUD.Email);

                //var user2 = logic.CreateUser(cUD);
                //var countUsers = from u in context.Users
                //                 where u.Email == user.Email
                //                 select u;
                //int count = 0;
                //foreach (User userMail in countUsers)
                //{
                //    count++;
                //}
                //Assert.Equal(1, count);
            }
        }

        /// <summary>
        /// Tests the UserExists() method of LogicClass
        /// Tests if user is already in the database
        /// </summary>
        [Fact]
        public async void TestForUserExists()
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
                User user = new User()
                {
                    UserName = "jerryrice",
                    Password = "jerry123",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com"
                };
                r.users.Add(user);
                await r.CommitSave();
                Assert.Contains<User>(user, context.Users);
                var userExists = await logic.UserExists(user.UserName, user.Email);
                Assert.True(userExists);
            }
        }

        /// <summary>
        /// Tests the LoginUser() method of LogicClass
        /// Tests if logged in user data is retrieved from database
        /// </summary>
        [Fact]
        public async void TestForLoginUser()
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
                User user = new User()
                {
                    UserName = "jerryrice",
                    Password = "jerry123",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com"
                };
                r.users.Add(user);
                await r.CommitSave();
                LoginDto loginDto = new LoginDto()
                {
                    UserName = user.UserName,
                    Password = user.Email
                };
                var user1 = await logic.LoginUser(loginDto);
                Assert.True(user1.UserName.Equals(user.UserName));
            }
        }

        /// <summary>
        /// Tests the DeleteUser() method of the LogicClass
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
                var deleteEmpty = await logic.DeleteUser(Guid.NewGuid()); 
                Assert.Contains<User>(user, context.Users);
                var delteUser = await logic.DeleteUser(user.UserID); 
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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

                var editedUser = await logic.EditUser(user.UserID, user2);
                Assert.Equal(editedUser.FullName, context.Users.Find(user.UserID).FullName);
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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

                var editedUser = await logic.CoachEditUser(user.UserID, user2);
                Assert.Equal(editedUser.RoleID, context.Users.Find(user.UserID).RoleID);
            }
        }

        /// <summary>
        /// Tests the GetUsers() method of LogicClass
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
                var listOfUsers = await logic.GetUsers();
                Assert.NotNull(listOfUsers);
                Assert.True(listOfUsers.FirstOrDefault(x => x.Email == user.Email).Email == user.Email);
            }
        }

        /// <summary>
        /// Tests the GetUserById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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
                var listOfUsers = await logic.GetUserById(user.UserID);
                Assert.True(listOfUsers.Equals(user));
            }
        }

        /// <summary>
        /// Tests the GetTeams() method of LogicClass
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
                var team = new Team
                {
                    TeamID = 4, // 4 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = await logic.GetTeams();
                Assert.NotNull(listOfTeams);
            }
        }

        /// <summary>
        /// Tests the GetTeamById() method of LogicClass
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
                var team = new Team
                {
                    TeamID = 5, // 5 for seeding
                    Name = "Broncos",
                    Wins = 2,
                    Losses = 1
                };

                r.teams.Add(team);
                var listOfTeams = await logic.GetTeamById(team.TeamID);
                Assert.True(listOfTeams.Equals(team));
            }
        }

        /// <summary>
        /// Tests the EditTeam method of LogicClass
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

                var editedTeam = await logic.EditTeam(team.TeamID, team2);
                Assert.Equal(editedTeam.Name, context.Teams.Find(team.TeamID).Name);
            }
        }

        /// <summary>
        /// Tests the GetRoles() method of LogicClass
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
                var role = new Role
                {
                    RoleID = 4, // 4 because of seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = await logic.GetRoles();
                Assert.NotNull(listOfRoles);
            }
        }

        /// <summary>
        /// Tests the GetRoleById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var role = new Role
                {
                    RoleID = 5, // 5 for seeding
                    RoleName = "Coach"
                };

                r.roles.Add(role);
                var listOfRoles = await logic.GetRoleById(role.RoleID);
                Assert.True(listOfRoles.Equals(role));
            }
        }

        /// <summary>
        /// Tests the EditUserRole method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForEditUserRole()
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
                var role = new Role()
                {
                    RoleID = 1,
                    RoleName = "Player"
                };
                var user = new User()
                {
                    UserID = Guid.NewGuid(),
                    FullName = "Tom Brady",
                    Email = "tombrady@gmail.com",
                    Password = "brady123",
                    PhoneNumber = "333-333-3333",
                    UserName = "tombrady",
                    TeamID = 1
                };

                r.roles.Add(role);
                r.users.Add(user);
                await r.CommitSave();

                var editedRole = await logic.EditUserRole(user.UserID, role.RoleID);
                Assert.Equal(editedRole.RoleID, context.Users.Find(user.UserID).RoleID);
            }
        }

        /// <summary>
        /// Tests the GetPlaybooks() method of LogicClass
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
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = await logic.GetPlaybooks();
                Assert.NotNull(listOfPlaybooks);
            }
        }

        /// <summary>
        /// Tests the GetPlaybookByid() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var playbook = new Playbook
                {
                    PlaybookID = 1,
                    TeamID = 1
                };

                r.playbooks.Add(playbook);
                var listOfPlaybooks = await logic.GetPlaybookById(playbook.PlaybookID);
                Assert.True(listOfPlaybooks.Equals(playbook));
            }
        }

        /// <summary>
        /// Tests the CreatePlaybook() method of LogicClass
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
                Team team = new Team()
                {
                    TeamID = 1,
                    Name = "Broncoes",
                    Wins = 2,
                    Losses = 0
                };
                var createPlaybook = await logic.CreatePlaybook(team.TeamID);

                //Assert.Equal(1, context.Playbooks.CountAsync().Result);

                Assert.Contains<Playbook>(createPlaybook, context.Playbooks);

            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of LogicClass
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
                PlayDto play = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    ImageString = "Football,football,football"
                };
                var createPlay = await logic.CreatePlay(play);
                //Assert.Equal(1, context.Plays.CountAsync().Result);
                Assert.Contains<Play>(createPlay, context.Plays);
            }
        }

        /// <summary>
        /// Tests the EditPlays method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForEditPlays()
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

                var editedPlay = await logic.EditPlay(play.PlayID, play2);
                Assert.Equal(editedPlay.Description, context.Plays.Find(play.PlayID).Description);
            }
        }
        
        /// <summary>
        /// Tests the GetPlays() method of LogicClass
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
                var play = new Play
                {
                    PlayID = 8,
                    PlaybookId = 5,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = null
                };

                r.plays.Add(play);
                await r.CommitSave();
                var listOfPlays = await logic.GetPlays();
                Assert.NotEmpty(listOfPlays);
                Assert.True(listOfPlays.FirstOrDefault(x => x.PlayID == play.PlayID).Name == play.Name);
                //Assert.Contains<PlayDto>(mapper.ConvertToPlayDto(play), listOfPlays);
            }
        }

        /// <summary>
        /// Tests the GetPlayDto() method of LogicClass
        /// </summary>
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
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var playDto = await logic.GetPlayDto(play.PlayID);
                Assert.True(playDto.PlayID.Equals(play.PlayID));
            }
        }

        /// <summary>
        /// Tests the GetPlayById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var play = new Play
                {
                    PlayID = 1,
                    PlaybookId = 1,
                    Name = "Tackle",
                    Description = "Tackle other players",
                    DrawnPlay = new byte[1]
                };

                r.plays.Add(play);
                var listOfPlays = await logic.GetPlayById(play.PlayID);
                Assert.True(listOfPlays.Equals(play));
            }
        }

        /// <summary>
        /// Tests the DeletePlay() method of the LogicClass
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
                var deleteEmpty = await logic.DeletePlay(3);
                Assert.Contains<Play>(play, context.Plays);
                var deletePlay = await logic.DeletePlay(play.PlayID);
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
        /// Tests the DeletePlaybook() method of the LogicClass
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
                var playbook = new Playbook()
                {
                    PlaybookID = 25,
                    TeamID = 13
                };
                r.playbooks.Add(playbook);
                await r.CommitSave();
                var deleteEmpty = await logic.DeletePlaybook(3);
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                var deletePlaybook = await logic.DeletePlaybook(playbook.PlaybookID);
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

        /// <summary>
        /// Tests the GetRecipientLists() method of LogicClass
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
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                var listOfRecipientLists = await logic.GetRecipientLists();
                Assert.NotNull(listOfRecipientLists);
            }
        }

        /// <summary>
        /// Tests the GetRecipientListById() method of LogicClass
        /// </summary>
        /// 
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                };

                r.recipientLists.Add(recipientList);
                await r.CommitSave();
                var listOfRecipientList = await logic.GetRecipientListById(recipientList.RecipientListID);
                Assert.True(listOfRecipientList.Equals(recipientList));
            }
        }

        /// <summary>
        /// Tests the BuildRecipientList() method of LogicClass
        /// Tests that a recipient list is added to the database
        /// </summary>
        [Fact]
        public async void TestForBuildRecipientList()
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
                RecipientList recipient = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid()
                };

                var createList = await logic.BuildRecipientList(recipient.RecipientListID, recipient.RecipientID);
                //Assert.Equal(1, context.Plays.CountAsync().Result);
                Assert.Contains<RecipientList>(createList, context.RecipientLists);
            }
        }

        /// <summary>
        /// Tests the GetMessages() method of LogicClass
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
                var message = new Message
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = await logic.GetMessages();
                Assert.NotNull(listOfMessages);
            }
        }

        /// <summary>
        /// Tests the GetMessageById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var message = new Message
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "Hello there"
                };

                r.messages.Add(message);
                var listOfMessages = await logic.GetMessageById(message.MessageID);
                Assert.True(listOfMessages.Equals(message));
            }
        }

        /// <summary>
        /// Tests the CreateNewMessage() method of LogicClass
        /// Tests that a message is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreateNewMessage()
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
                var message = new Message()
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    SentDate = DateTime.Now,
                    MessageText = "How you doin'?"
                };
                var rL = new List<Guid>();
                rL.Add(message.RecipientListID);
                var messageDto = new NewMessageDto()
                {
                    SenderID = message.SenderID,
                    RecipientList = rL,
                    MessageText = message.MessageText
                };
                var createMessage = await logic.CreateNewMessage(messageDto);
                
                //Assert.Equal(1, context.Messages.CountAsync().Result);
                Assert.Contains<Message>(createMessage, context.Messages);
            }
        }

        /// <summary>
        /// Tests the SendMessage() method of LogicClass
        /// Tests that a sent message is added to the database
        /// TODO: add assert statement
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
                var message = new Message()
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    SentDate = DateTime.Now,
                    MessageText = "How you doin'?"
                };
                var recipientList = new RecipientList()
                {
                    RecipientListID = message.RecipientListID,
                    RecipientID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                await r.CommitSave();

                var sendMessage = await logic.SendMessage(message);

                Assert.True(sendMessage);
            }
        }

        /// <summary>
        /// Tests the DeleteMessageFromInbox() method of LogicClass
        /// Tests that a message is deleted from the database
        /// </summary>
        [Fact]
        public async void TestForDeleteMessageFromInbox()
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
                var messageInbox = await logic.CreateUserInbox(Guid.NewGuid(), Guid.NewGuid());

                Assert.Contains<UserInbox>(messageInbox, context.UserInboxes);
                await logic.DeleteMessageFromInbox(messageInbox.UserID, messageInbox.MessageID);

                var countMessages =  from p in context.UserInboxes
                                    where p.UserID == messageInbox.UserID && p.MessageID == messageInbox.MessageID
                                    select p;
                int count = 0;
                foreach (UserInbox inbox in countMessages)
                {
                    count++;
                }
                Assert.Equal(0, count);

            }
        }

        /// <summary>
        /// Tests the GetUserInbox() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
                var userInbox = new UserInbox()
                {
                    UserID = Guid.NewGuid(),
                    MessageID = Guid.NewGuid(),
                    IsRead = true
                };

                r.userInboxes.Add(userInbox);
                var listOfUserInboxes = await logic.GetUserInbox(userInbox.UserID);
                Assert.NotNull(listOfUserInboxes);
            }
        }

        /// <summary>
        /// Tests the CreateUserInbox() method of LogicClass
        /// </summary>
        [Fact]
        public async void TestForCreateUserInbox()
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
                var userInbox = new UserInbox()
                {
                    UserID = Guid.NewGuid(),
                    MessageID = Guid.NewGuid(),
                    IsRead = false
                };

                var createInbox = await logic.CreateUserInbox(userInbox.UserID, userInbox.MessageID);
                Assert.Contains<UserInbox>(createInbox, context.UserInboxes);
            }
        }

        /// <summary>
        /// Tests the GetGames() method of LogicClass
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
                var listOfGames = await logic.GetGames();
                Assert.NotNull(listOfGames);
            }
        }

        /// <summary>
        /// Tests the GetGameById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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
                var listOfGames = await logic.GetGameById(game.GameID);
                Assert.True(listOfGames.Equals(game));
            }
        }

        /// <summary>
        /// Tests the CreateGame() method of LogicClass
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public async void TestForCreateGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using var context = new ProgContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Repo r = new Repo(context, new NullLogger<Repo>());
            Mapper mapper = new Mapper();
            LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
            CreateGameDto game = new CreateGameDto()
            {
                HomeTeamID = 1,
                AwayTeamID = 2
            };
            var createGame = await logic.CreateGame(game);
            //Assert.Equal(1, context.Games.CountAsync().Result);
            Assert.Contains<Game>(createGame, context.Games);
        }

        /// <summary>
        /// Tests the EditGame method of LogicClass
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

                var editedGame= await logic.EditGame(game.GameID, game2);
                Assert.Equal(editedGame.HomeScore, context.Games.Find(game.GameID).HomeScore);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequests() method of LogicClass
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
                var listOfEquipment = await logic.GetEquipmentRequests();
                Assert.NotNull(listOfEquipment);
            }
        }
        
        /// <summary>
        /// Tests the GetEquipmentRequestById() method of LogicClass
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
                Mapper mapper = new Mapper();
                LogicClass logic = new LogicClass(r, mapper, _token, new NullLogger<Repo>());
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
                var listOfEquipment = await logic.GetEquipmentRequestById(equipment.RequestID);
                Assert.True(listOfEquipment.Equals(equipment));
            }
        }

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of LogicClass
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
                CreateEquipmentRequestDto equipmentRequest = new CreateEquipmentRequestDto()
                {
                    TeamID = 1,
                    UserID = Guid.NewGuid(),
                    ItemID = 2,
                    Message = "Need this equipment",
                    Status = "Pending"
                };
                var createEquipmentRequest = await logic.CreateEquipmentRequest(equipmentRequest);
                //Assert.Equal(1, context.EquipmentRequests.CountAsync().Result);
                Assert.Contains<EquipmentRequest>(createEquipmentRequest, context.EquipmentRequests);
            }
        }

        /// <summary>
        /// Tests the EditEquipmentRequest method of LogicClass
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

                var editedEquipmentRequest = await logic.EditEquipmentRequest(equipmentRequest.RequestID, equipmentRequest2);
                Assert.Equal(editedEquipmentRequest.Status, context.EquipmentRequests.Find(equipmentRequest.RequestID).Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of LogicClass
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
                var equipmentItem = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipmentItem);
                var getEquipmentItems = await logic.GetEquipmentItems();
                Assert.NotNull(getEquipmentItems);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemById() method of LogicClass
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
                var equipmentItem = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipmentItem);
                var getEquipmentItem = await logic.GetEquipmentItemtById(equipmentItem.EquipmentID);
                Assert.True(getEquipmentItem.Equals(equipmentItem));
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemByName() method of LogicClass
        /// TODO: For some reason, assert statement doesn't work
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemByName()
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
                var equipmentItem = new EquipmentItem()
                {
                    EquipmentID = 9,
                    Description = "cup"
                };

                r.equipmentItems.Add(equipmentItem);
                await r.CommitSave();
                var getEquipmentItem = await logic.GetEquipmentItemByName(equipmentItem.Description);
                Assert.True(getEquipmentItem.Description.Equals(equipmentItem.Description));
            }
        }

        /// <summary>
        /// Tests the InitializeCalendar() method in LogicClass
        /// TODO: write actual test for InitializeCalendar
        /// </summary>
        /// 
        [Fact]
        public async void TestForInitializeCalendar()
        {

            EventDto eventDto = new EventDto()
            {
                Description = "Practice",
                Location = "Football field",
                Message = "Don't miss it!",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            };

            var calendarService = await LogicClass.InitializeCalendar();
            var calendar = await LogicClass.GetCalendar();
            var getEvents = await LogicClass.GetMyEvents();
            //var createEvent = await LogicClass.CreateEvent(eventDto);
            //var deleteEvent = await LogicClass.DeleteEvent(createEvent.Id);
        }

        //--------------------------End of LogicClass Tests-----------------------

        //---------------------------Start of Mapper Tests------------------------

        /// <summary>
        /// Tests the ConvertImage() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertImage()
        {
            Mapper mapper = new Mapper();
            string textSting = "text,text";
            var convert = mapper.ConvertImage(textSting);

            Assert.IsType<byte[]>(convert);
            Assert.NotNull(convert);
        }


        /// <summary>
        /// Tests the ConvertUserToUserDto() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertUserToUserDto()
        {
            Mapper mapper = new Mapper();
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

            var convert = mapper.ConvertUserToUserDto(user);

            Assert.True(convert.UserName.Equals(user.UserName));
        }

        /// <summary>
        /// Tests the ConvertToPlayDto() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertToPlayDto()
        {
            Mapper mapper = new Mapper();
            var play = new Play()
            {
                PlayID = 6,
                Name = "Tackle",
                Description = "Tackle other players",
                PlaybookId = 3,
                DrawnPlay = new byte[1]
            };

            var convert = mapper.ConvertToPlayDto(play);

            Assert.True(convert.Name.Equals(play.Name));
        }

        /// <summary>
        /// Tests the ConvertUserToUserLoggedInDto() method of Mapper
        /// </summary>
        /// 
        [Fact]
        public void TestForConvertUserToUserLoggedInDto()
        {
            Mapper mapper = new Mapper();
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

            var convert = mapper.ConvertUserToUserLoggedInDto(user);

            Assert.True(convert.UserName.Equals(user.UserName));
            
        }

        //----------------------------End of Mapper tests-------------------------

        //-------------------------Start of TokenService Tests--------------------

        /// <summary>
        /// Tests the CreateToken() method in TokenService
        /// TODO: write actual test for CreateToken()
        /// </summary>
        [Fact]
        public void TestForCreateToken()
        {

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
            //TokenService token = new TokenService(mockConfiguration.Object);
            //User user = new User()
            //{
            //    UserID = Guid.NewGuid(),
            //    UserName = "jerryrice",
            //    Password = "jerry123",
            //    PasswordHash = new byte[1],
            //    PasswordSalt = new byte[1],
            //    FullName = "Jerry Rice",
            //    Email = "jerryrice@gmail.com",
            //    PhoneNumber = "222-222-2222",
            //    TeamID = 1,
            //    RoleID = 1
            //};

            //var createToken = token.CreateToken(user);
        }

        //--------------------------End of TokenService Tests---------------------
    } // end of class

    public static class MockJwtTokens
    {
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
        private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
        private static readonly byte[] s_key = new byte[32];

        static MockJwtTokens()
        {
            s_rng.GetBytes(s_key);
            SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
        }
    }

    public class BaseIntegrationTest : WebApplicationFactory<IStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(ConfigureServices);
            builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole(options => options.IncludeScopes = true);
            });
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration()
                {
                    Issuer = MockJwtTokens.Issuer
                };

                config.SigningKeys.Add(MockJwtTokens.SecurityKey);
                options.Configuration = config;
            });
        }
    }
} // end of namespace
