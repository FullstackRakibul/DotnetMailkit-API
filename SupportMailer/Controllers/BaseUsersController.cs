using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportMailer.Models;

namespace SupportMailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUsersController : ControllerBase
    {
        private readonly SupportMailerContext _context;

        public BaseUsersController(SupportMailerContext context)
        {
            _context = context;
        }

        // GET: api/BaseUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseUser>>> GetBaseUsers()
        {
          if (_context.BaseUsers == null)
          {
              return NotFound();
          }
            return await _context.BaseUsers.ToListAsync();
        }

        // GET: api/BaseUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseUser>> GetBaseUser(int id)
        {
          if (_context.BaseUsers == null)
          {
              return NotFound();
          }
            var baseUser = await _context.BaseUsers.FindAsync(id);

            if (baseUser == null)
            {
                return NotFound();
            }

            return baseUser;
        }

        // PUT: api/BaseUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseUser(int id, BaseUser baseUser)
        {
            if (id != baseUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(baseUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BaseUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaseUser>> PostBaseUser(BaseUser baseUser)
        {
          if (_context.BaseUsers == null)
          {
              return Problem("Entity set 'SupportMailerContext.BaseUsers'  is null.");
          }
            _context.BaseUsers.Add(baseUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseUser", new { id = baseUser.UserId }, baseUser);
        }

        // DELETE: api/BaseUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaseUser(int id)
        {
            if (_context.BaseUsers == null)
            {
                return NotFound();
            }
            var baseUser = await _context.BaseUsers.FindAsync(id);
            if (baseUser == null)
            {
                return NotFound();
            }

            _context.BaseUsers.Remove(baseUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BaseUserExists(int id)
        {
            return (_context.BaseUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
