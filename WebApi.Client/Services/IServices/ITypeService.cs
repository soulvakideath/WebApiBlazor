using WebApi.Client.Dto.TypeDtos;
using WebApi.Client.Models;

namespace WebApi.Client.Services.IServices;

    public interface ITypeService
    {
        Task<List<TypeDto>> GetTypesAsync();
        Task<TypeDto> GetTypeByIdAsync(int id);
        Task<bool> CreateTypeAsync(TypeCreateDto model);
        Task<bool> UpdateTypeAsync(int id, TypeUpdateDto model);
        Task<bool> DeleteTypeAsync(int id);
    }
