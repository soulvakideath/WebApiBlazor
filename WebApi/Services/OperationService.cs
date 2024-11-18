using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Dto.OperationDto;
using WebApi.Models;
using WebApi.Services.IServices;

namespace WebApi.Services
{
    public class OperationService : IOperationService
    {
        private readonly FinanceContext _context;

        public OperationService(FinanceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OperationDto>> GetAllAsync()
        {
            var operations = await _context.Operations
                .Include(op => op.Type)
                .ToListAsync();

            return operations.Select(operation => new OperationDto
            {
                Id = operation.Id,
                Description = operation.Description,
                Amount = operation.Amount,
                Date = operation.Date,
                TypeId = operation.TypeId,
                TypeName = operation.Type?.Name ?? "Unknown",
                IsIncome = operation.Type?.IsIncome ?? false
            });
        }

        public async Task<OperationDto?> GetByIdAsync(int id)
        {
            var operation = await _context.Operations
                .Include(op => op.Type)
                .FirstOrDefaultAsync(op => op.Id == id);

            if (operation is null) return null;

            return new OperationDto
            {
                Id = operation.Id,
                Description = operation.Description,
                Amount = operation.Amount,
                Date = operation.Date,
                TypeId = operation.TypeId,
                TypeName = operation.Type?.Name ?? "Unknown",
                IsIncome = operation.Type?.IsIncome ?? false
            };
        }

        public async Task<OperationDto> CreateAsync(OperationCreateDto dto)
        {
            var type = await _context.TransactionTypes.FindAsync(dto.TypeId);
            if (type == null)
            {
                throw new InvalidOperationException("Transaction type not found.");
            }

            var operation = new Operation
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Date = dto.Date,
                TypeId = dto.TypeId
            };

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return new OperationDto
            {
                Id = operation.Id,
                Description = operation.Description,
                Amount = operation.Amount,
                Date = operation.Date,
                TypeId = operation.TypeId,
                TypeName = type.Name,
                IsIncome = type.IsIncome
            };
        }


        public async Task<bool> UpdateAsync(OperationUpdateDto dto)
        {
            var existingOperation = await _context.Operations.FindAsync(dto.Id);
            if (existingOperation == null) return false;

            existingOperation.Description = dto.Description;
            existingOperation.Amount = dto.Amount;
            existingOperation.Date = dto.Date;
            existingOperation.TypeId = dto.TypeId;

            _context.Operations.Update(existingOperation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var operation = await _context.Operations.FindAsync(id);
            if (operation is null) return false;

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsDuplicateOnCreateAsync(OperationCreateDto dto, int? existingOperationId = null)
        {
            return await _context.Operations.AnyAsync(op =>
                op.Description == dto.Description &&
                op.Date.Date == dto.Date.Date &&
                (existingOperationId == null || op.Id != existingOperationId.Value));
        }

        public async Task<bool> IsDuplicateOnUpdateAsync(OperationUpdateDto dto, int operationId)
        {
            return await _context.Operations.AnyAsync(op =>
                op.Description == dto.Description &&
                op.Date.Date == dto.Date.Date &&
                op.Id != operationId);
        }
    }
}
