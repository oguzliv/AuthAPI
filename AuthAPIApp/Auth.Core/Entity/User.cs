﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Entity
{
    public class User
    {
        public enum ROLE
        {
            ADMIN, USER
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required,NotMapped]
        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public ROLE Role { get; set; }
        public string RoleText
        {
            get { return Role.ToString(); }
            set { Role = (ROLE)System.Enum.Parse(typeof(ROLE), value); }
        }

        public DateTime CreatedAt { get; set; }
        public bool IsVerificationMailSent { get; set; }
        public bool IsVerified { get; set; }
    }
}
