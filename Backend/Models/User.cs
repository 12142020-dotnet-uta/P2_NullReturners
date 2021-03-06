﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("User ID")]
        public Guid UserID { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Full Name")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Phone number must be valid phone number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }
        [DisplayName("Team ID")]
        [ForeignKey("TeamID")]
        public int TeamID { get; set; }
        [DisplayName("Role ID")]
        [ForeignKey("RoleID")]
        public int RoleID { get; set; }
    }
}
