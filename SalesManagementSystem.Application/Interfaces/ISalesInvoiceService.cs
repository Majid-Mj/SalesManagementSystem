using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces;

public interface ISalesInvoiceService
{
    Task<ApiResponse<List<SalesInvoiceMaster>>> GetAllAsync();
    Task<ApiResponse<SalesInvoiceMaster>> GetByIdAsync(int id);
    Task<ApiResponse<SalesInvoiceMaster>> AddAsync(AddInvoiceDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
