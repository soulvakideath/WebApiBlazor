using WebApi.Client.Dto.TypeDtos;
using WebApi.Client.Models;
using WebApi.Client.Services.IServices;
using MudBlazor;

namespace WebApi.Client.Services;

    public class TypeService : ITypeService
    {
        private readonly HttpService _httpService;
        private readonly ISnackbar _snackbar;
        private readonly string _baseUrl = "api/type";
        private readonly string _token = "123";

        public TypeService(HttpService httpService, ISnackbar snackbar)
        {
            _httpService = httpService;
            _snackbar = snackbar;
        }

        public async Task<List<TypeDto>> GetTypesAsync()
        {
            try
            {
                var result = await _httpService.GetAsync<List<TypeDto>>(_baseUrl, _token);
                if (result.IsSuccess)
                {
                    return result.Value;
                }
                else
                {
                    _snackbar.Add($"Error loading types: {result.ErrorMessage}", Severity.Error);
                    return new List<TypeDto>();
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
                return new List<TypeDto>();
            }
        }

        public async Task<TypeDto> GetTypeByIdAsync(int id)
        {
            try
            {
                var result = await _httpService.GetAsync<TypeDto>($"{_baseUrl}/{id}", _token);
                if (result.IsSuccess)
                {
                    return result.Value;
                }
                else
                {
                    _snackbar.Add($"Error loading type: {result.ErrorMessage}", Severity.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
                return null;
            }
        }

        public async Task<bool> CreateTypeAsync(TypeCreateDto dto)
        {
            try
            {
                var result = await _httpService.PostAsync<TypeDto>(_baseUrl, dto, _token);
                if (result.IsSuccess)
                {
                    return result.Value.Id > 0;
                }
                else
                {
                    _snackbar.Add($"Error creating type: {result.ErrorMessage}", Severity.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
                return false;
            }
        }

        public async Task<bool> UpdateTypeAsync(int id, TypeUpdateDto dto)
        {
            try
            {
                var result = await _httpService.PutAsync<bool>($"{_baseUrl}/{id}", dto, _token);
                if (result.IsSuccess)
                {
                    return result.Value;
                }
                else
                {
                    _snackbar.Add($"Error updating type: {result.ErrorMessage}", Severity.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
                return false;
            }
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            try
            {
                var result = await _httpService.DeleteAsync<bool>($"{_baseUrl}/{id}", _token);
                if (result.IsSuccess)
                {
                    return result.Value;
                }
                else
                {
                    _snackbar.Add($"Error deleting type: {result.ErrorMessage}", Severity.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
                return false;
            }
        }
    }