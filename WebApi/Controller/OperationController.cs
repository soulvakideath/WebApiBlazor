using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.OperationDto;
using WebApi.Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var operations = await _operationService.GetAllAsync();
            if (!operations.Any())
                return Ok(new List<OperationDto>());

            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var operation = await _operationService.GetByIdAsync(id);
            if (operation is null)
                return NotFound();
            return Ok(operation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationCreateDto operationCreateDto)
        {
            if (await _operationService.IsDuplicateOnCreateAsync(operationCreateDto))
                return Conflict("Operation with the same details already exists.");

            var createdOperation = await _operationService.CreateAsync(operationCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdOperation.Id }, new { id = createdOperation.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OperationUpdateDto operationUpdateDto)
        {
            if (await _operationService.IsDuplicateOnUpdateAsync(operationUpdateDto, id))
                return Conflict("Duplicate operation.");

            var updated = await _operationService.UpdateAsync(operationUpdateDto);
            if (!updated)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var operation = await _operationService.GetByIdAsync(id);
            if (operation is null)
                return NotFound();

            var deleted = await _operationService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Unable to delete the operation due to business rules or constraints.");

            return Ok(deleted);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetTypes()
        {
            var operations = await _operationService.GetAllAsync();
            if (!operations.Any())
                return Ok(new List<OperationDto>());

            return Ok(operations);
        }
    }
}
