using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Speciality;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetSpecialityByDepartmentIdQueryHandler : IRequestHandler<GetSpecialityByDepartmentIdQuery, List<SpecialityTypeModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetSpecialityByDepartmentIdQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<SpecialityTypeModel>> Handle(GetSpecialityByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetSpecialityByDepartmentIdAsync(request.departmentId);
        }
    }
}
