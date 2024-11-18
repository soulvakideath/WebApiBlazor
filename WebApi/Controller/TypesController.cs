using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.TypeDtos;
using WebApi.Services.IServices;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var types = await _typeService.GetTypesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var type = await _typeService.GetTypeByIdAsync(id);
            if (type is null)
                return NotFound();
            return Ok(type);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TypeCreateDto typeCreateDto)
        {
            var createdType = await _typeService.CreateTypeAsync(typeCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdType.Id }, createdType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TypeUpdateDto typeUpdateDto)
        {
            if (await _typeService.IsDuplicateOnUpdateAsync(typeUpdateDto, id))
                return Conflict("Duplicate type.");

            var updated = await _typeService.UpdateTypeAsync(typeUpdateDto);
            if (!updated)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var deleted = await _typeService.DeleteTypeAsync(id);
            if (!deleted)
                return NotFound();

            return Ok(deleted);
        }
    }
}
