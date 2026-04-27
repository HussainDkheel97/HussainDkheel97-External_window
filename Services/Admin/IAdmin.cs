using External_Party.Models;
using External_Party.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace External_Party.Services.Admin
{
    public interface IAdmin
    {

        Task<bool> AddUser(ExternelUserDto user);
        Task<bool> UdateUser(int UserId,ExternelUserDto userDto);
        Task<bool> DeleteUser(int UserId);

    }
}
