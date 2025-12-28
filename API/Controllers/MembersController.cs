using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class MembersController(IMemberRepository memberRepository) : BaseApi
    {
        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            //wrapping so Ok ti unistuva type safety duri i kaj Member da smenis ke bide errorless poradi Ok
            return Ok( await memberRepository.GetMembersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id); //only works when using primary key

            if(member == null) return NotFound();
            return member;
        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(string id)
        {
            return  Ok( await memberRepository.GetPhotoForMemberAsync(id));
        }

    }
}
