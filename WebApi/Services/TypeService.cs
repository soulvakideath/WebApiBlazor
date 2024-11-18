using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Dto.TypeDtos;
using WebApi.Models;
using WebApi.Services.IServices;

namespace WebApi.Services
{
    public class TypeService : ITypeService
    {
        private readonly FinanceContext _context;

        public TypeService(FinanceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            return await _context.TransactionTypes
                .Select(type => new TypeDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description,
                    IsIncome = type.IsIncome
                }).ToListAsync();
        }

        public async Task<TypeDto> GetTypeByIdAsync(int id)
        {
            var type = await _context.TransactionTypes.FindAsync(id);
            if (type == null) return null;

            return new TypeDto
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description,
                IsIncome = type.IsIncome
            };
        }

        public async Task<TypeDto> CreateTypeAsync(TypeCreateDto dto)
        {
            if (await IsDuplicateOnCreateAsync(dto))
            {
                throw new InvalidOperationException("A type with the same name already exists.");
            }

            var type = new TransactionType
            {
                Name = dto.Name,
                Description = dto.Description,
                IsIncome = dto.IsIncome
            };

            _context.TransactionTypes.Add(type);
            await _context.SaveChangesAsync();

            return new TypeDto
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description,
                IsIncome = type.IsIncome
            };
        }

            public async Task<bool> UpdateTypeAsync(TypeUpdateDto dto)
            {
                var existingType = await _context.TransactionTypes.FindAsync(dto.Id);
                if (existingType == null) return false;

                existingType.Name = dto.Name;
                existingType.Description = dto.Description;
                existingType.IsIncome = dto.IsIncome;

                _context.TransactionTypes.Update(existingType);
                await _context.SaveChangesAsync();

                return true;
            }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            var type = await _context.TransactionTypes.FindAsync(id);
            if (type == null) return false;

            _context.TransactionTypes.Remove(type);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> IsDuplicateOnCreateAsync(TypeCreateDto dto, int? existingTypeId = null)
        {
            return await _context.TransactionTypes.AnyAsync(t =>
                t.Name == dto.Name &&
                (existingTypeId == null || t.Id != existingTypeId.Value));
        }

        public async Task<bool> IsDuplicateOnUpdateAsync(TypeUpdateDto dto, int typeId)
        {
            return await _context.TransactionTypes.AnyAsync(t =>
                t.Name == dto.Name &&
                t.Id != typeId);
        }
    }
}
