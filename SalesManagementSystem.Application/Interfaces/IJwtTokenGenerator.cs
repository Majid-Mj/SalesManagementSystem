using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
