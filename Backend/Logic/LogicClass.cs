using Microsoft.Extensions.Logging;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicClass
    {
        public LogicClass() { }
        public LogicClass(Repo repo, Mapper mapper, ILogger<Repo> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        private readonly Repo _repo;
        private readonly Mapper _mapper;
        private readonly ILogger _logger;

        // Context accessors
        public async Task<User> GetUserById(int id)
        {
            return await _repo.GetUserById(id);
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repo.GetUsers();
        }
        public async Task<Team> GetTeamById(int id)
        {
            return await _repo.GetTeamById(id);
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _repo.GetTeams();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _repo.GetRoleById(id);
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _repo.GetRoles();
        }
        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await _repo.GetPlaybookById(id);
        }
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _repo.GetPlaybooks();
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await _repo.GetPlayById(id);
        }
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await _repo.GetPlays();
        }
        public async Task<Message> GetMessageById(int id)
        {
            return await _repo.GetMessageById(id);
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _repo.GetMessages();
        }
        public async Task<Game> GetGameById(int id)
        {
            return await _repo.GetGameById(id);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _repo.GetGames();
        }
        public async Task<Event> GetEventById(int id)
        {
            return await _repo.GetEventById(id);
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _repo.GetEvents();
        }
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await _repo.GetEquipmentRequestById(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await _repo.GetEquipmentRequests();
        }
    }
}
