using API.Data;
using API.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class UserController : BaseApiController
  {
    private readonly DataContext context;

    public UserController(DataContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
      return await this.context.Users.ToListAsync();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
      var user = await this.context.Users.FindAsync(id);
      if (user == null)
      {
        return BadRequest("User doesnt exists");
      }
      else
      {
        return user;
      }
    }
  }
}