using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThesisApp.API.Data;
using ThesisApp.API.Models.Device;

namespace ThesisApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ThesisAppDbContext _context;
        private readonly IMapper _mapper;

        public DevicesController(ThesisAppDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceReadOnlyDto>>> GetDevices()
        {
            var devicesDtos = await _context.Devices.ProjectTo<DeviceReadOnlyDto>(_mapper.ConfigurationProvider).ToListAsync();
            return devicesDtos;
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDetailsDto>> GetDevice(int id)
        {
            var devices = await _context.Devices
                .Include(q => q.AssignedUsers)
                .ThenInclude(q => q.User)
                .Include(q => q.Room)
                .FirstOrDefaultAsync(q => q.Id == id);

            var deviceDto = _mapper.Map<DeviceDetailsDto>(devices);

            if (deviceDto == null)
            {
                return NotFound();
            }

            return Ok(deviceDto);
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, DeviceUpdateDto deviceDto)
        {
            if (id != deviceDto.Id)
            {
                return BadRequest();
            }

            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            _mapper.Map(deviceDto, device);
            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DeviceExists(id))
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

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeviceCreateDto>> PostDevice(DeviceCreateDto deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);

            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> DeviceExists(int id)
        {
            return await _context.Devices.AnyAsync(e => e.Id == id);
        }
    }
}
