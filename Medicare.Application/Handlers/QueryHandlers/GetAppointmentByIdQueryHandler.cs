using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.Doctor;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetAppointmentByIdQueryHandler: IRequestHandler<GetAppointmentByIdQuery, AppointmentDetailModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDetailModel> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _appointmentRepository.GetAppointmentById(request.appointmentId);
            return new AppointmentDetailModel
            {
                AppointmentId = result.AppointmentId,
                PatientId = result.PatientId,
                ProfileId = result.ProfileId,
                SlotId = result.SlotId,
                PatientName = result.PatientName,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender,
                SlotDate = result.SlotDate,
                SlotDay = result.SlotDay,
                SlotStartTime = result.SlotStartTime,
                SlotEndTime = result.SlotEndTime,
                AppointmentStatus = result.AppointmentStatus,
                VisitPurpose = result.VisitPurpose,
                VisitType = result.VisitType,
                OtpMethod = result.OtpMethod,
                CreatedDate = result.CreatedDate,
                Insurance = result.Insurance,
                DoctorProfile = new DoctorProfileModel
                {
                    AssociateId = result.AssociateId,
                    Name = result.Name,
                    Degree = result.Degree,
                    Image = result.Image,
                    Department = result.Department,
                    DesignationName = result.DesignationName,
                    FromTime = result.FromTime,
                    ToTime = result.ToTime
                },
                InsuranceData = new InsuranceData
                {
                    Provider = result.Provider,
                    Policy = result.Policy,
                    GroupId = result.GroupId,
                    HolderName = result.HolderName,
                    Address = result.Address
                },

                PaymentData = new PaymentData
                {
                    PaymentType = result.PaymentType,
                    CardHolder = result.CardHolder,
                    CardNumber = result.CardNumber,
                    Expiry = result.Expiry
                },
                IsSuccess = result.IsSuccess,
                Status = result.Status,
                ResponseMessage = result.ResponseMessage
            };
        }
    }
}
