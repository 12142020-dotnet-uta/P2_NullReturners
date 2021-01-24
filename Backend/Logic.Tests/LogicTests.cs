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

namespace Logic.Tests
{
    public class LogicTests
    {

        private readonly Mapper _mapper;
        //private readonly ILogger<Repo> _logger;
        
        /// <summary>
        /// Tests the CreateUser() method of LogicClass
        /// Tests that a user is added to the database
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                CreateUserDto cUD = new CreateUserDto()
                {
                    UserName = "jerryrice",
                    Password = "jerry123",
                    FullName = "Jerry Rice",
                    PhoneNumber = "111-111-1111",
                    Email = "jerryrice@gmail.com"
                };
                var user = logic.CreateUser(cUD);
                //Assert.NotEmpty(context.Users);
                Assert.Contains<User>(user.Result, context.Users);

                var user2 = logic.CreateUser(cUD);
                //Assert.Equal(1, context.Users.CountAsync().Result); // this is 16 because of seeding. remove when not seeding.
                var countUsers = from u in context.Users
                                 where u.Email == user.Result.Email
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                //Assert.NotEmpty(context.Users);
                Assert.Contains<User>(user, context.Users);
                logic.DeleteUser(user.UserID); // fails for some reason when I add await
                //Assert.Equal(0, context.Users.CountAsync().Result); // using this cause there are 15 normally. +1 -1 = 15.
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                var editedTeam = logic.EditTeam(team.TeamID, team2);
                Assert.Equal(editedTeam.Result.Name, context.Teams.Find(team.TeamID).Name);
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                var editedRole = logic.EditUserRole(user.UserID, role.RoleID);
                Assert.Equal(editedRole.Result.RoleID, context.Users.Find(user.UserID).RoleID);
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
        /// Tests the CreatePlaybook() method of LogicClass
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                Team team = new Team()
                {
                    TeamID = 1,
                    Name = "Broncoes",
                    Wins = 2,
                    Losses = 0
                };
                var createPlaybook = logic.CreatePlaybook(team.TeamID);

                //Assert.Equal(1, context.Playbooks.CountAsync().Result);

                Assert.Contains<Playbook>(createPlaybook.Result, context.Playbooks);

            }
        }

        /// <summary>
        /// Tests the CreatePlay() method of LogicClass
        /// Tests that a play is added to the database
        /// </summary>
        [Fact]
        public void TestForCreatePlay()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                PlayDto play = new PlayDto()
                {
                    PlaybookID = 1,
                    Name = "Tackle",
                    Description = "Tackle other players"
                };
                var createPlay = logic.CreatePlay(play);
                //Assert.Equal(1, context.Plays.CountAsync().Result);
                Assert.Contains<Play>(createPlay.Result, context.Plays);
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                var editedPlay = logic.EditPlay(play.PlayID, play2);
                Assert.Equal(editedPlay.Result.Description, context.Plays.Find(play.PlayID).Description);
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
                logic.DeletePlay(3); // fails for some reason when I add await
                //Assert.NotEmpty(context.Plays);
                Assert.Contains<Play>(play, context.Plays);
                logic.DeletePlay(play.PlayID); // fails for some reason when I add await
                //Assert.Empty(context.Users);
                //Assert.Equal(0, context.Plays.CountAsync().Result); // using this cause there are 15 normally. +1 -1 = 15.
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                var playbook = new Playbook()
                {
                    PlaybookID = 25,
                    TeamID = 13
                };
                r.playbooks.Add(playbook);
                await r.CommitSave();
                logic.DeletePlaybook(3); // fails for some reason when I add await
                //Assert.NotEmpty(context.Playbooks);
                Assert.Contains<Playbook>(playbook, context.Playbooks);
                logic.DeletePlaybook(playbook.PlaybookID); // fails for some reason when I add await
                //Assert.Empty(context.Users);
                //Assert.Equal(0, context.Playbooks.CountAsync().Result); // using this cause there are 15 normally. +1 -1 = 15.
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid()
                };

                r.recipientLists.Add(recipientList);
                var listOfRecipientLists = logic.GetRecipientLists();
                Assert.NotNull(listOfRecipientLists);
            }
        }

        /// <summary>
        /// Tests the GetRecipientListById() method of LogicClass
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                var recipientList = new RecipientList()
                {
                    RecipientListID = Guid.NewGuid(),
                    RecipientID = Guid.NewGuid(),
                };

                r.recipientLists.Add(recipientList);
                var listOfRecipientList = logic.GetRecipientListById(recipientList.RecipientListID, recipientList.RecipientID);
                Assert.True(listOfRecipientList.Result.Equals(recipientList));
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
        /// Tests the CreateNewMessage() method of LogicClass
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public void TestForCreateNewMessage()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using (var context = new ProgContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                var message = new Message()
                {
                    MessageID = Guid.NewGuid(),
                    SenderID = Guid.NewGuid(),
                    RecipientListID = Guid.NewGuid(),
                    MessageText = "How you doin'?"
                };
                var rL = new List<Guid>();
                rL.Add(Guid.NewGuid());
                var messageDto = new NewMessageDto()
                {
                    SenderID = message.SenderID,
                    RecipientList = rL,
                    MessageText = message.MessageText
                };
                var createMessage = logic.CreateNewMessage(messageDto);

                //Assert.Equal(1, context.Messages.CountAsync().Result);
                Assert.Contains<Message>(createMessage.Result, context.Messages);
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
        /// Tests the CreateGame() method of LogicClass
        /// Tests that a user is added to the database
        /// </summary>
        [Fact]
        public void TestForCreateGame()
        {
            var options = new DbContextOptionsBuilder<ProgContext>()
            .UseInMemoryDatabase(databaseName: "p2newsetuptest")
            .Options;

            using var context = new ProgContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Repo r = new Repo(context, new NullLogger<Repo>());
            LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
            CreateGameDto game = new CreateGameDto()
            {
                HomeTeamID = 1,
                AwayTeamID = 2
            };
            var createGame = logic.CreateGame(game);
            //Assert.Equal(1, context.Games.CountAsync().Result);
            Assert.Contains<Game>(createGame.Result, context.Games);
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                var editedGame= logic.EditGame(game.GameID, game2);
                Assert.Equal(editedGame.Result.HomeScore, context.Games.Find(game.GameID).HomeScore);
            }
        }

        /// <summary>
        /// Tests the GetEvents() method of LogicClass
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
        //        LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
        //        var listOfEvents = logic.GetEvents();
        //        Assert.NotNull(listOfEvents);
        //    }
        //}

        /// <summary>
        /// Tests the GetEventById() method of LogicClass
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
        //        LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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
        //        var listOfEvents = logic.GetEventById(eventSchedule.EventID);
        //        Assert.True(listOfEvents.Result.Equals(eventSchedule));
        //    }
        //}

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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                Repo r = new Repo(context, new NullLogger<Repo>());
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of LogicClass
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
                CreateEquipmentRequestDto equipmentRequest = new CreateEquipmentRequestDto()
                {
                    TeamID = 1,
                    UserID = Guid.NewGuid(),
                    ItemID = 2,
                    RequestDate = DateTime.Now,
                    Message = "Need this equipment",
                    Status = "Pending"
                };
                var createEquipmentRequest = logic.CreateEquipmentRequest(equipmentRequest);
                //Assert.Equal(1, context.EquipmentRequests.CountAsync().Result);
                Assert.Contains<EquipmentRequest>(createEquipmentRequest.Result, context.EquipmentRequests);
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
                LogicClass logic = new LogicClass(r, _mapper, new NullLogger<Repo>());
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

                var editedEquipmentRequest = logic.EditEquipmentRequest(equipmentRequest.RequestID, equipmentRequest2);
                Assert.Equal(editedEquipmentRequest.Result.Status, context.EquipmentRequests.Find(equipmentRequest.RequestID).Status);
            }
        }

    } // end of class
} // end of namespace
