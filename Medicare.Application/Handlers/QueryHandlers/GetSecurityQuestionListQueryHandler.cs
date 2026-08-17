using MediatR;
using Medicare.Application.Features.Queries.SecurityQuestions;
using Medicare.Application.Interfaces.ISecurityQuestionsRepository;
using Medicare.Application.Models.User;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetSecurityQuestionListQueryHandler : IRequestHandler<GetSecurityQuestionListQuery, List<SecurityQuestionDataModel>>
    {
        private readonly ISecurityQuestionRepository _securityRepository;

        public GetSecurityQuestionListQueryHandler(ISecurityQuestionRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }
        public async Task<List<SecurityQuestionDataModel>> Handle(GetSecurityQuestionListQuery request, CancellationToken cancellationToken)
        {
            return await _securityRepository.GetSecurityQuestionMasterAsync();
        }
    }
}
