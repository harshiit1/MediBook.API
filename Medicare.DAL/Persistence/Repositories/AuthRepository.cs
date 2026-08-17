using Dapper;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.Email;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.User;
using Medicare.DAL.Persistence.Dapper;
using Microsoft.AspNetCore.Identity;

namespace Medicare.DAL.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public AuthRepository(IDapperContext context, IErrorLogRepository errorLog, IEmailService emailService, IJwtTokenRepository jwtTokenRepository)
        {
            _context = context;
            _errorLog = errorLog;
            _emailService = emailService;
            _jwtTokenRepository = jwtTokenRepository;
        }

        public async Task<ResponseModel> RegisterUserAsync(UserModel model)
        {
            string procName = "USP_UserRegister";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Username", model.Username);
                param.Add("PasswordHash", model.PasswordHash);
                param.Add("PasswordSalt", model.PasswordSalt);
                param.Add("FirstName", model.FirstName);
                param.Add("LastName", model.LastName);
                param.Add("PhoneNumber", model.PhoneNumber);
                param.Add("Email", model.Email);
                param.Add("RoleId", model.RoleId);
                param.Add("DepartmentId", model.DepartmentId);
                param.Add("CreatedBy", model.CreatedBy);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<AssociateResponseModel> RegisterAssociateAsync(CreateAssociateRequestModel model)
        {
            string procName = "USP_RegisterAssociate";
            AssociateResponseModel returnData = new AssociateResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("TenantId", model.TenantId);
                param.Add("FirstName", model.FirstName);
                param.Add("MiddleName", model.MiddleName);
                param.Add("LastName", model.LastName);
                param.Add("PasswordHash", model.Password);
                param.Add("DateOfBirth", model.DateOfBirth);
                param.Add("Gender", model.Gender);
                param.Add("IdentityDocument", model.IdentityDocument);
                param.Add("IdentityFile", model.IdentityFileBytes);
                param.Add("PhoneCountryCode", model.PhoneCountryCode);
                param.Add("PhoneNumber", model.PhoneNumber);
                param.Add("EmailId", model.EmailId);
                param.Add("ResidentialAddress", model.ResidentialAddress);
                param.Add("PermanentAddress", model.PermanentAddress);
                param.Add("CityId", model.CityId);
                param.Add("StateId", model.StateId);
                param.Add("CountryId", model.CountryId);
                param.Add("ZipCode", model.ZipCode);
                param.Add("LanguagesSpoken", model.LanguagesSpoken);
                param.Add("EmergencyName", model.EmergencyName);
                param.Add("EmergencyRelationship", model.EmergencyRelationship);
                param.Add("EmergencyPhone", model.EmergencyPhone);
                param.Add("EmergencyCode", model.EmergencyCode);
                param.Add("JoiningDate", model.JoiningDate);
                param.Add("EmployeeType", model.EmployeeType);
                param.Add("DepartmentId", model.DepartmentId);
                param.Add("RoleId", model.RoleId);
                param.Add("SpecialityId", model.SpecialityId);
                param.Add("DesignationId", model.DesignationId);

                param.Add("HighestDegree", model.AssociateQualification.HighestDegree);
                param.Add("Specialization", model.AssociateQualification.Specialization);
                param.Add("InstitutionName", model.AssociateQualification.InstitutionName);
                param.Add("YearOfPassing", model.AssociateQualification.YearOfPassing);
                param.Add("RegistrationNumber", model.AssociateQualification.RegistrationNumber);
                param.Add("LicenseExpiry", model.AssociateQualification.LicenseExpiry);
                param.Add("AdditionalCertifications", model.AssociateQualification.AdditionalCertifications);
                param.Add("QualificationDocuments", model.AssociateQualification.QualificationDocumentBytes);
                param.Add("ExperienceYears", model.AssociateExperience.ExperienceYears);
                param.Add("OrganizationName", model.AssociateExperience.OrganizationName);
                param.Add("DesignationRole", model.AssociateExperience.DesignationRole);
                param.Add("DepartmentWorked", model.AssociateExperience.DepartmentWorked);
                param.Add("KeySkills", model.AssociateExperience.KeySkills);

                param.Add("CreatedBy", model.CreatedBy);

                returnData = await _context.QuerySingleStoredProcAsync<AssociateResponseModel>(procName, param);

                if (returnData.IsSuccess == 1 && returnData.Status == 1)
                {
                    string token = _jwtTokenRepository.GeneratePasswordResetToken(
                         returnData.UserId.ToString(),
                         returnData.EmployeeId
                     );

                    var body = $"""
                        <div style='font-family:Arial,sans-serif;max-width:600px;margin:auto;border:1px solid #e0e0e0;border-radius:8px;overflow:hidden;'>

                             <div style='background:#0066cc;padding:24px;text-align:center;'>
                               <h1 style='color:white;margin:0;'>Welcome to MediBook</h1>
                               <p style='color:#cce5ff;margin:8px 0 0;'>
                                 Your Healthcare Management Platform
                               </p>
                             </div>

                             <div style='padding:24px;'>

                               <h2 style='color:#333;margin-top:0;'>
                                 Welcome, {model.FirstName} {model.LastName}!
                               </h2>

                               <p style='color:#444;line-height:1.6;'>
                                 Your MediBook account has been successfully created.
                                 You can now set your password and access your account.
                               </p>

                               <table style='width:100%;border-collapse:collapse;margin:20px 0;'>

                                 <tr>
                                   <td style='padding:12px;font-weight:bold;'>
                                     Username
                                   </td>
                                   <td style='padding:12px;'>
                                     {returnData.EmployeeId}
                                   </td>
                                 </tr>

                               </table>

                               <p style='color:#444;line-height:1.6;'>
                                 To secure your account, please set your password using the button below.
                                 This link will expire in <strong>30 minutes</strong>.
                               </p>

                               <div style='text-align:center;margin:30px 0;'>

                                 <a href='https://medibook.com/set-password?token={token}'
                                    style='display:inline-block;
                                           background:#0066cc;
                                           color:white;
                                           padding:13px 28px;
                                           text-decoration:none;
                                           border-radius:5px;
                                           font-weight:bold;
                                           font-size:15px;'>
                                   Set My Password
                                 </a>

                               </div>

                               <div style='background:#fff5f5;
                                           border-left:4px solid #dc3545;
                                           padding:14px 16px;
                                           margin:20px 0;'>

                                 <p style='margin:0;color:#721c24;font-size:13px;line-height:1.6;'>
                                   <strong>🔒 Security Notice</strong><br>
                                   This password setup link is unique to your account.
                                   Never share this link with anyone.
                                 </p>

                               </div>

                               <p style='color:#666;font-size:13px;line-height:1.6;'>
                                 If you did not expect this account creation email,
                                 please contact the MediBook support team immediately.
                               </p>

                             </div>

                             <div style='background:#f5f5f5;padding:16px;text-align:center;'>
                               <p style='color:#999;font-size:12px;margin:0;'>
                                 MediBook — Your Health, Our Priority
                               </p>
                             </div>

                           </div>
                        """;

                    var emailmodel = new EmailModel
                    {
                        ToEmail = model.EmailId,
                        ToName = $"{model.FirstName} {model.LastName}",
                        Subject = "Associate Login - Rest Password Link",
                        Body = body,
                        IsHtml = true
                    };
                    bool resetPasswrodMail = await _emailService.SendEmailAsync(emailmodel);

                    if (!resetPasswrodMail)
                    {
                        returnData = new AssociateResponseModel
                        {
                            EmployeeId = string.Empty,
                            UserId = Guid.Empty,
                            IsSuccess = 0,
                            ResponseMessage = "An Error Occured While Creating Assoicate",
                            ResponseId = 0,
                            Status = 0
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<AuthDetailModel> GetPasswordByUsernameAsync(string Username)
        {
            string procName = "USP_GetPasswordByUsername";
            AuthDetailModel returnData = new AuthDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Username", Username);
                returnData = await _context.QuerySingleStoredProcAsync<AuthDetailModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }
        public async Task<ResponseModel> IncrementOtpAttemptsAsync(Guid userId)
        {
            string procName = "USP_UpdatePatientOtpAttempts";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", userId);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }

        public async Task<ResponseModel> ClearOtpAsync(string email)
        {
            string procName = "USP_ClearPatientOtp";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Email", email);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }

        public async Task<ResponseModel> SaveOtpAsync(OtpDetailModel model)
        {
            string procName = "USP_SavePatientOtp";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", model.UserId);
                param.Add("UserType", model.UserType);
                param.Add("OtpHash", model.OtpHash);
                param.Add("OtpExpiry", model.OtpExpiry);
                param.Add("OtpAttempts", model.OtpAttempts);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }
        public async Task<OtpDetailModel> GetOtpDetailAsync(string email)
        {
            string procName = "USP_GetPatientOtpDetail";
            OtpDetailModel returnData = new OtpDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Email", email);
                returnData = await _context.QuerySingleStoredProcAsync<OtpDetailModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }

        public async Task<ResponseModel> SendOtpEmailAsync(string toEmail, string toName, string otpCode)
        {
            try
            {
                var email = new EmailModel
                {
                    ToEmail = toEmail,
                    ToName = toName,
                    Subject = "Your Medicare Login OTP",
                    Body = BuildOtpEmailBody(toName, otpCode),
                    IsHtml = true
                };

                await _emailService.SendEmailAsync(email);

                return new ResponseModel()
                {
                    IsSuccess = 1,
                    ResponseMessage = "OTP email sent successfully."
                };
            }
            catch (Exception ex) 
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = "SendOtpEmailAsync",
                    Error_Trace = ex.StackTrace
                });

                return new ResponseModel()
                {
                    IsSuccess = 0,
                    ResponseMessage = "Failed to send OTP email."
                };
            }
        }
        private static string BuildOtpEmailBody(string name, string otpCode)
        {
            return $@"
            <div style='font-family:Arial,sans-serif;max-width:600px;margin:auto;border:1px solid #e0e0e0;border-radius:8px;overflow:hidden;'>

                  <div style='background:#0066cc;padding:24px;text-align:center;'>
                    <h1 style='color:white;margin:0;'>MediBook</h1>
                    <p style='color:#cce5ff;margin:8px 0 0;'>Secure OTP Verification</p>
                  </div>

                  <div style='padding:24px;'>

                    <p>Hi <strong>{name}</strong>,</p>

                    <p>
                      We received a request to verify your identity for your
                      <strong>MediBook</strong> account. Please use the One-Time Password
                      (OTP) below to complete your verification.
                    </p>

                    <div style='text-align:center;margin:28px 0;'>

                      <p style='color:#555;font-size:14px;margin-bottom:10px;'>
                        Your Verification Code
                      </p>

                      <div style='display:inline-block;
                                  background:#f0f7ff;
                                  border:2px dashed #0066cc;
                                  border-radius:8px;
                                  padding:16px 30px;
                                  font-size:30px;
                                  font-weight:bold;
                                  letter-spacing:8px;
                                  color:#0066cc;'>
                        {otpCode}
                      </div>

                      <p style='color:#e65c00;font-weight:bold;margin-top:16px;'>
                        ⏱️ This OTP is valid for 5 minutes.
                      </p>

                    </div>

                    <div style='background:#f5f9ff;
                                border-left:4px solid #0066cc;
                                padding:14px 16px;
                                margin:20px 0;'>

                      <p style='margin:0;color:#444;font-size:13px;line-height:1.6;'>
                        <strong>🔒 Security Notice</strong><br>
                        Never share this OTP with anyone. MediBook support will never ask
                        you to provide your verification code.
                      </p>

                    </div>

                    <p style='color:#666;font-size:13px;'>
                      If you did not request this verification code, please ignore this email.
                      Your account will remain secure.
                    </p>

                  </div>

                  <div style='background:#f5f5f5;padding:16px;text-align:center;'>
                    <p style='color:#999;font-size:12px;margin:0;'>
                      MediBook — Your Health, Our Priority
                    </p>
                  </div>

                </div>";
        }

        public async Task<ResponseModel> ResetFailedAttemptsAsync(string email)
        {
            string procName = "USP_ResetFailedAttempts";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Email", email);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<ResponseModel> ResetPasswordAsync(Guid userId, string passwordHash)
        {
            string procName = "USP_ResetAssociatePassword";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", userId);
                param.Add("PasswordHash", passwordHash);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<ResponseModel> SavePasswordResetTokenAsync(Guid userId, Guid token)
        {
            string procName = "USP_SavePasswordResetToken";
            ResponseModel returnData = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", userId);
                param.Add("Token", token);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<ResponseModel> ResetForgotPasswordAsync(ResetForgotPasswordModel model)
        {
            string procName = "USP_ResetForgotPassword";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Token", model.Token);
                param.Add("PasswordHash", model.Password);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<OtpDetailModel> GetOtpDetailByUserIdAsync(Guid userId)
        {
            string procName = "USP_GetPatientOtpDetail";
            OtpDetailModel returnData = new OtpDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", userId);
                returnData = await _context.QuerySingleStoredProcAsync<OtpDetailModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<ResponseModel> ClearForgotPasswordOtpAsync(Guid userId)
        {
            string procName = "USP_ClearPatientOtp";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", userId);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }
    }
}
