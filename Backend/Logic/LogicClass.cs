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
        // Users
        /// <summary>
        /// UserID -> Repo.GetUser
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user</returns>
        public async Task<User> GetUserById(Guid id)
        {
            return await _repo.GetUserById(id);
        }
        /// <summary>
        /// Gets list of Users 
        /// </summary>
        /// <returns>List<UserDto></UserDto></returns>
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
        /// <summary>
        /// Takes user input, creates authentication data
        /// </summary>
        /// <param name="createUser">User info sent from controller</param>
        /// <returns>UserLoggedInDto</returns>
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
        /// <summary>
        /// Checks if user or email already exists in DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns>Boolean</returns>
        public async Task<bool> UserExists(string username, string email)
        {
            // should be && so that username and email are both unique right?
            bool userExists = await _repo.users.AnyAsync(x => x.UserName == username && x.Email == email);
            if (userExists)
            {
                _logger.LogInformation("User found in database");
                return userExists;
            }
            return userExists;
        }
        /// <summary>
        /// Fetches user from context
        /// </summary>
        /// <param name="loginDto">User to search for</param>
        /// <returns>User</returns>
        public async Task<User> LoginUser(LoginDto loginDto)
        {
            return await _repo.users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
        }
        /// <summary>
        /// Verifies password passed from user input
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginDto"></param>
        /// <returns>UserLoggedInDto</returns>
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
        /// <summary>
        /// Delete user from context by ID
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns>deleted User</returns>
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
        /// <summary>
        /// Add user Role to User by ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns>modified User</returns>
        public async Task<User> AddUserRole(Guid userId, int roleId)
        {
            User tUser = await GetUserById(userId);
            tUser.RoleID = roleId;
            await _repo.CommitSave();
            return tUser;
        }
        /// <summary>
        /// Checks if input data is different from existing and updates if so
        /// </summary>
        /// <param name="userId">User to edit</param>
        /// <param name="editUserDto">New information</param>
        /// <returns>modified User</returns>
        public async Task<User> EditUser(Guid userId, EditUserDto editUserDto)
        {
            User tUser = await GetUserById(userId);
            if (tUser.FullName != editUserDto.FullName && editUserDto.FullName != "") { tUser.FullName = editUserDto.FullName; }
            if (tUser.Email != editUserDto.Email && editUserDto.Email != "") { tUser.Email = editUserDto.Email; }
            if (tUser.Password != editUserDto.Password && editUserDto.Password != "") { tUser.Password = editUserDto.Password; }
            if (tUser.PhoneNumber != editUserDto.PhoneNumber && editUserDto.PhoneNumber != "") { tUser.PhoneNumber = editUserDto.PhoneNumber; }
            await _repo.CommitSave();
            return tUser;  
        }
        /// <summary>
        /// Same as above, more options for higher user level
        /// </summary>
        /// <param name="userId">User to edit</param>
        /// <param name="coachEditUserDto">New information</param>
        /// <returns>modified User</returns>
        public async Task<User> CoachEditUser(Guid userId, CoachEditUserDto coachEditUserDto)
        {
            User tUser = await GetUserById(userId);
            if (tUser.FullName != coachEditUserDto.FullName && coachEditUserDto.FullName != "") { tUser.FullName = coachEditUserDto.FullName; }
            if (tUser.Email != coachEditUserDto.Email && coachEditUserDto.Email != "") { tUser.Email = coachEditUserDto.Email; }
            if (tUser.Password != coachEditUserDto.Password && coachEditUserDto.Password != "") { tUser.Password = coachEditUserDto.Password; }
            if (tUser.PhoneNumber != coachEditUserDto.PhoneNumber && coachEditUserDto.PhoneNumber != "") { tUser.PhoneNumber = coachEditUserDto.PhoneNumber; }
            if (tUser.RoleID != coachEditUserDto.RoleID && coachEditUserDto.RoleID >= 1 && tUser.RoleID <= 3) { tUser.RoleID = coachEditUserDto.RoleID; }
            if (tUser.UserName != coachEditUserDto.UserName && coachEditUserDto.UserName != "") { tUser.UserName = coachEditUserDto.UserName; }
            await _repo.CommitSave();
            return tUser;
        }
        // Teams
        /// <summary>
        /// Get Team by ID
        /// </summary>
        /// <param name="id">TeamID</param>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamById(int id)
        {
            return await _repo.GetTeamById(id);
        }
        /// <summary>
        /// Get list of Teams
        /// </summary>
        /// <returns>Teams</returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _repo.GetTeams();
        }
        /// <summary>
        /// Edit Team
        /// </summary>
        /// <param name="id">Team to edit</param>
        /// <param name="editTeamDto">New information</param>
        /// <returns>modified Team</returns>
        public async Task<Team> EditTeam(int id, EditTeamDto editTeamDto)
        {
            Team tTeam = await GetTeamById(id);
            if (tTeam.Name != editTeamDto.Name && editTeamDto.Name != "") { tTeam.Name = editTeamDto.Name; }
            if (tTeam.Wins != editTeamDto.Wins && editTeamDto.Wins >= 0) { tTeam.Wins = editTeamDto.Wins; }
            if (tTeam.Losses != editTeamDto.Losses && editTeamDto.Losses >= 0) { tTeam.Losses = editTeamDto.Losses; }
            await _repo.CommitSave();
            return tTeam;
        }
        // Roles
        /// <summary>
        /// Get user Role
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns>RoleID</returns>
        public async Task<Role> GetRoleById(int id)
        {
            return await _repo.GetRoleById(id);
        }
        /// <summary>
        /// Get list of user Roles
        /// </summary>
        /// <returns>list of Roles</returns>
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _repo.GetRoles();
        }
        /// <summary>
        /// Edit User to change Role
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="roleId">RoleID</param>
        /// <returns>Role added</returns>
        public async Task<Role> EditUserRole(Guid userId, int roleId)
        {
            User tUser = await GetUserById(userId);
            tUser.RoleID = roleId;
            await _repo.CommitSave();
            return await GetRoleById(roleId);
        }
        // Playbooks
        /// <summary>
        /// Get Playbook
        /// </summary>
        /// <param name="id">PlaybookID</param>
        /// <returns>PlaybookID</returns>
        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await _repo.GetPlaybookById(id);
        }
        /// <summary>
        /// Get list of Playbooks
        /// </summary>
        /// <returns>list of Playbooks</returns>
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _repo.GetPlaybooks();
        }
        /// <summary>
        /// Create new Playbook and assign it to a team
        /// </summary>
        /// <param name="teamId">TeamID</param>
        /// <returns>Playbook</returns>
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
        /// <summary>
        /// Create new Play and assign it to the current Playbook
        /// </summary>
        /// <param name="playDto">new Play</param>
        /// <returns>Play</returns>
        public async Task<Play> CreatePlay(PlayDto playDto)
        {
            Play newPlay = new Play()
            {
                PlaybookId = playDto.PlaybookID,
                Name = playDto.Name,
                Description = playDto.Description,
                DrawnPlay = _mapper.ConvertImage(playDto.ImageString)
            };
            await _repo.plays.AddAsync(newPlay);
            await _repo.CommitSave();
            return newPlay;
        }
        /// <summary>
        /// Edit a Play
        /// </summary>
        /// <param name="playId">Play to edit</param>
        /// <param name="playDto">New Play info</param>
        /// <returns>edited Play</returns>
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
        /// <summary>
        /// Get Play by PlayID
        /// </summary>
        /// <param name="id">PlayID</param>
        /// <returns>Play</returns>
        public async Task<Play> GetPlayById(int id)
        {
            return await _repo.GetPlayById(id);
        }
        /// <summary>
        /// Get PlayDto by PlayID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlayDto> GetPlayDto(int id)
        {
            Play play = await _repo.GetPlayById(id);
            return _mapper.ConvertToPlayDto(play);
        }
        /// <summary>
        /// Get list of Plays
        /// </summary>
        /// <returns>list of Plays</returns>
        public async Task<IEnumerable<PlayDto>> GetPlays()
        {
            IEnumerable<Play> playList = await _repo.GetPlays();
            List<PlayDto> playDtos = new List<PlayDto>();
            foreach (var play in playList)
            {
                PlayDto playDto = _mapper.ConvertToPlayDto(play);
                playDtos.Add(playDto);
            }
            return playDtos;
        }
        /// <summary>
        /// Delete a Playbook by ID
        /// </summary>
        /// <param name="id">PlaybookID</param>
        /// <returns>deleted Playbook</returns>
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
        /// <summary>
        /// Delete a Play from a Playbook
        /// </summary>
        /// <param name="id">PlayID</param>
        /// <returns>deleted Play</returns>
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
        /// <summary>
        /// Get Message by MessageID
        /// </summary>
        /// <param name="id">MessageID</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageById(Guid id)
        {
            return await _repo.GetMessageById(id);
        }
        /// <summary>
        /// Get list of Messages
        /// </summary>
        /// <returns>list of Messages</returns>
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _repo.GetMessages();
        }
        /// <summary>
        /// Get RecipientList by ID
        /// </summary>
        /// <param name="listId">RecipientListID</param>
        /// <param name="recId">RecipientID</param>
        /// <returns>RecipientList</returns>
        public async Task<RecipientList> GetRecipientListById(Guid listId, Guid recId)
        {
            return await _repo.GetRecipientListById(listId, recId);
        }
        /// <summary>
        /// Get list of RecipientLists
        /// </summary>
        /// <returns>list of RecipientLists</returns>
        public async Task<IEnumerable<RecipientList>> GetRecipientLists()
        {
            return await _repo.GetRecipientLists();
        }
        /// <summary>
        /// Create RecipientList entity with ListID from Message and RecipientID from input
        /// </summary>
        /// <param name="listId">RecipientListID</param>
        /// <param name="recId">RecipientID</param>
        /// <returns>RecipientList</returns>
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
        /// <summary>
        /// Create a new Message
        /// </summary>
        /// <param name="newMessageDto">Message from user</param>
        /// <returns>Message</returns>
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
        /// <summary>
        /// Assign Message to Recipients via RecipientList
        /// </summary>
        /// <param name="message">Message to be assigned</param>
        /// <returns>Boolean success</returns>
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
        /// <summary>
        /// Get a UserInbox entity for given User
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>UserInbox</returns>
        public async Task<IEnumerable<UserInbox>> GetUserInbox(Guid userId)
        {
            return await _repo.GetUserInbox(userId);
        }
        /// <summary>
        /// Create a UserInbox item for assigned Message
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="messageId">MessageID</param>
        /// <returns>UserInbox</returns>
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
        /// <summary>
        /// Delete a Message from given User
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="messageId">MessageID</param>
        /// <returns></returns>
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
        // Games
        /// <summary>
        /// Get a Game by GameID
        /// </summary>
        /// <param name="id">GameID</param>
        /// <returns>Game</returns>
        public async Task<Game> GetGameById(int id)
        {
            return await _repo.GetGameById(id);
        }
        /// <summary>
        /// Get a list of Games
        /// </summary>
        /// <returns>list of Games</returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _repo.GetGames();
        }
        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="createGameDto">Game from input</param>
        /// <returns>Game</returns>
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
        /// <summary>
        /// Edit a Game
        /// </summary>
        /// <param name="id">GameID</param>
        /// <param name="editGameDto">New information</param>
        /// <returns>modified Game</returns>
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
        // Calendar
        /// <summary>
        /// Initialize Calendar service with credentials
        /// </summary>
        /// <returns>Calendar service</returns>
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
        /// <summary>
        /// Get a Calendar by ID
        /// </summary>
        /// <param name="service">Calendar service</param>
        /// <returns>Calendar</returns>
        public static async Task<Calendar> GetCalendar(CalendarService service)
        {
            string calendarId = @"a6jdhdbp5mpv8au8mbps8qfelk@group.calendar.google.com";
            var calendar = await service.Calendars.Get(calendarId).ExecuteAsync();
            return calendar;
        }
        /// <summary>
        /// Get list of Events from Calendar
        /// </summary>
        /// <param name="service">Calendar service</param>
        /// <returns>list of Events</returns>
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
        /// <summary>
        /// Create a new Event on Calendar
        /// </summary>
        /// <param name="service">Calendar service</param>
        /// <param name="eventDto">Event info from input</param>
        /// <returns>Calendar Event</returns>
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
        // Equipment
        /// <summary>
        /// Get an EquipmentRequest by ID
        /// </summary>
        /// <param name="id">EquipmentRequestID</param>
        /// <returns>EquipmentRequest</returns>
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await _repo.GetEquipmentRequestById(id);
        }
        /// <summary>
        /// Get a list of EquipmentRequests
        /// </summary>
        /// <returns>list of EquipmentRequests</returns>
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await _repo.GetEquipmentRequests();
        }
        /// <summary>
        /// Get EquipmentRequest by ID
        /// </summary>
        /// <param name="id">EquipmentRequestID</param>
        /// <returns>EquipmentRequest</returns>
        public async Task<EquipmentItem> GetEquipmentItemtById(int id)
        {
            return await _repo.GetEquipmentItemById(id);
        }
        /// <summary>
        /// Get list of EquipmentItems
        /// </summary>
        /// <returns>list of EquipmentRequests</returns>
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await _repo.GetEquipmentItems();
        }
        /// <summary>
        /// Create new EquipmentRequest
        /// </summary>
        /// <param name="createEquipmentRequestDto">EquipmentRequest from input</param>
        /// <returns>EquipmentRequest</returns>
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
        /// <summary>
        /// Edit an EquipmentRequest
        /// </summary>
        /// <param name="id">EqupmentRequestID</param>
        /// <param name="editEquipmentRequestDto">new information from input</param>
        /// <returns>modified EquipmentRequest</returns>
        public async Task<EquipmentRequest> EditEquipmentRequest(int id, EditEquipmentRequestDto editEquipmentRequestDto)
        {
            EquipmentRequest editedEquipmentRequest = await GetEquipmentRequestById(id);
            if (editedEquipmentRequest != null && editedEquipmentRequest.Status != editEquipmentRequestDto.Status) { editedEquipmentRequest.Status = editEquipmentRequestDto.Status; }
            await _repo.CommitSave();
            return editedEquipmentRequest;
        }
        /// <summary>
        /// Get an EquipmentItem by Name
        /// </summary>
        /// <param name="eqName">Equipment Name</param>
        /// <returns>EquipmentItem</returns>
        public async Task<EquipmentItem> GetEquipmentItemByName(string eqName)
        {
            return await _repo.equipmentItems.FirstOrDefaultAsync(x => x.Description == eqName);
        }
    }
}