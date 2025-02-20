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
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberApiController(
        ILogging _logger, 
        ApplicationDbContext _db, 
        IMapper _mapper,
        IVillaRepositoryNumber _villaNumberRepository
        ) : ControllerBase
    {
        protected APIResponse _response = new APIResponse();
        
        
        [HttpGet("getAllVillaNumbers")]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetAllVillaNumbers()
        {
            _response.Result = await _villaNumberRepository.GetVillas();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        
    
        [HttpGet("{id:int}", Name = "GetVillaNumberByNo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaNumberDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetVillaNumberByNo(int id)
        {
            var result = await (_villaNumberRepository.GetVillaById(u => u.VillaNo == id));
            if (result != null)
            {
                _response.Result = _mapper.Map<VillaNumberDto>(result);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            return NotFound();
        }
        

        [HttpPost("createVillaNumber")]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody]VillaNumberCreateDto villaNumberDto)
        {
            if (villaNumberDto == null)
            {
                return BadRequest(villaNumberDto);
            }
            var villaNumber = _mapper.Map<VillaNumber>(villaNumberDto);
            await _villaNumberRepository.Create(villaNumber);
            
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = villaNumber;
            return CreatedAtRoute("GetVillaById", new {id = villaNumber.VillaNo}, _response);
        }
        

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber"), ActionName("DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaNumberRepository.GetVillaById(u => u.VillaNo == id);
            if (villa == null)
            {
                return NotFound();
            }

            await _villaNumberRepository.Remove(villa);
            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccessStatusCode = true;
            
            return Ok(_response);
        }
        

        [HttpPut("update/{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto villaDto)
        {
            if (villaDto == null || villaDto.VillaNo != id)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            var model = _mapper.Map<VillaNumber>(villaDto);
            await _villaNumberRepository.Update(model);

            _response.Result = model;
            
            return Ok(_response);
        }
    }
}