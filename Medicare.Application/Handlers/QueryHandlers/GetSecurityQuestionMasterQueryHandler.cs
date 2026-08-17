using MediatR;
using Medicare.Application.Features.Queries.SecurityQuestions;
using Medicare.Application.Interfaces.ISecurityQuestionsRepository;
using Medicare.Application.Models.User;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetSecurityQuestionMasterQueryHandler : IRequestHandler<GetSecurityQuestionMasterQuery, List<SecurityQuestionDataModel>>
    {
        private readonly ISecurityQuestionsRepository _securityRepository;

        public GetSecurityQuestionMasterQueryHandler(ISecurityQuestionsRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }
        public async Task<List<SecurityQuestionDataModel>> Handle(GetSecurityQuestionMasterQuery request, CancellationToken cancellationToken)
        {
            return await _securityRepository.GetSecurityQuestionMasterAsync();
        }
    }
}
