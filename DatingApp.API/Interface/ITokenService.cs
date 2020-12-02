using System;
using DatingApp.API.Models;

namespace DatingApp.API.Interface
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
