using MediatR;
using Medicare.Application.Features.Commands.Associate;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Features.Commands.Patient;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.JwtTokens;
using Medicare.Application.Models.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IMediator mediator, IJwtTokenRepository jwtTokenRepository, IRefreshTokenRepository refreshTokenRepository, IAuthRepository authRepository, IConfiguration config)
        {
            _mediator = mediator;
            _jwtTokenRepository = jwtTokenRepository;
            _authRepository = authRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("doLogin")]
        public async Task<IActionResult> doLogin([FromBody] AuthModel model)
        {
            AuthResultModel response = new AuthResultModel();
            response = await _mediator.Send(new AuthCommand(model));
            if (response.IsSuccess != 1) return HandleResponse(response);
            var token = _jwtTokenRepository.GenerateToken(new JwtTokenClaimModel
            {
                UserId = response.UserId,
                RefId = response.RefId,
                UserType = response.UserType,
                Email = response.Email,
                Username = response.Username,
                FullName = response.FullName,
                RoleName = response.RoleName,
                TenantId = response.TenantId,
            });

            string refreshToken = _jwtTokenRepository.GenerateRefreshToken();
            DateTime expiryDate = DateTime.UtcNow.AddDays(int.Parse(_config["JwtSettings:RefreshTokenExpDays"]));

            var refreshTokenData = new JwtRefreshTokenModel
            {
                UserId = response.UserId,
                UserType = response.UserType,
                RefreshToken = refreshToken,
                ExpiryDate = expiryDate
            };

            await _refreshTokenRepository.SaveRefreshTokenAsync(refreshTokenData);

            return HandleLoginResponse(response, token, refreshToken);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreatePatientAccount")]
        public async Task<IActionResult> CreatePatientAccount([FromBody] CreatePatientRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new CreatePatientCommand(model));
            return HandleResponse(response);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("RegisterAssociate")]
        public async Task<IActionResult> RegisterAssociate(CreateAssociateRequestModel model)
        {
            var tenantId = Guid.Parse(User.FindFirst("TenantId")!.Value);
            model.TenantId = tenantId;
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new CreateAssociateCommand(model));
            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetAssociatePassword")]
        public async Task<IActionResult> ResetAssociatePassword([FromBody] ResetAssociatePasswordModel model)
        {
            ResponseModel response = new ResponseModel();
            if (string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new ApiResponse<object>
                {
                    Data = null,
                    StatusMessage = "Token and new password are required.",
                    StatusCode = HttpStatusCode.BadRequest,
                    Result = 0
                });
            response = await _mediator.Send(new ResetPasswordCommand(model));
            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
        {
            if (string.IsNullOrEmpty(model.AccessToken) || string.IsNullOrEmpty(model.RefreshToken))
             return BadRequest(
                 new ApiResponse<ResponseModel> { 
                     Data = null,    
                     StatusMessage = "Access token and refresh token are required.", 
                     StatusCode = HttpStatusCode.BadRequest, 
                     Result = 0 
                 });

            RefreshTokenResponseModel response = new RefreshTokenResponseModel();
            response = await _mediator.Send(new RefreshTokenCommand(model));
            return HandleTokenResponse(response);
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            ResponseModel response = new ResponseModel();
            response = await _refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken);
            return HandleLoggedOutResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RequestOtp")]
        public async Task<IActionResult> RequestOtp([FromBody] RequestOtpModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new RequestOtpCommand(model));
            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new VerifyOtpCommand(model));
            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] RequestOtpModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new ForgotPasswordCommand(model));
            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("VerifyForgotPasswordOtp")]
        public async Task<IActionResult> VerifyForgotPasswordOtp([FromBody] VerifyForgotPasswordModel model)
        {
            VerifyForgotPasswordResponseModel response = new VerifyForgotPasswordResponseModel();
            response = await _mediator.Send(new VerifyForgotPasswordCommand(model));
            return HandleForgotPasswordResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetForgotPassword")]
        public async Task<IActionResult> ResetForgotPassword([FromBody] ResetForgotPasswordModel model)
        {
            if (model.Token == Guid.Empty || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new ResponseModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseId = 0,
                    ResponseMessage = "Token and Password are required.",
                });

            var response = await _mediator.Send(new ResetForgotPasswordCommand(model));
            return HandleResponse(response);
        }
    }
}
