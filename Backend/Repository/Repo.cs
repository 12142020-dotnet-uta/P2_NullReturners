using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public DbSet<EquipmentItem> equipmentItems;
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
            this.equipmentItems = _progContext.EquipmentItems;
            this.roles = _progContext.Roles;
            this.teams = _progContext.Teams;
            this.messages = _progContext.Messages;
            this.recipientLists = _progContext.RecipientLists;
            this.userInboxes = _progContext.UserInboxes;
            //ValidateRoleTable();
            //ValidateTeamTable();
            //ValidateUserTable();
            //ValidateEquipmentRequestTable();
            //ValidateItemTable();
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
        public async Task<EquipmentItem> GetEquipmentItemById(int id)
        {
            return await equipmentItems.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await equipmentItems.ToListAsync();
        }

        //private void ValidateRoleTable()
        //{
        //    if (roles.Count() == 0)
        //    {
        //        string[] rolenames = { "Coach", "Player", "Parent" };
        //        for (int i = 0; i < rolenames.Length; i++)
        //        {
        //            Role newrole = new Role()
        //            {
        //                RoleName = $"{rolenames[i]}"
        //            };
        //            roles.Add(newrole);
        //        }
        //        _progContext.SaveChanges();
        //    }
        //}
        //private void ValidateTeamTable()
        //{
        //    if (teams.Count() == 0)
        //    {
        //        string[] teamnames = { "Milwaukee Beers", "Dallas Felons", "New Jersey Informants", "Roswell Aliens", "Miami Dealers", "Los Angeles Riots" };
        //        for (int i = 0; i < teamnames.Length; i++)
        //        {
        //            Team newteam = new Team()
        //            {
        //                Name = $"{teamnames[i]}"
        //            };
        //            teams.Add(newteam);
        //        }
        //        _progContext.SaveChanges();
        //    }
        //}
        //private void ValidateUserTable()
        //{
        //    if (users.Count() == 0)
        //    {
        //        string[] userNames = { "CooperJoe", "RemerDoug", "ScolariKenny", "MartinRobert", "UnderwoodCarolyn", "CooperGavin", "RemerMax", "ScolariLiam", "MartinStewart", "UnderwoodColin", "WalshSteven",
        //                               "KingLeah", "VaughnMax", "GrantMelanie", "RobertsWilliam", "BakerAndrew", "KingBrandon", "VaughanRobert", "GrantRichard", "RobertsAdam", "BakerDominic", "AbrahamVictor",
        //                               "PooleAmy", "DuncanJames", "LawrenceGabrielle", "RossJake", "CarrMegan", "PooleEvan", "DuncanKeith", "LawrenceJulian", "RossNicholas", "CarrCameron", "AlsopDiana",
        //                               "SmithFelicity", "OgdenIsaac", "JacksonRebecca", "VanceWanda", "MillsConnor", "SmithRyan", "OgdenNathan", "JacksonJoseph", "VanceChristopher", "MillsDylan", "ClarkJames",
        //                               "NashAlan", "WallaceClaire", "MackenzieRobert", "HarrisSally", "CameronSimon", "NashEric", "WallaceMichael", "MackenzieKevin", "HarrisOliver", "CameronStephen", "PiperAndrew"};
        //        string[] passwords = { "nonmeteorically", "airsickness", "exemplarily", "nonsobriety", "ileocolitis", "tittivating", "preconcentrating", "vascularization", "semidigested", "strumectomy", "fumigate",
        //                               "overprolific", "geometrically", "superoccipital", "unilaterality", "sycophantishly", "readvertising", "unsplendid", "rattlebrained", "overannotate", "preprophetic", "reemphasize",
        //                               "gelatinity", "nonforbearing", "carnation", "saliferous", "nonnobility", "diaeresis", "safekeeping", "overobjectify", "macmahon", "thionine", "uncompressible",
        //                               "uncontemplable", "undeficient", "speciousness", "contradicter", "prosternal", "preremunerating", "immeasurableness", "disembodying", "undevious", "impatiently", "carbonating",
        //                               "exorability", "epimysium", "preconsumed", "refulgentness", "whitechapel", "pecuniarily", "complicatedness", "cingalese", "unnautical", "haemagglutinate", "nonionized"};
        //        List<byte[]> hashes = new List<byte[]>();
        //        List<byte[]> salts = new List<byte[]>();
        //        for (int i = 0; i < userNames.Length; i++)
        //        {
        //            using var hmac = new HMACSHA512();
        //            hashes.Add(hmac.ComputeHash(Encoding.UTF8.GetBytes(passwords[i])));
        //            salts.Add(hmac.Key);
        //            hmac.Dispose();
        //        }
        //        string[] names = {  "Joe Cooper", "Doug Remer", "Kenny Scolari", "Robert Martin", "Carolyn Underwood", "Gavin Cooper", "Max Remer", "Liam Scolari", "Stewart Martin", "Colin Underwood", "Steven Walsh",
        //                            "Leah King", "Max Vaughan", "Melanie Grant", "William Roberts", "Andrew Baker", "Brandon King", "Robert Vaughan", "Richard Grant", "Adam Roberts", "Dominic Baker", "Victor Abraham",
        //                            "Amy Poole", "James Duncan", "Gabrielle	Lawrence", "Jake Ross", "Megan Carr", "Evan Poole", "Keith Duncan", "Julian Lawrence", "Nicholas Ross", "Cameron Carr", "Diana Alsop",
        //                            "Felicity Smith", "Isaac Ogden", "Rebecca Jackson", "Wanda Vance", "Connor Mills", "Ryan Smith", "Nathan Ogden", "Joseph Jackson", "Christopher Vance", "Dylan Mills", "James Clark",
        //                            "Alan Nash", "Claire Wallace", "Robert Mackenzie", "Sally Harris", "Simon Cameron", "Eric Nash", "Michael Wallace", "Kevin Mackenzie", "Oliver Harris", "Stephen Cameron", "Andrew Piper"};
        //        string[] phonenumbers = { "414-555-6548", "414-555-6453", "414-555-1056", "414-555-3546", "414-555-4356", "414-555-3685", "414-555-3257", "414-555-3428", "414-555-7839", "414-555-4523", "414-555-3658",
        //                                  "469-555-7382", "469-555-1354", "469-555-3876", "469-555-5613", "469-555-7862", "469-555-7485", "469-555-5436", "469-555-5423", "469-555-6987", "469-555-3652", "469-555-4387",
        //                                  "973-555-5368", "973-555-4138", "973-555-1687", "973-555-8146", "973-555-2839", "973-555-9825", "973-555-2568", "973-555-7891", "973-555-3568", "973-555-1568", "973-555-1654",
        //                                  "575-555-8432", "575-555-3257", "575-555-0257", "575-555-0452", "575-555-2104", "575-555-5269", "575-555-2578", "575-555-2058", "575-555-2581", "575-555-4378", "575-555-3486",
        //                                  "786-555-6753", "786-555-6748", "786-555-8912", "786-555-4237", "786-555-3458", "786-555-3285", "786-555-7856", "786-555-5287", "786-555-6587", "786-555-2876", "786-555-0281"};
        //        string[] emails = { "CooperJoe@MilwaukeeBeers.com", "RemerDoug@MilwaukeeBeers.com", "ScolariKenny@MilwaukeeBeers.com", "MartinRobert@MilwaukeeBeers.com", "UnderwoodCarolyn@MilwaukeeBeers.com", "CooperGavin@MilwaukeeBeers.com", "RemerMax@MilwaukeeBeers.com", "ScolariLiam@MilwaukeeBeers.com", "MartinStewart@MilwaukeeBeers.com", "UnderwoodColin@MilwaukeeBeers.com", "WalshSteven@MilwaukeeBeers.com",
        //                            "KingLeah@DallasFelons.com", "VaughnMax@DallasFelons.com", "GrantMelanie@DallasFelons.com", "RobertsWilliam@DallasFelons.com", "BakerAndrew@DallasFelons.com", "KingBrandon@DallasFelons.com", "VaughanRobert@DallasFelons.com", "GrantRichard@DallasFelons.com", "RobertsAdam@DallasFelons.com", "BakerDominic@DallasFelons.com", "AbrahamVictor@DallasFelons.com",
        //                            "PooleAmy@NewJerseyInformants.com", "DuncanJames@NewJerseyInformants.com", "LawrenceGabrielle@NewJerseyInformants.com", "RossJake@NewJerseyInformants.com", "CarrMegan@NewJerseyInformants.com", "PooleEvan@NewJerseyInformants.com", "DuncanKeith@NewJerseyInformants.com", "LawrenceJulian@NewJerseyInformants.com", "RossNicholas@NewJerseyInformants.com", "CarrCameron@NewJerseyInformants.com", "AlsopDiana@NewJerseyInformants.com",
        //                            "SmithFelicity@RoswellAliens.com", "OgdenIsaac@RoswellAliens.com", "JacksonRebecca@RoswellAliens.com", "VanceWanda@RoswellAliens.com", "MillsConnor@RoswellAliens.com", "SmithRyan@RoswellAliens.com", "OgdenNathan@RoswellAliens.com", "JacksonJoseph@RoswellAliens.com", "VanceChristopher@RoswellAliens.com", "MillsDylan@RoswellAliens.com", "ClarkJames@RoswellAliens.com",
        //                            "NashAlan@MiamiDealers.com", "WallaceClaire@MiamiDealers.com", "MackenzieRobert@MiamiDealers.com", "HarrisSally@MiamiDealers.com", "CameronSimon@MiamiDealers.com", "NashEric@MiamiDealers.com", "WallaceMichael@MiamiDealers.com", "MackenzieKevin@MiamiDealers.com", "HarrisOliver@MiamiDealers.com", "CameronStephen@MiamiDealers.com", "PiperAndrew@MiamiDealers.com"};
        //        int[] teamids = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        //                          2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
        //                          3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
        //                          4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
        //                          5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};                
        //        int[] roleids = { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1,
        //                          3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1,
        //                          3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1,
        //                          3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1,
        //                          3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1};
        //        for (int i = 0; i < userNames.Length; i++)
        //        {
        //            User newuser = new User()
        //            {
        //                UserName = $"{userNames[i]}",
        //                Password = $"{passwords[i]}",
        //                PasswordHash = hashes.ElementAt(i),
        //                PasswordSalt = salts.ElementAt(i),
        //                FullName = $"{names[i]}",
        //                PhoneNumber = $"{phonenumbers[i]}",
        //                Email = $"{emails[i]}",
        //                TeamID = teamids[i],
        //                RoleID = roleids[i]
        //            };
        //            users.Add(newuser);
        //        }
        //        _progContext.SaveChanges();
        //    }
        //}
        //private void ValidateEquipmentRequestTable()
        //{
        //    if (equipmentRequests.Count() == 0)
        //    {
        //        User user1 = users.FirstOrDefault(x => x.UserName == "CooperJoe");
        //        User user2 = users.FirstOrDefault(x => x.UserName == "ScolariKenny");
        //        User user3 = users.FirstOrDefault(x => x.UserName == "PooleAmy");
        //        Guid[] userlist = { user1.UserID, user2.UserID, user3.UserID };
        //        int[] teamslist = { 1, 1, 3 };
        //        DateTime[] requesttimes = { DateTime.Now, DateTime.Now, DateTime.Now };
        //        string[] messageslist = { "Gavin's helmet is dented", "Liam somehow managed to lose his shoulderpads", "Evan's jersey ripped last Saturday" };
        //        int[] items = { 1, 2, 3 };
        //        string[] status = { "App. Pending", "Fulfilled", "Approved" };
        //        for (int i = 0; i < userlist.Length; i++)
        //        {
        //            EquipmentRequest newrequest = new EquipmentRequest()
        //            {
        //                UserID = userlist[i],
        //                TeamID = teamslist[i],
        //                RequestDate = requesttimes[i],
        //                Message = $"{messageslist[i]}",
        //                ItemId = items[i],
        //                Status = status[i]
        //            };
        //            equipmentRequests.Add(newrequest);
        //        }
        //        _progContext.SaveChanges();
        //    }
        //}
        //private void ValidateItemTable()
        //{
        //    if (equipmentItems.Count() == 0)
        //    {
        //        string[] description = { "Helmet", "Shoulderpads", "Jersey" };
        //        for (int i = 0; i < description.Length; i++)
        //        {
        //            EquipmentItem newItem = new EquipmentItem()
        //            {
        //                Description = $"{description[i]}"
        //            };
        //            equipmentItems.Add(newItem);
        //        }
        //        _progContext.SaveChanges();
        //    }
        //}
    }
}