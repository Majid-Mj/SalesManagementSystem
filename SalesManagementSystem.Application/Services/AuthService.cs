using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAppDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IAppDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ApiResponse> RegisterAsync(RegisterDto request)
    {
        bool exists = await _context.Users.AnyAsync(u => u.Username == request.Username);
        if (exists)
            return ApiResponse.Conflict("Username already exists");

        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return ApiResponse.Created("User registered successfully");
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return ApiResponse<LoginResponseDto>.Unauthorized("Invalid username or password");

        var token = _jwtTokenGenerator.GenerateToken(user);
        var loginResult = new LoginResponseDto
        {
            Token = token,
            Username = user.Username
        };

        return ApiResponse<LoginResponseDto>.SuccessResponse(loginResult, "Login successful");
    }
}
