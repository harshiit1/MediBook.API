using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Features.Queries.SecurityQuestions;
using Medicare.Application.Models.Master;
using Medicare.Application.Models.MasterModels;
using Medicare.Application.Models.Speciality;
using Medicare.Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterController : BaseApiController
    {
        private readonly IMediator _mediator;
        public MasterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetStatusKeyList")]
        public async Task<IActionResult> GetStatusKeyList()
        {
            List<StatusCategoryModel> response = new List<StatusCategoryModel>();
            response = await _mediator.Send(new GetStatusKeyListQuery());
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetRelationTypeList")]
        public async Task<IActionResult> GetRelationTypeList()
        {
            List<RelationTypeModel> response = new List<RelationTypeModel>();
            response = await _mediator.Send(new GetRealtionTypeListQuery());
            return HandleListResponse(response);
        }
        [HttpGet]
        [Route("GetAgeTypeList")]
        public async Task<IActionResult> GetAgeTypeList()
        {
            List<AgeTypeModel> response = new List<AgeTypeModel>();
            response = await _mediator.Send(new GetAgeTypeListQuery());
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetWeekDaysList")]
        public async Task<IActionResult> GetWeekDaysList()
        {
            List<WeekDaysModel> response = new List<WeekDaysModel>();
            response = await _mediator.Send(new GetWeekDaysListQuery());
            return HandleListResponse(response);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("GetRoleList")]
        public async Task<IActionResult> GetRoleList()
        {
            var role = User.FindFirst(ClaimTypes.Role)!.Value;
            List<RoleDataModel> response = new List<RoleDataModel>();
            response = await _mediator.Send(new GetRoleListQuery(role));
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetDepartmentByRoleId/{roleId}")]
        public async Task<IActionResult> GetDepartmentByRoleId(int roleId)
        {
            List<DepartmentDataModel> response = new List<DepartmentDataModel>();
            response = await _mediator.Send(new GetDepartmentByRoleIdQuery(roleId));
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetSpecialityByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetSpecialityByDepartmentId(int departmentId)
        {
            List<SpecialityTypeModel> response = new List<SpecialityTypeModel>();
            response = await _mediator.Send(new GetSpecialityByDepartmentIdQuery(departmentId));
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetDesignationByRoleId/{roleId}")]
        public async Task<IActionResult> GetDesignationByRoleId(int roleId)
        {
            List<DesignationDataModel> response = new List<DesignationDataModel>();
            response = await _mediator.Send(new GetDesignationByRoleIdQuery(roleId));
            return HandleListResponse(response);
        }

        [Authorize(Roles =("Admin"))]
        [HttpGet]
        [Route("GetRoleDepartmentSpecialityList")]
        public async Task<IActionResult> GetRoleDepartmentSpecialityList()
        {
            var role = User.FindFirst(ClaimTypes.Role)!.Value;
            List<RoleHierarchyModel> response = new List<RoleHierarchyModel>();
            response = await _mediator.Send(new GetRoleDepartmentSpecialityListQuery(role));
            return HandleListResponse(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetSecurityQuestionList")]
        public async Task<IActionResult> GetSecurityQuestionList()
        {
            List<SecurityQuestionDataModel> response = new List<SecurityQuestionDataModel>();
            response = await _mediator.Send(new GetSecurityQuestionListQuery());
            return HandleListResponse(response);
        }
    }
}
