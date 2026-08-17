using MediatR;
using Medicare.Application.Features.Commands.Associate;
using Medicare.Application.Helper.DocumentHelper;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateAssociateCommandHandler : IRequestHandler<CreateAssociateCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly PasswordHelper _passwordHelper;
        public CreateAssociateCommandHandler(IAuthRepository authRepository, PasswordHelper passwordHelper) 
        {
            _authRepository = authRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task<ResponseModel> Handle(CreateAssociateCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;

            if(model.IdentityFile != null)
            {
                var (identityDocBytes, identityErr) = DocumentHelper.ProcessDocument(model.IdentityFile);

                if (identityErr != null)
                    return new ResponseModel() 
                    { 
                        Status = 0, 
                        ResponseMessage = $"Identity Document: {identityErr}",
                        IsSuccess = 0,
                        ResponseId = 0
                    };

                model.IdentityFileBytes = identityDocBytes;
            }

            if (model.AssociateQualification.QualificationDocuments != null)
            {
                var (qualificationDocBytes, qualErr) = DocumentHelper.ProcessDocument(model.AssociateQualification.QualificationDocuments);

                if (qualErr != null)
                    return new ResponseModel() 
                    { 
                        Status = 0, 
                        ResponseMessage = $"Qualification Document: {qualErr}",
                        IsSuccess = 0,
                        ResponseId = 0
                    };

                model.AssociateQualification.QualificationDocumentBytes = qualificationDocBytes;
            }
            string tempPassword = _passwordHelper.GenerateTempPassword();
            
            model.Password = _passwordHelper.HashPassword(tempPassword);
           
            var result = await _authRepository.RegisterAssociateAsync(request.model);

            if (result == null)
                return new ResponseModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseId = 0,
                    ResponseMessage = "Registration Failed — Please Try Again"
                };

            if (result.IsSuccess != 1 || result.Status != 1)
                return new ResponseModel
                {
                    Status = result.Status,
                    IsSuccess = result.IsSuccess,
                    ResponseId = result.ResponseId,
                    ResponseMessage = result.ResponseMessage
                };

            return new ResponseModel
            {
                IsSuccess = result.IsSuccess,
                ResponseId = result.ResponseId,
                Status = result.Status,
                ResponseMessage = result.ResponseMessage
            };
        }
    }
}
