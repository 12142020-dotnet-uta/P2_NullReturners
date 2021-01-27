using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProgContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Playbook> Playbooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EquipmentRequest> EquipmentRequests { get; set; }
        public DbSet<EquipmentItem> EquipmentItems { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RecipientList> RecipientLists { get; set; }
        public DbSet<UserInbox> UserInboxes { get; set; }

        public ProgContext() { }

        public ProgContext(DbContextOptions<ProgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInbox>()
                .HasKey(c => new { c.UserID, c.MessageID });
            modelBuilder.Entity<RecipientList>()
                .HasKey(c => new { c.RecipientListID, c.RecipientID });
        }
    }
}
