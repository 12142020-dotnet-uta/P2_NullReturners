using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;
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
        public async Task<User> GetUserById(Guid id)
        {
            return await _repo.GetUserById(id);
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repo.GetUsers();
        }
        //Users 
        public async Task<User> CreateUser(string userName, string password, string fullName, string phoneNumber, string email)
        {
            User user = _repo.users.FirstOrDefault(x => x.UserName == userName || x.Email == email);
            if (user == null)
            {
                user = new User()
                {
                    UserName = userName,
                    Password = password,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Email = email
                };
                await _repo.users.AddAsync(user);
                await _repo.CommitSave();
                _logger.LogInformation("User created");
            }
            else
            {
                _logger.LogInformation("User found in database");
            }
            return user;
        }
        public async Task DeleteUser(Guid id)
        {
            User user = await _repo.users.FindAsync(id);
            if (user != null)
            {
                _repo.users.Remove(user);
                await _repo.CommitSave();
                _logger.LogInformation("User removed");
            }
            else
            {
                _logger.LogInformation("User not found");
            }
        }
        public async Task<User> AddUserRole(User user, int roleId)
        {
            User tUser = await _repo.GetUserById(user.UserID);
            tUser.RoleID = roleId;
            await _repo.CommitSave();
            return tUser;
        }
        public async Task<User> EditUser(User user)
        {
            User tUser = _repo.users.FirstOrDefault(x => x.UserID == user.UserID);
            if (tUser.FullName != user.FullName) { tUser.FullName = user.FullName; }
            if (tUser.Email != user.Email) { tUser.Email = user.Email; }
            if (tUser.Password != user.Password) { tUser.Password = user.Password; }
            if (tUser.PhoneNumber != user.PhoneNumber) { tUser.PhoneNumber = user.PhoneNumber; }
            await _repo.CommitSave();
            return tUser;  
        }
        public async Task<User> CoachEditUser(User user)
        {
            User tUser = _repo.users.FirstOrDefault(x => x.UserID == user.UserID);
            if (tUser.FullName != user.FullName) { tUser.FullName = user.FullName; }
            if (tUser.Email != user.Email) { tUser.Email = user.Email; }
            if (tUser.Password != user.Password) { tUser.Password = user.Password; }
            if (tUser.PhoneNumber != user.PhoneNumber) { tUser.PhoneNumber = user.PhoneNumber; }
            if (tUser.RoleID != user.RoleID) { tUser.RoleID = user.RoleID; }
            if (tUser.UserName != user.UserName) { tUser.UserName = user.UserName; }
            await _repo.CommitSave();
            return tUser;
        }
        //Teams
        public async Task<Team> GetTeamById(int id)
        {
            return await _repo.GetTeamById(id);
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _repo.GetTeams();
        }
        //Roles
        public async Task<Role> GetRoleById(int id)
        {
            return await _repo.GetRoleById(id);
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _repo.GetRoles();
        }
        //Playbooks
        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await _repo.GetPlaybookById(id);
        }
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _repo.GetPlaybooks();
        }
        public async Task<Play> CreateNewPlay(Guid userId, string playName, string description, byte[] drawnPlay)
        {
            User user = await GetUserById(userId);
            var playbookId = _repo.playbooks.FirstOrDefault(x => x.TeamID == user.TeamID).PlaybookID;
            Play newPlay = new Play()
            {
                PlaybookId = playbookId,
                Name = playName,
                Description = description,
                DrawnPlay = drawnPlay
            };
            await _repo.plays.AddAsync(newPlay);
            await _repo.CommitSave();
            return newPlay;
        }
        public async Task<Play> EditPlay(Play play, PlayDto playDto) 
        {
            Play editedPlay = await _repo.plays.FindAsync(play);
            editedPlay.Name = playDto.Name;
            editedPlay.Description = playDto.Description;
            editedPlay.DrawnPlay = playDto.DrawnPlay;
            await _repo.CommitSave();
            return editedPlay;
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await _repo.GetPlayById(id);
        }
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await _repo.GetPlays();
        }
        //Messaging
        public async Task<Message> GetMessageById(Guid id)
        {
            return await _repo.GetMessageById(id);
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _repo.GetMessages();
        }
        public async Task<RecipientList> GetRecipientListById(Guid id)
        {
            return await _repo.GetRecipientListById(id);
        }
        public async Task<IEnumerable<RecipientList>> GetRecipientLists()
        {
            return await _repo.GetRecipientLists();
        }
        public async Task<Message> CreateNewMessage(Guid senderId, Guid recipientListId, string message)
        {
            Message newMessage = new Message()
            {
                SenderID = senderId,
                RecipientListID = recipientListId,
                MessageText = message
            };
            await _repo.messages.AddAsync(newMessage);
            await _repo.CommitSave();
            return newMessage;
        }
        public async Task SendMessage(Message message)
        {            
            List<Guid> recipientList = new List<Guid>();
            foreach (RecipientList r in _repo.recipientLists)
            {
                if (r.RecipientListID == message.RecipientListID)
                {
                    recipientList.Add(r.RecipientID);
                }
            }
            //foreach(Guid r in recipientList)
            //{
            //    add userid, messageid, read = false to inboxes, change user inbox status to notify
            //}
            
            await _repo.CommitSave();
        }
        //Games
        public async Task<Game> GetGameById(int id)
        {
            return await _repo.GetGameById(id);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _repo.GetGames();
        }
        //Events
        public async Task<Event> GetEventById(int id)
        {
            return await _repo.GetEventById(id);
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _repo.GetEvents();
        }
        //Equipment
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
