using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Repository;
using EStoreAPI.Model;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        private readonly IMemberRepository _memberRepository;
        public MembersController( IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return Ok(await _memberRepository.GetMembers());
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member?>> GetMember(int id)
        {
            return Ok(await _memberRepository.GetMember(id));
        }

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id,[FromBody] MemberModel memberModel)
        {
            var member = new Member
            {
                Email = memberModel.Email,
                CompanyName = memberModel.CompanyName,
                City = memberModel.City,
                Country = memberModel.Country,
                Password = memberModel.Password
            };
            return Ok(await _memberRepository.UpdateMember(id, member));
        }

        // POST: api/Members
        [HttpPost]
        public async Task<ActionResult<Member>> AddMember([FromBody]MemberModel memberModel)
        {
            try
            {
                var member = new Member
                {
                    Email = memberModel.Email,
                    CompanyName = memberModel.CompanyName,
                    City = memberModel.City,
                    Country = memberModel.Country,
                    Password = memberModel.Password
                };
                var result = await _memberRepository.AddMember(member);
                if (result > 0)
                {
                    return Ok(member); 
                }
                return BadRequest("Failed to add member.");
            }
            catch (Exception ex)
            {                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            return Ok( await _memberRepository.DeleteMember(id));
        }

        [HttpPost("login")]
        public async Task<ActionResult<Member?>> Login([FromBody] LoginModel member)
        {
            return Ok(await _memberRepository.Login(member.Email, member.Password));
        }
    }
}
