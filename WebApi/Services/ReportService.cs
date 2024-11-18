using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Dto.OperationDto;
using WebApi.Services.IServices;

namespace WebApi.Services;

    public class ReportService : IReportService
    {
        private readonly FinanceContext _context;

        public ReportService(FinanceContext context)
        {
            _context = context;
        }

        public async Task<ReportDailyDto> GetDailyReportAsync(DateTime date)
        {
            var operations = await _context.Operations
                .Include(op => op.Type)
                .Where(op => op.Date.Date == date.Date)
                .ToListAsync();

            var totalIncome = operations
                .Where(op => op.Type?.IsIncome == true)
                .Sum(op => op.Amount);

            var totalExpenses = operations
                .Where(op => op.Type?.IsIncome == false)
                .Sum(op => op.Amount);

            var operationDtos = operations.Select(op => new OperationDto
            {
                Id = op.Id,
                Description = op.Description,
                Amount = op.Amount,
                Date = op.Date,
                TypeId = op.TypeId,
                TypeName = op.Type?.Name ?? "Unknown",
                IsIncome = op.Type?.IsIncome ?? false
            }).ToList();

            return new ReportDailyDto
            {
                Date = date,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Operations = operationDtos
            };
        }

        public async Task<ReportPeriodDto> GetPeriodReportAsync(DateTime start, DateTime end)
        {
            var operations = await _context.Operations
                .Include(op => op.Type)
                .Where(op => op.Date >= start && op.Date <= end)
                .ToListAsync();

            var totalIncome = operations
                .Where(op => op.Type?.IsIncome == true)
                .Sum(op => op.Amount);

            var totalExpenses = operations
                .Where(op => op.Type?.IsIncome == false)
                .Sum(op => op.Amount);

            var operationDtos = operations.Select(op => new OperationDto
            {
                Id = op.Id,
                Description = op.Description,
                Amount = op.Amount,
                Date = op.Date,
                TypeId = op.TypeId,
                TypeName = op.Type?.Name ?? "Unknown",
                IsIncome = op.Type?.IsIncome ?? false
            }).ToList();

            return new ReportPeriodDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Operations = operationDtos
            };
        }
    }
