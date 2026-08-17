using MediatR;
using Medicare.Application.Models.User;

namespace Medicare.Application.Features.Queries.User
{
    public record GetUserByIdQuery(int Id) : IRequest<UserInfoDataModel>;
}
