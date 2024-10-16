using TaskManager.API.Models.Abstractions;
using TaskManager.Common.Models;

namespace TaskManager.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string Phone { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastLoginTime { get; set;}
        public byte[]? Photo { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<Task> Tasks { get; set; } = new List<Task>();
        public UserStatus Status { get; set; }
        public User() { }
        public User(string firtsName, string lastName, string email, string password,
            UserStatus status = UserStatus.User, string phone = null, byte[] photo = null)
        {
            FirtsName = firtsName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            Photo = photo;
            RegistrationTime = DateTime.Now;
            Status = status;
        }

        public User(UserModel model)
        {
            Id = model.Id;
            FirtsName = model.FirtsName;
            LastName = model.LastName;
            Email = model.Email;
            Password = model.Password; 
            Phone = model.Phone;
            Photo = model.Photo;
            RegistrationTime = model.RegistrationTime;
            Status = model.Status;
        }

        public UserModel ToDto()
        {
            return new UserModel()
            {
                Id = this.Id,
                FirtsName = this.FirtsName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                Phone = this.Phone,
                Photo = this.Photo,
                RegistrationTime = this.RegistrationTime,
                Status = this.Status
            };
        }
    }
}
