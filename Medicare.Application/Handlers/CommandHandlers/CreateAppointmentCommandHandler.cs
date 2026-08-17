using MediatR;
using Medicare.Application.Features.Commands.Appointment;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.CommonModels.ResponseModel;
using System.Security.Cryptography;
using System.Text;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ResponseModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<ResponseModel> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken) 
        {
            if (request.model.PaymentData != null)
            {
                if (!string.IsNullOrWhiteSpace(request.model.PaymentData.CVV))
                {
                    CreateHash(request.model.PaymentData.CVV, out byte[] cvvHash, out byte[] cvvSalt);

                    request.model.PaymentData.CvvHash = cvvHash;
                    request.model.PaymentData.CvvSalt = cvvSalt;
                }
            }
            return await _appointmentRepository.CreateAppointmentAsync(request.model);
        } 
        private static void CreateHash(string value, out byte[] hash, out byte[] salt)
        {
                using var hmac = new HMACSHA512();
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}