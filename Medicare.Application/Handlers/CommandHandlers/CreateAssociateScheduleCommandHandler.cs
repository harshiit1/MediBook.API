using MediatR;
using Medicare.Application.Features.Commands.Associate;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateAssociateScheduleCommandHandler : IRequestHandler<CreateAssociateScheduleCommand, ResponseModel>
    {
        private readonly IAssociateRepository _associateRepository;
        public CreateAssociateScheduleCommandHandler(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<ResponseModel> Handle(CreateAssociateScheduleCommand request, CancellationToken cancellationToken)
        {
            return await _associateRepository.CreateAssociateScheduleAsync(request.model);
        }
    }
}
