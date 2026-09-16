using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Application.DTOs;

namespace SalesManagementSystem.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponse> RegisterAsync(RegisterDto request);
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
}
