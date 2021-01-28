using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicClass
    {
        public LogicClass() { }
        public LogicClass(Repo repo, Mapper mapper, ITokenService token, ILogger<Repo> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _token = token;
            _logger = logger;
        }
        private readonly Repo _repo;
        private readonly Mapper _mapper;
        private readonly ITokenService _token;
        private readonly ILogger<Repo> _logger;

        // Context accessors
        public async Task<User> GetUserById(Guid id)
        {
            return await _repo.GetUserById(id);
        }
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            IEnumerable<User> users = await _repo.GetUsers();
            List<UserDto> userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                UserDto userDto = _mapper.ConvertUserToUserDto(user);
                userDtos.Add(userDto);
            }

            return userDtos;
        }
        //Users 


        // can likely be deleted
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



        // DANIEL TESTING
        public async Task<UserLoggedInDto> RegisterUser(CreateUserDto createUser)
        {
            using var hmac = new HMACSHA512();

            User user = new User()
            {
                    UserName = createUser.UserName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(createUser.Password)),
                    PasswordSalt = hmac.Key,
                    FullName = createUser.FullName,
                    PhoneNumber = createUser.PhoneNumber,
                    Email = createUser.Email,
                    TeamID = createUser.TeamID,
                    RoleID = createUser.RoleID
            };
            await _repo.users.AddAsync(user);
            await _repo.CommitSave();
            _logger.LogInformation("User created");


            UserLoggedInDto newUser = _mapper.ConvertUserToUserLoggedInDto(user);
            newUser.Token = _token.CreateToken(user);
            return newUser;
        }

        // testing if user by username or email exists
        public async Task<bool> UserExists(string username, string email)
        {
            bool userExists = await _repo.users.AnyAsync(x => x.UserName == username || x.Email == email);
            if (userExists)
            {
                _logger.LogInformation("User found in database");
                return userExists;
            }
            return userExists;
        }

        public async Task<User> LoginUser(LoginDto loginDto)
        {
            return await _repo.users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
        }

        public async Task<UserLoggedInDto> CheckPassword(Task<User> user, LoginDto loginDto)
        {
            using var hmac = new HMACSHA512(user.Result.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Result.PasswordHash[i])
                {
                    return null;
                }
            }

            User loggedIn = await user;

            UserLoggedInDto loggedInUser = _mapper.ConvertUserToUserLoggedInDto(loggedIn);
            loggedInUser.Token = _token.CreateToken(loggedIn);
            return loggedInUser;
        }


        // END TESTING

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
            Mapper mapper = new Mapper(); // needed to use this because _mapper was null
            Play newPlay = new Play()
            {
                PlaybookId = playDto.PlaybookID,
                Name = playDto.Name,
                Description = playDto.Description,
                DrawnPlay = mapper.ConvertImage(playDto.ImageString)
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
        public async Task<RecipientList> GetRecipientListById(Guid listId, Guid recId)
        {
            return await _repo.GetRecipientListById(listId, recId);
        }
        public async Task<IEnumerable<RecipientList>> GetRecipientLists()
        {
            return await _repo.GetRecipientLists();
        }
        public async Task<RecipientList> BuildRecipientList(Guid listId, Guid recId)
        {
            RecipientList rL = new RecipientList()
            {
                RecipientListID = listId,
                RecipientID = recId
            };
            await _repo.recipientLists.AddAsync(rL); 
            await _repo.CommitSave();
            return rL;
        }
        public async Task<Message> CreateNewMessage(NewMessageDto newMessageDto)
        {
            Message newMessage = new Message()
            {
                SenderID = newMessageDto.SenderID,
                RecipientListID = Guid.NewGuid(),
                MessageText = newMessageDto.MessageText
            };
            foreach (Guid id in newMessageDto.RecipientList)
            {
                await BuildRecipientList(newMessage.RecipientListID, id);
            }
            await _repo.messages.AddAsync(newMessage);
            await _repo.CommitSave();
            return newMessage;
        }
        public async Task<bool> SendMessage(Message message)
        {            
            List<Guid> recipientList = new List<Guid>();
            bool success;
            foreach (RecipientList r in _repo.recipientLists)
            {
                if (r.RecipientListID == message.RecipientListID)
                {
                    recipientList.Add(r.RecipientID);
                    
                }
            }
            foreach (Guid r in recipientList)
            {
                await CreateUserInbox(r, message.MessageID);
            }
            success = true;
            await _repo.CommitSave();
            return success;
        }
        public async Task<IEnumerable<UserInbox>> GetUserInbox(Guid userId)
        {
            return await _repo.GetUserInbox(userId);
        }
        public async Task<UserInbox> CreateUserInbox(Guid userId, Guid messageId)
        {
            UserInbox uI = new UserInbox()
            {
                UserID = userId,
                MessageID = messageId,
                IsRead = false
            };
            await _repo.userInboxes.AddAsync(uI);
            await _repo.CommitSave();
            return uI;
        }
        public async Task DeleteMessageFromInbox(Guid userId, Guid messageId)
        {
            foreach (UserInbox u in _repo.userInboxes)
            {
                if (u.UserID == userId && u.MessageID == messageId)
                {
                    _repo.userInboxes.Remove(u);
                }
            }
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
                if (editedGame.Statistic1 != editGameDto.Statistic1) { editedGame.Statistic1 = editGameDto.Statistic1; }
                if (editedGame.Statistic2 != editGameDto.Statistic2) { editedGame.Statistic2 = editGameDto.Statistic2; }
                if (editedGame.Statistic3 != editGameDto.Statistic3) { editedGame.Statistic3 = editGameDto.Statistic3; }
                await _repo.CommitSave();
            }
            return editedGame;
        }
        //Calendar
        public static async Task<CalendarService> InitializeCalendar()
        {
            string jsonFile = "p2nullreturners-997092916366.json";
            string[] Scopes = { CalendarService.Scope.Calendar };

            ServiceAccountCredential credential;

            await using (var stream =
                new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "P2NullReturners",
            });
            
            return service;
        }
        public static async Task<Calendar> GetCalendar(CalendarService service)
        {
            string calendarId = @"a6jdhdbp5mpv8au8mbps8qfelk@group.calendar.google.com";
            var calendar = await service.Calendars.Get(calendarId).ExecuteAsync();
            return calendar;
        }
        public static async Task<IEnumerable<Event>> GetMyEvents(CalendarService service)
        {
            string calendarId = @"a6jdhdbp5mpv8au8mbps8qfelk@group.calendar.google.com";
            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            listRequest.TimeMin = DateTime.Now;
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            Events events = await listRequest.ExecuteAsync();
            List<Event> eventList = new List<Event>();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    eventList.Add(eventItem);
                }
            }
            return eventList;
        }
        public static async Task<Event> CreateEvent(CalendarService service, EventDto eventDto)
        {
            string calendarId = @"a6jdhdbp5mpv8au8mbps8qfelk@group.calendar.google.com";
            var myevent = new Event()
            {
                Id = eventDto.EventID.ToString(),
                Start = eventDto.StartTime,
                End = eventDto.EndTime,
                Summary = eventDto.Description,
                Description = eventDto.Message
            };
            var InsertRequest = service.Events.Insert(myevent, calendarId);
            try
            {
                await InsertRequest.ExecuteAsync();
            }
            catch (Exception)
            {
                try
                {
                    await service.Events.Update(myevent, calendarId, myevent.Id).ExecuteAsync();
                }
                catch (Exception)
                {
                    myevent = new Event();
                }
            }
            return myevent;
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
        public async Task<EquipmentRequest> GetEquipmentItemtById(int id)
        {
            return await _repo.GetEquipmentItemById(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentItems()
        {
            return await _repo.GetEquipmentItems();
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
        public async Task<EquipmentItem> GetEquipmentItemByName(string eqName)
        {
            EquipmentItem eqItem = await _repo.equipmentItems.FirstOrDefaultAsync(x => x.Description == eqName);
            return eqItem;
        }
    }
}