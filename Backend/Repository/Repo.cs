using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {
        private readonly ProgContext _progContext;
        private readonly ILogger _logger;
        public DbSet<User> users;
        public DbSet<Game> games;
        public DbSet<Play> plays;
        public DbSet<Playbook> playbooks;
        public DbSet<Event> events;
        public DbSet<EquipmentRequest> equipmentRequests;
        public DbSet<Role> roles;
        public DbSet<Team> teams;
        public DbSet<Message> messages;
        public DbSet<RecipientList> recipientLists;
        public DbSet<UserInbox> userInboxes;

        public Repo(ProgContext progContext, ILogger<Repo> logger)
        {
            _progContext = progContext;
            _logger = logger;
            this.users = _progContext.Users;
            this.games = _progContext.Games;
            this.plays = _progContext.Plays;
            this.playbooks = _progContext.Playbooks;
            this.events = _progContext.Events;
            this.equipmentRequests = _progContext.EquipmentRequests;
            this.roles = _progContext.Roles;
            this.teams = _progContext.Teams;
            this.messages = _progContext.Messages;
            this.recipientLists = _progContext.RecipientLists;
            this.userInboxes = _progContext.UserInboxes;
            _logger.LogInformation("Context created.");
            ValidateRoleTable();
            _logger.LogInformation("Role table validated.");
            ValidateTeamTable();
            _logger.LogInformation("Team table validated.");
            ValidateUserTable();
            _logger.LogInformation("User table validated.");
            ValidateEquipmentRequestTable();
            _logger.LogInformation("Request table validated.");
        }

        // Access SaveChanges from Logic class
        public async Task CommitSave()
        {
            await _progContext.SaveChangesAsync();
            _logger.LogInformation("Context saved.");
        }
        // Context accessors
        public async Task<IEnumerable<User>> GetUsers()
        {
            List<User> uList = await users.ToListAsync();
            return uList;
        }
        public async Task<User> GetUserById(Guid id)
        {
            return await users.FindAsync(id);
        }
        public async Task<Team> GetTeamById(int id)
        {
            return await teams.FindAsync(id);
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await teams.ToListAsync();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await roles.FindAsync(id);
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await roles.ToListAsync();
        }
        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await playbooks.FindAsync(id);
        }
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await playbooks.ToListAsync();
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await plays.FindAsync(id);
        }
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await plays.ToListAsync();
        }
        public async Task<Message> GetMessageById(Guid id)
        {
            return await messages.FindAsync(id);
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await messages.ToListAsync();
        }
        public async Task<RecipientList> GetRecipientListById(Guid id)
        {
            return await recipientLists.FindAsync(id);
        }
        public async Task<IEnumerable<RecipientList>> GetRecipientLists()
        {
            return await recipientLists.ToListAsync();
        }
        public async Task<Game> GetGameById(int id)
        {
            return await games.FindAsync(id);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await games.ToListAsync();
        }
        public async Task<Event> GetEventById(int id)
        {
            return await events.FindAsync(id);
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await events.ToListAsync();
        }
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await equipmentRequests.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await equipmentRequests.ToListAsync();
        }

        private void ValidateRoleTable()
        {
            if (roles.Any())
            {
                string[] roleNames = { "Coach", "Player", "Parent" };

                for (int i = 0; i < roleNames.Length; i++)
                {
                    Role newRole = new Role
                    {
                        RoleName = $"{roleNames[i]}"
                    };
                    roles.Add(newRole);
                }
                _progContext.SaveChanges();
            }
            _logger.LogInformation("Table populated.");
        }
        private void ValidateTeamTable()
        {
            
            if (teams.Any())
            {
                string[] teamNames = { "Lions", "Tigers", "Bears" };

                for (int i = 0; i < teamNames.Length; i++)
                {
                    Team newTeam = new Team
                    {
                        Name = $"{teamNames[i]}"
                    };
                    teams.Add(newTeam);
                }
                _progContext.SaveChanges();
            }
            _logger.LogInformation("Table populated.");
        }
        private void ValidateUserTable()
        {
            if (users.Any())
            {
                string[] userNames = { "jerryjones1", "jerryrice1", "terrybradshaw1",
                "lionplayer1", "lionplayer2", "tigerplayer1", "tigerplayer2", "bearplayer1", "bearplayer2",
                "lionparent1", "lionparent2", "tigerparent1", "tigerparent2", "bearparent1", "bearparent2"};

                string[] passwords = { "jerry123", "jerryr123", "terry123",
                "password1", "password2", "password3", "password4", "password5", "password6",
                "parent1","parent2","parent3","parent4","parent5","parent6"};

                string[] names = { "Jerry Jones", "Jerry Rice", "Terry Bradshaw",
                "lionplayer1", "lionplayer2", "tigerplayer1", "tigerplayer2", "bearplayer1", "bearplayer2",
                "lionparent1", "lionparent2", "tigerparent1", "tigerparent2", "bearparent1", "bearparent2"};

                string[] phoneNumbers = { "123-456-7899", "222-454-7689", "213-796-5698",
                "856-369-8888", "312-568-1234", "147-258-0369", "963-852-7410", "555-111-9999", "654-322-9870",
                "856-369-8888", "312-568-1234", "147-258-0369", "963-852-7410", "555-111-9999", "654-322-9870"};

                string[] emails = { "jerry@jones.com", "jerry@rice.com", "terry@bradshaw.com",
                "lion1@player.com", "lion2@player.com", "tiger1@player.com", "tiger2@player.com", "bear1@player.com", "bear2@player.com",
                "lion1@parent.com", "lion2@parent.com", "tiger1@parent.com", "tiger2@parent.com", "bear1@parent.com", "bear2@parent.com"};
                int[] teamIds = { 1, 2, 3,
                1, 1, 2, 2, 3, 3,
                1, 1, 2, 2, 3, 3};
                int[] roleIds = { 1, 1, 1,
                2, 2, 2, 2, 2, 2,
                3, 3, 3, 3, 3, 3};

                for (int i = 0; i < userNames.Length; i++)
                {
                    User newUser = new User
                    {
                        UserName = $"{userNames[i]}",
                        Password = $"{passwords[i]}",
                        FullName = $"{names[i]}",
                        PhoneNumber = $"{phoneNumbers[i]}",
                        Email = $"{emails[i]}",
                        TeamID = teamIds[i],
                        RoleID = roleIds[i]
                    };
                    users.Add(newUser);
                }
                _progContext.SaveChanges();
            }
            _logger.LogInformation("Table populated.");
        }
        private void ValidateEquipmentRequestTable()
        {
            if (equipmentRequests.Any())
            {
                User user1 = users.FirstOrDefault(x => x.UserName == "lionparent1");
                User user2 = users.FirstOrDefault(x => x.UserName == "bearparent2");

                Guid[] userList = { user1.UserID, user2.UserID };
                int[] teamsList = { 1, 3 };
                DateTime[] requestTimes = { DateTime.Now, DateTime.Now };
                string[] messagesList = { "This is a message for request 1", "This is a message for request 2" };
                int[] items = { 1, 2 };
                string[] status = { "Requested", "Fulfilled" };

                for (int i = 0; i < userList.Length; i++)
                {
                    EquipmentRequest newRequest = new EquipmentRequest
                    {
                        UserID = userList[i],
                        TeamID = teamsList[i],
                        RequestDate = requestTimes[i],
                        Message = $"{messagesList[i]}",
                        ItemId = items[i],
                        Status = status[i]
                    };
                    equipmentRequests.Add(newRequest);
                }
                _progContext.SaveChanges();
            }
            _logger.LogInformation("Table populated.");
        }
    }
}