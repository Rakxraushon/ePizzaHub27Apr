using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {

        }
        public bool CreateUser(User user, string Role)
        {
            try
            {
                Role role = _db.Roles.Where(r => r.Name == Role).FirstOrDefault();
                user.Roles.Add(role);

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            try
            {
                User model = _db.Users.Include(u => u.Roles).Where(u => u.Email == Email).FirstOrDefault();
                if (model != null)
                {
                    var isVerified = BCrypt.Net.BCrypt.Verify(Password, model.Password);
                    if (isVerified)
                    {
                        UserModel user = new UserModel
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            Roles = model.Roles.Select(r => r.Name).ToArray()
                        };
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
