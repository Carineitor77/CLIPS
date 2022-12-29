using Clips.Models;

namespace Clips.Bll.JWT
{
    public interface ITokenManager
    {
        string GenerateToken(User user);
    }
}
