using MediatR;
using Medicare.Application.Features.Queries.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetPatientByContactQueryHandler : IRequestHandler<GetPatientByContactQuery, PatientMasterModel>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByContactQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<PatientMasterModel> Handle(GetPatientByContactQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientByContact(request.contactNo);
        }
    }
}
