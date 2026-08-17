using MediatR;
using Medicare.Application.Features.Commands.Associate;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class DeleteAssociateCommandHandler : IRequestHandler<DeleteAssociateCommand, ResponseModel>
    {
        private readonly IAssociateRepository _associateRepository;
        public DeleteAssociateCommandHandler(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<ResponseModel> Handle(DeleteAssociateCommand request, CancellationToken cancellationToken)
        {
            return await _associateRepository.DeleteAssociateAsync(request.model);
        }
    }
}
