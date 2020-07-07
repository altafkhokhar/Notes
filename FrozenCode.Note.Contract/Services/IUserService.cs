﻿using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrozenCode.Note.Contract.Services
{
    public interface IUserService
    {
            UserDTO Authenticate(ref UserDTO registeredUser);
            Task<List<UserDTO>> GetAll();
            bool TryRegister(ref RegisterUserDTO newUser);
        
    }
}
