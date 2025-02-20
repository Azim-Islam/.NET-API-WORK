using System.Net;
using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Logging;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    
    // [Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController(
        ILogging _logger, 
        ApplicationDbContext _db, 
        IMapper _mapper,
        IVillaRepository _villaRepository
        ) : ControllerBase
    {
        protected APIResponse _response = new APIResponse();
        
        
        [HttpGet("getAllVillas")]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetAllVilla()
        {
            _response.Result = await _villaRepository.GetVillas();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        
    
        [HttpGet("{id:int}", Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            var result = await (_villaRepository.GetVillaById(u => u.Id == id));
            if (result != null)
            {
                _response.Result = _mapper.Map<VillaDto>(result);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            return NotFound();
        }
        

        [HttpPost("createVilla")]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            var villa = _mapper.Map<Villa>(villaDto);
            await _villaRepository.Create(villa);
            
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = villa;
            return CreatedAtRoute("GetVillaById", new {id = villa.Id}, _response);
        }
        

        [HttpDelete("{id:int}", Name = "DeleteVilla"), ActionName("DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepository.GetVillaById(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            await _villaRepository.Remove(villa);
            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccessStatusCode = true;
            
            return Ok(_response);
        }
        

        [HttpPut("update/{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (villaDto == null || villaDto.Id != id)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            var model = _mapper.Map<Villa>(villaDto);
            await _villaRepository.Update(model);

            _response.Result = model;
            
            return Ok(_response);
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