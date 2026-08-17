using MediatR;
using Medicare.Application.Features.Queries.Associate;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Models.Associate;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetAssociateListQueryHandler : IRequestHandler<GetAssociateListQuery, List<AssociateListModel>>
    {
        private readonly IAssociateRepository _associateRepository;
        public GetAssociateListQueryHandler(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<List<AssociateListModel>> Handle(GetAssociateListQuery request, CancellationToken cancellationToken)
        {
            return await _associateRepository.GetAssociateListAsync();
        }
    }
}
