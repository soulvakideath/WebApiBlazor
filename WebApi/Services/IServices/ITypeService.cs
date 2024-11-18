using WebApi.Dto.TypeDtos;
using WebApi.Models;

namespace WebApi.Services.IServices;

public interface ITypeService
{
    Task<IEnumerable<TypeDto>> GetTypesAsync();
    Task<TypeDto?> GetTypeByIdAsync(int id);
    Task<TypeDto> CreateTypeAsync(TypeCreateDto dto);
    Task<bool> UpdateTypeAsync(TypeUpdateDto dto);
    Task<bool> DeleteTypeAsync(int id);
    Task<bool> IsDuplicateOnCreateAsync(TypeCreateDto dto, int? existingTypeId = null);
    Task<bool> IsDuplicateOnUpdateAsync(TypeUpdateDto dto, int typeId);
}