using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThesisApp.API.Data;
using ThesisApp.API.Models.User;
using ThesisApp.API.Static;

namespace ThesisApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ThesisAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ThesisAppDbContext context, IMapper mapper, ILogger<UsersController> logger)
        {
            _context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadOnlyDto>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                var userDtos = _mapper.Map<IEnumerable<UserReadOnlyDto>>(users);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing Get in {nameof(GetUsers)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDto>> GetUser(int id)
        {

            var user = await _context.Users
                .Include(u => u.AssignedDevices)
                .ThenInclude(du => du.Device)
                .ThenInclude(d => d.RoomLocation)
                .FirstOrDefaultAsync(q => q.Id == id);
    

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserReadOnlyDto>(user);
            return Ok(userDto);

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDto, user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCreateDto>> PostUser(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
    }
}
