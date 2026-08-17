using MediatR;
using Medicare.Application.Features.Commands.LabResult;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateLabResultCommandHandler : IRequestHandler<CreateLabResultCommand, ResponseModel>
    {
        private readonly ILabRepository _labRepository;
        public CreateLabResultCommandHandler(ILabRepository labRepository)
        {
            _labRepository = labRepository;
        }
        public async Task<ResponseModel> Handle(CreateLabResultCommand request, CancellationToken cancellationToken)
        {
            return await _labRepository.CreateLabResultAsync(request.model);
        }
    }
}
