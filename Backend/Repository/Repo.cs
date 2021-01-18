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
        }

        // Access SaveChanges from Logic class
        public async Task CommitSave()
        {
            await _progContext.SaveChangesAsync();
        }
        // Context accessors
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await users.ToListAsync();
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
        public async Task<Message> GetMessageById(int id)
        {
            return await messages.FindAsync(id);
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await messages.ToListAsync();
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
    }
}
