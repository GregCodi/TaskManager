using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public byte[]? Photo { get; set; }
        public UserStatus Status { get; set; }

        public UserModel(string firtsName, string lastName, string email, string password,
            UserStatus status, string phone)
        {
            FirtsName = firtsName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            RegistrationTime = DateTime.Now;
            Status = status;
        }
        public UserModel() { }
    }

}
