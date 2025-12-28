using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApi
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        if(await EmailExists(registerDto.Email)) return BadRequest("EmailExists");

        using var hmac = new HMACSHA512(); //cryptography class
         var user = new AppUser
         {
             DisplayName = registerDto.DisplayName,
             Email = registerDto.Email,
             PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
             PasswordSalt = hmac.Key
         };

         context.Users.Add(user);
        context.SaveChanges(); // VO KODOT NEGOV E AWAIT KAJ MENE PRAVI ERROR

        return user.toDto(tokenService);
    }
    //mozes IActionResult da vratis no nemas type safety i mozes se da pratis
    //tipicno IActionResult se koristi koga imas mal endpoint
    //Koga koristis objekt kako prakanje gleda vo body-to na prakanjeto
    //koga koristis parametri gleda preku query string
    //kako objekt mozes isto preku query no treba da anotiras isto i za parametar [FromBody],[fromquery]

    [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
    
        if(user == null) return Unauthorized("Invalid email address");

        using var hmac = new HMACSHA3_512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
    
        for(var i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }
        return user.toDto(tokenService);
    }


    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
