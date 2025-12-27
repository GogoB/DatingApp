using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    public class MembersController(AppDbContext context) : BaseApi
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
