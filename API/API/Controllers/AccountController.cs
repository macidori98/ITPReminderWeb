using API.API.Interfaces;
using API.API.Services;
using API.Data;
using API.DTOs;
using API.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext context;
    private readonly ITokenService tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
      this.tokenService = tokenService;
      this.context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await this.UserExists(registerDto.Name))
      {
        return BadRequest("Name already exists");
      }

      using var hmac = new HMACSHA512(); // if we want to send data in body it has to be an object

      var user = new AppUser()
      {
        Name = registerDto.Name,
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };

      this.context.Users.Add(user);
      await this.context.SaveChangesAsync();

      return new UserDto()
      {
        Name = user.Name,
        Token = this.tokenService.CreateToken(user),
      };
    }

    [HttpGet("login")]
    public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
    {
      var user = await this.context.Users
        .SingleOrDefaultAsync(x => x.Name.ToLower() == loginDto.Name.ToLower());

      if (user == null)
      {
        return Unauthorized("User does not exists");
      }

      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for (int i = 0; i < computedHash.Length; ++i)
      {
        if (computedHash[i] != user.PasswordHash[i])
        {
          return Unauthorized("Invalid password");
        }
      }

      return new UserDto() {
        Name = loginDto.Name,
        Token = this.tokenService.CreateToken(user)
      };
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<bool>> Delete (int id)
    {
      var user = await this.context.Users
        .SingleOrDefaultAsync(x => x.ID == id);

      if (user == null)
      {
        return Unauthorized("User does not exists");
      }

      try
      {
          this.context.Users.Remove(user);
          await this.context.SaveChangesAsync();
          return true;
      }
      catch (System.Exception)
      { 
          return false;
      }
    }

    private async Task<bool> UserExists(string Name)
    {
      return await this.context.Users.AnyAsync(x => x.Name.ToLower() == Name.ToLower());
    }
  }
}
