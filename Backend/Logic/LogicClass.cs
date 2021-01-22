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
        public async Task<User> CreateUser(CreateUserDto createUser)
        {
            User user = _repo.users.FirstOrDefault(x => x.UserName == createUser.UserName || x.Email == createUser.Email);
            if (user == null)
            {
                user = new User()
                {
                    UserName = createUser.UserName,
                    Password = createUser.Password,
                    FullName = createUser.FullName,
                    PhoneNumber = createUser.PhoneNumber,
                    Email = createUser.Email,
                    TeamID = createUser.TeamID,
                    RoleID = createUser.RoleID
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
        public async Task<User> DeleteUser(Guid id)
        {
            User user = await GetUserById(id);
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
            return user;
        }
        public async Task<User> AddUserRole(Guid userId, int roleId)
        {

            User tUser = await GetUserById(userId);
            tUser.RoleID = roleId;
            await _repo.CommitSave();
            return tUser;
        }
        public async Task<User> EditUser(Guid userId, EditUserDto editUserDto)
        {
            User tUser = await GetUserById(userId);
            if (tUser.FullName != editUserDto.FullName) { tUser.FullName = editUserDto.FullName; }
            if (tUser.Email != editUserDto.Email) { tUser.Email = editUserDto.Email; }
            if (tUser.Password != editUserDto.Password) { tUser.Password = editUserDto.Password; }
            if (tUser.PhoneNumber != editUserDto.PhoneNumber) { tUser.PhoneNumber = editUserDto.PhoneNumber; }
            await _repo.CommitSave();
            return tUser;  
        }
        public async Task<User> CoachEditUser(Guid userId, CoachEditUserDto coachEditUserDto)
        {
            User tUser = await GetUserById(userId);
            if (tUser.FullName != coachEditUserDto.FullName) { tUser.FullName = coachEditUserDto.FullName; }
            if (tUser.Email != coachEditUserDto.Email) { tUser.Email = coachEditUserDto.Email; }
            if (tUser.Password != coachEditUserDto.Password) { tUser.Password = coachEditUserDto.Password; }
            if (tUser.PhoneNumber != coachEditUserDto.PhoneNumber) { tUser.PhoneNumber = coachEditUserDto.PhoneNumber; }
            if (tUser.RoleID != coachEditUserDto.RoleID) { tUser.RoleID = coachEditUserDto.RoleID; }
            if (tUser.UserName != coachEditUserDto.UserName) { tUser.UserName = coachEditUserDto.UserName; }
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
        public async Task<Team> EditTeam(int id, EditTeamDto editTeamDto)
        {
            Team tTeam = await GetTeamById(id);
            if (tTeam.Name != editTeamDto.Name) { tTeam.Name = editTeamDto.Name; }
            if (tTeam.Wins != editTeamDto.Wins) { tTeam.Wins = editTeamDto.Wins; }
            if (tTeam.Losses != editTeamDto.Losses) { tTeam.Losses = editTeamDto.Losses; }
            await _repo.CommitSave();
            return tTeam;
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
        public async Task<Role> EditUserRole(Guid userId, int roleId)
        {
            User tUser = await GetUserById(userId);
            tUser.RoleID = roleId;
            await _repo.CommitSave();
            return await GetRoleById(roleId);
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
        public async Task<Playbook> CreatePlaybook(int teamId)
        {
            Playbook newPlayBook = new Playbook()
            {
                TeamID = teamId
            };
            await _repo.playbooks.AddAsync(newPlayBook);
            await _repo.CommitSave();
            return newPlayBook;
        }
        public async Task<Play> CreatePlay(PlayDto playDto)
        {
            Play newPlay = new Play()
            {
                PlaybookId = playDto.PlaybookID,
                Name = playDto.Name,
                Description = playDto.Description,
                DrawnPlay = playDto.DrawnPlay
            };
            await _repo.plays.AddAsync(newPlay);
            await _repo.CommitSave();
            return newPlay;
        }
        public async Task<Play> EditPlay(int playId, PlayDto playDto) 
        {
            Play editedPlay = await GetPlayById(playId);
            if (editedPlay != null)
            {
                if (editedPlay.Name != playDto.Name) { editedPlay.Name = playDto.Name; }
                if (editedPlay.Description != playDto.Description) { editedPlay.Description = playDto.Description; }
                if (editedPlay.DrawnPlay != playDto.DrawnPlay) { editedPlay.DrawnPlay = playDto.DrawnPlay; }
                await _repo.CommitSave();
            }
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
        public async Task<Playbook> DeletePlaybook(int id)
        {
            Playbook playbook = await GetPlaybookById(id);
            if (playbook != null)
            {
                _repo.playbooks.Remove(playbook);
                await _repo.CommitSave();
            }
            return playbook;
        }
        public async Task<Play> DeletePlay(int id)
        {
            Play play = await GetPlayById(id);
            if (play != null)
            {
                _repo.plays.Remove(play);
                await _repo.CommitSave();
            }
            return play;
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
        public async Task<Game> CreateGame(CreateGameDto createGameDto)
        {
            Game newGame = new Game()
            {
                HomeTeamID = createGameDto.HomeTeamID,
                AwayTeamID = createGameDto.AwayTeamID
            };
            await _repo.games.AddAsync(newGame);
            await _repo.CommitSave();
            return newGame;
        }
        public async Task<Game> EditGame(int id, EditGameDto editGameDto)
        {
            Game editedGame = await GetGameById(id);
            if (editedGame != null)
            {
                if (editedGame.WinningTeam != editGameDto.WinningTeamID) { editedGame.WinningTeam = editGameDto.WinningTeamID; }
                if (editedGame.HomeScore != editGameDto.HomeScore) { editedGame.HomeScore = editGameDto.HomeScore; }
                if (editedGame.AwayScore != editGameDto.AwayScore) { editedGame.AwayScore = editGameDto.AwayScore; }
                await _repo.CommitSave();
            }
            return editedGame;
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
        public async Task<EquipmentRequest> CreateEquipmentRequest(CreateEquipmentRequestDto createEquipmentRequestDto)
        {
            EquipmentRequest newEquipmentRequest = new EquipmentRequest()
            {
                UserID = createEquipmentRequestDto.UserID,
                TeamID = createEquipmentRequestDto.TeamID,
                RequestDate = createEquipmentRequestDto.RequestDate,
                Message = createEquipmentRequestDto.Message,
                ItemId = createEquipmentRequestDto.ItemID,
                Status = createEquipmentRequestDto.Status
            };
            await _repo.equipmentRequests.AddAsync(newEquipmentRequest);
            await _repo.CommitSave();
            return newEquipmentRequest;
        }
        public async Task<EquipmentRequest> EditEquipmentRequest(int id, EditEquipmentRequestDto editEquipmentRequestDto)
        {
            EquipmentRequest editedEquipmentRequest = await GetEquipmentRequestById(id);
            if (editedEquipmentRequest != null && editedEquipmentRequest.Status != editEquipmentRequestDto.Status) { editedEquipmentRequest.Status = editEquipmentRequestDto.Status; }
            await _repo.CommitSave();
            return editedEquipmentRequest;
        }
    }
}
