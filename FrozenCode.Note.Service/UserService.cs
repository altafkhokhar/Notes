using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Entities;
using FrozenCode.Note.Contract.Services;
using FrozenCode.Note.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrozenCode.Note.Service
{

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private SockoNotesContext dbContext = new SockoNotesContext();

        public UserService()
        {

        }

        public UserDTO Authenticate(ref UserDTO registeredUser)
        {
            string userName = registeredUser.UserName, password = registeredUser.Password;

            var user = dbContext.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
                return null;

            return new UserDTO { Id = user.Id, UserName = user.UserName };
        }

        public bool TryRegister(ref RegisterUserDTO newUser)
        {
            try
            {
                string userName = newUser.UserName.Trim();

                if (dbContext.Users.FirstOrDefault(x => x.UserName == userName) != null)
                {
                    return false;
                }

                dbContext.Users.Add(new Users() { UserName = newUser.UserName.Trim(), Email = newUser.Email.Trim(), Password = newUser.Password.Trim(), Salt = string.Empty });
                dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

        public Task<List<UserDTO>> GetAll()
        {
            var result = dbContext.Users.Select(sel => new UserDTO { UserName = sel.UserName, Email = sel.Email }).ToListAsync();
            return result;
        }

        public Task<List<UserDTO>> Search(string searchText)
        {
            var result = dbContext.Users.Where(wh => wh.UserName.Contains(searchText) || wh.Email.Contains(searchText))
            .Select(sel => new UserDTO {Id = sel.Id, UserName = sel.UserName, Email = sel.Email }).ToListAsync();
            return result;
        }
    }
}
