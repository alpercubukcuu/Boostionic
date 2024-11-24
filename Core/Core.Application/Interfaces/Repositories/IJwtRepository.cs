

using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IJwtRepository
    {
        string GenerateJwtToken(User user);
    }
}
