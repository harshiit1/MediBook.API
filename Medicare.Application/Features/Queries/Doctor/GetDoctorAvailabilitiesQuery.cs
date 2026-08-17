using MediatR;
using Medicare.Application.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Features.Queries.Doctor
{
    public record GetDoctorAvailabilitiesQuery(int DoctorId)
       : IRequest<List<DoctorAvailabilityModel>>;
}
