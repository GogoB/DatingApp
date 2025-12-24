using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions
{
    public static UserDto toDto(this AppUser user, ITokenService service)
    {
        return new UserDto
        {
            id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = service.CreateToken(user)
        };
    }
}
