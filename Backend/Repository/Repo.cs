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
            this.equipmentRequests = _progContext.EquipmentRequests;
            this.roles = _progContext.Roles;
            this.teams = _progContext.Teams;
            this.messages = _progContext.Messages;
            this.recipientLists = _progContext.RecipientLists;
            this.userInboxes = _progContext.UserInboxes;
            //ValidateRoleTable();
            //ValidateTeamTable();
            //ValidateUserTable();
            //ValidateEquipmentRequestTable();
        }

        // Access SaveChanges from Logic class
        public async Task CommitSave()
        {
            await _progContext.SaveChangesAsync();
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
        public async Task<IEnumerable<UserInbox>> GetUserInbox(Guid id)
        {
            return await userInboxes.Where(x => x.UserID == id).ToListAsync();
        }
        public async Task<RecipientList> GetRecipientListById(Guid listId, Guid recId)
        {
            return await recipientLists.FindAsync(listId, recId);
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
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await equipmentRequests.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await equipmentRequests.ToListAsync();
        }

        //private void validateroletable()
        //{
        //    if (roles.count() == 0)
        //    {
        //        string[] rolenames = { "coach", "player", "parent" };

        //        for (int i = 0; i < rolenames.length; i++)
        //        {
        //            role newrole = new role
        //            {
        //                rolename = $"{rolenames[i]}"
        //            };
        //            roles.add(newrole);
        //        }
        //        _progcontext.savechanges();
        //    }
        //}
        //private void validateteamtable()
        //{

        //    if (teams.count() == 0)
        //    {
        //        string[] teamnames = { "lions", "tigers", "bears" };

        //        for (int i = 0; i < teamnames.length; i++)
        //        {
        //            team newteam = new team
        //            {
        //                name = $"{teamnames[i]}"
        //            };
        //            teams.add(newteam);
        //        }
        //        _progcontext.savechanges();
        //    }
        //}
        //private void validateusertable()
        //{
        //    if (users.count() == 0)
        //    {
        //        string[] usernames = { "jerryjones1", "jerryrice1", "terrybradshaw1",
        //        "lionplayer1", "lionplayer2", "tigerplayer1", "tigerplayer2", "bearplayer1", "bearplayer2",
        //        "lionparent1", "lionparent2", "tigerparent1", "tigerparent2", "bearparent1", "bearparent2"};

        //        string[] passwords = { "jerry123", "jerryr123", "terry123",
        //        "password1", "password2", "password3", "password4", "password5", "password6",
        //        "parent1","parent2","parent3","parent4","parent5","parent6"};

        //        string[] names = { "jerry jones", "jerry rice", "terry bradshaw",
        //        "lionplayer1", "lionplayer2", "tigerplayer1", "tigerplayer2", "bearplayer1", "bearplayer2",
        //        "lionparent1", "lionparent2", "tigerparent1", "tigerparent2", "bearparent1", "bearparent2"};

        //        string[] phonenumbers = { "123-456-7899", "222-454-7689", "213-796-5698",
        //        "856-369-8888", "312-568-1234", "147-258-0369", "963-852-7410", "555-111-9999", "654-322-9870",
        //        "856-369-8888", "312-568-1234", "147-258-0369", "963-852-7410", "555-111-9999", "654-322-9870"};

        //        string[] emails = { "jerry@jones.com", "jerry@rice.com", "terry@bradshaw.com",
        //        "lion1@player.com", "lion2@player.com", "tiger1@player.com", "tiger2@player.com", "bear1@player.com", "bear2@player.com",
        //        "lion1@parent.com", "lion2@parent.com", "tiger1@parent.com", "tiger2@parent.com", "bear1@parent.com", "bear2@parent.com"};
        //        int[] teamids = { 1, 2, 3,
        //        1, 1, 2, 2, 3, 3,
        //        1, 1, 2, 2, 3, 3};
        //        int[] roleids = { 1, 1, 1,
        //        2, 2, 2, 2, 2, 2,
        //        3, 3, 3, 3, 3, 3};

        //        for (int i = 0; i < usernames.length; i++)
        //        {
        //            user newuser = new user
        //            {
        //                username = $"{usernames[i]}",
        //                password = $"{passwords[i]}",
        //                fullname = $"{names[i]}",
        //                phonenumber = $"{phonenumbers[i]}",
        //                email = $"{emails[i]}",
        //                teamid = teamids[i],
        //                roleid = roleids[i]
        //            };
        //            users.add(newuser);
        //        }
        //        _progcontext.savechanges();
        //    }
        //}
        //private void validateequipmentrequesttable()
        //{
        //    if (equipmentrequests.count() == 0)
        //    {
        //        user user1 = users.firstordefault(x => x.username == "lionparent1");
        //        user user2 = users.firstordefault(x => x.username == "bearparent2");

        //        guid[] userlist = { user1.userid, user2.userid };
        //        int[] teamslist = { 1, 3 };
        //        datetime[] requesttimes = { datetime.now, datetime.now };
        //        string[] messageslist = { "this is a message for request 1", "this is a message for request 2" };
        //        int[] items = { 1, 2 };
        //        string[] status = { "requested", "fulfilled" };

        //        for (int i = 0; i < userlist.length; i++)
        //        {
        //            equipmentrequest newrequest = new equipmentrequest
        //            {
        //                userid = userlist[i],
        //                teamid = teamslist[i],
        //                requestdate = requesttimes[i],
        //                message = $"{messageslist[i]}",
        //                itemid = items[i],
        //                status = status[i]
        //            };
        //            equipmentrequests.add(newrequest);
        //        }
        //        _progcontext.savechanges();
        //    }
        //}
    }
}