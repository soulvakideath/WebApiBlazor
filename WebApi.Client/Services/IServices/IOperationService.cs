using WebApi.Client.Dto.OperationDto;
using WebApi.Client.Models;

namespace WebApi.Client.Services.IServices;

public interface IOperationService
{
    Task<List<OperationDto>> GetAllAsync();
    Task<OperationDto> GetByIdAsync(int id);
    Task<bool> CreateAsync(OperationCreateDto dto);
    Task<bool> UpdateAsync(int id, OperationUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}