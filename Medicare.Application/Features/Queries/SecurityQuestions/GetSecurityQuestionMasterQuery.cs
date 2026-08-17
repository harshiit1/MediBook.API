using MediatR;
using Medicare.Application.Models.User;

namespace Medicare.Application.Features.Queries.SecurityQuestions
{
    public record GetSecurityQuestionMasterQuery() : IRequest<List<SecurityQuestionDataModel>>;
}
