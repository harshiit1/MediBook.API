using Medicare.Application.Models.Associate;

namespace Medicare.Application.Interfaces.IToken
{
    public interface IJwtTokenInterface
    {
        string GenerateToken(AssociateDetailModel model);
    }
}
