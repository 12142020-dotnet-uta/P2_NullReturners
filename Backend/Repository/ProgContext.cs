﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
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
        public DbSet<Message> Messages { get; set; }
        public DbSet<RecipientList> RecipientLists { get; set; }
        public DbSet<UserInbox> UserInboxes { get; set; }

        public ProgContext() { }

        public ProgContext(DbContextOptions<ProgContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=p2test;Trusted_Connection=True;");
                //options.UseSqlServer("Data Source=nullteammanager.database.windows.net;Initial Catalog=P2TeamManagerDB;User ID=nullreturnadmin;Password=ReturningNull0;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInbox>()
                .HasKey(c => new { c.UserID, c.MessageID });
            modelBuilder.Entity<RecipientList>()
                .HasKey(c => new { c.RecipientListID, c.RecipientID });
        }
    }
}
