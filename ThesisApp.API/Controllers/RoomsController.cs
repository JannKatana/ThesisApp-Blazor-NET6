using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThesisApp.API.Data;
using ThesisApp.API.Models.Room;

namespace ThesisApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ThesisAppDbContext _context;
        private readonly IMapper _mapper;

        public RoomsController(ThesisAppDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomReadOnlyDto>>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var roomDtos = _mapper.Map<IEnumerable<RoomReadOnlyDto>>(rooms);
            return Ok(roomDtos);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailsDto>> GetRoom(int id)
        {
            var room = await _context.Rooms
                .Include(q => q.Devices)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            var roomDto = _mapper.Map<RoomDetailsDto>(room);
            return Ok(roomDto);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomUpdateDto roomDto)
        {
            if (id != roomDto.Id)
            {
                return BadRequest();
            }

            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            _mapper.Map(roomDto, room);
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomCreateDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> RoomExists(int id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }
    }
}
