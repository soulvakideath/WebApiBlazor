using WebApi.Dto.OperationDto;
using WebApi.Models;

namespace WebApi.Services.IServices;

public interface IOperationService
{
    Task<IEnumerable<OperationDto>> GetAllAsync();
    Task<OperationDto?> GetByIdAsync(int id);
    Task<OperationDto> CreateAsync(OperationCreateDto dto);
    Task<bool> UpdateAsync(OperationUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> IsDuplicateOnCreateAsync(OperationCreateDto dto, int? existingOperationId = null);
    Task<bool> IsDuplicateOnUpdateAsync(OperationUpdateDto dto, int operationId);
}