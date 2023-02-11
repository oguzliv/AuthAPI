using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Persistence.Entity
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
        [Required, NotMapped]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsVerificationMailSent { get; set; }
        public bool IsVerified { get; set; }
    }
}
