using MediatR;
using Medicare.Application.Features.Queries.Doctor;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Models.Speciality;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetDoctorSpecialityListQueryHandler : IRequestHandler<GetDoctorSpecialityListQuery, List<DoctorSpecialityDataModel>>
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetDoctorSpecialityListQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorSpecialityDataModel>> Handle(GetDoctorSpecialityListQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorSpecialityListAsync(request.DoctorName, request.DepartmentName);
        }
    }
}
