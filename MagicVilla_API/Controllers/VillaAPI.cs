using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VillaAPIMeow")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        public class VillaAPIController(ILogger<VillaAPIController> _logger)
        {
            
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetAllVilla()
        {
            return Ok(VillaStore.villaList);
        }
    
        [HttpGet("{id:int}", Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(404)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            var result = (VillaStore.villaList.FirstOrDefault(u => u.Id == id));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.villaList.Count;
            VillaStore.villaList.Add(villaDto);
            return CreatedAtRoute("GetVillaById", new {id = villaDto.Id}, villaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla"), ActionName("DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto == null || villaDto.Id != id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaDto> villaPatchDto)
        {
            if (villaPatchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            villaPatchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}