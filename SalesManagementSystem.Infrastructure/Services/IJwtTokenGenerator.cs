using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Infrastructure.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
