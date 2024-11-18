using WebApi.Client.Dto.OperationDto;
using WebApi.Client.Models;
using WebApi.Client.Services.IServices;
using MudBlazor;

namespace WebApi.Client.Services;

public class OperationService : IOperationService
{
    private readonly HttpService _httpService;
    private readonly ISnackbar _snackbar;
    private readonly string _baseUrl = "api/operation";
    private readonly string _token = "123";

    public OperationService(HttpService httpService, ISnackbar snackbar)
    {
        _httpService = httpService;
        _snackbar = snackbar;
    }

    public async Task<List<OperationDto>> GetAllAsync()
    {
        try
        {
            var result = await _httpService.GetAsync<List<OperationDto>>(_baseUrl, _token);
            if (result.IsSuccess && result.Value != null)
            {
                return result.Value;
            }
            else
            {
                _snackbar.Add("Error loading operations", Severity.Error);
                return new List<OperationDto>();
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
            return new List<OperationDto>();
        }
    }

    public async Task<OperationDto> GetByIdAsync(int id)
    {
        try
        {
            var result = await _httpService.GetAsync<OperationDto>($"{_baseUrl}/{id}", _token);
            if (result.IsSuccess && result.Value != null)
            {
                return result.Value;
            }
            else
            {
                _snackbar.Add("Error loading operation details", Severity.Error);
                return null;
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
            return null;
        }
    }

    public async Task<bool> CreateAsync(OperationCreateDto dto)
    {
        try
        {
            var result = await _httpService.PostAsync<OperationDto>(_baseUrl, dto, _token);
            if (result.IsSuccess && result.Value != null)
            {
                return true;
            }
            else
            {
                _snackbar.Add("Error creating operation", Severity.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, OperationUpdateDto dto)
    {
        try
        {
            var result = await _httpService.PutAsync<bool>($"{_baseUrl}/{id}", dto, _token);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                _snackbar.Add("Error updating operation", Severity.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Unhandled exception: {ex.Message}", Severity.Error);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _httpService.DeleteAsync<bool>($"{_baseUrl}/{id}", _token);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                _snackbar.Add("Error deleting operation", Severity.Error);
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
