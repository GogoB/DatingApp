using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = context.Users.ToList();

            return members;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = context.Users.Find(id); //only works when using primary key

            if(member == null) return NotFound();
            return member;
        }
    }
}
