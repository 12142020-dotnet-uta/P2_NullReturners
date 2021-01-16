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
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await users.FindAsync(id);
        }


        public void CommitSave()
        {
            _progContext.SaveChanges();
        }


    }
}
