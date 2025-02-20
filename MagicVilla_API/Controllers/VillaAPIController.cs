using MagicVilla_API.Data;
using MagicVilla_API.Logging;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController(ILogging _logger, ApplicationDbContext _db) : ControllerBase
    {
        [HttpGet("getAllVillas")]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetAllVilla()
        {
            return Ok(_db.Villas.ToList());
        }
    
        [HttpGet("{id:int}", Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            var result = await (_db.Villas.FirstOrDefaultAsync(u => u.Id == id));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("createVilla")]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaCreateDTO villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            
            Villa model = new Villa
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Image = villaDto.Image
            };
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVillaById", new {id = model.Id}, villaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla"), ActionName("DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            return NoContent();
        }

        [HttpPut("update/{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (villaDto == null || villaDto.Id != id)
            {
                // logger.Log(LogLevel.Error, "The villa patch did not provide a valid id");
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Image = villaDto.Image
            };
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPut("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaDto> villaPatchDto)
        {
            if (villaPatchDto == null || id == 0)
            {
                return BadRequest();
            }
            
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                Image = villa.Image
            };
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            villaPatchDto.ApplyTo(villaDto, ModelState);

            Villa model = new Villa()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                Image = villaDto.Image
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            
            return NoContent();
        }
    }
}