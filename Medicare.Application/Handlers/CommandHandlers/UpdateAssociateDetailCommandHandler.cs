using MediatR;
using Medicare.Application.Features.Commands.Associate;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdateAssociateDetailCommandHandler : IRequestHandler<UpdateAssociateDetailCommand, ResponseModel>
    {
        private readonly IAssociateRepository _associateRepository;
        public UpdateAssociateDetailCommandHandler(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<ResponseModel> Handle(UpdateAssociateDetailCommand request, CancellationToken cancellationToken)
        {
            return await _associateRepository.UpdateAssociateDetailAsync(request.model);
        }
    }
}
