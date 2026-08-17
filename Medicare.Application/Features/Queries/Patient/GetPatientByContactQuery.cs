using MediatR;
using Medicare.Application.Models.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Features.Queries.Patient
{
    public record GetPatientByContactQuery(string contactNo) : IRequest<PatientMasterModel>;
}
