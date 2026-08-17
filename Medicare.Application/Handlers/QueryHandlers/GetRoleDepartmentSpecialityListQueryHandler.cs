using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.MasterModels;
using System.Data;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetRoleDepartmentSpecialityListQueryHandler : IRequestHandler<GetRoleDepartmentSpecialityListQuery, List<RoleHierarchyModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetRoleDepartmentSpecialityListQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<RoleHierarchyModel>> Handle(GetRoleDepartmentSpecialityListQuery request, CancellationToken cancellationToken)
        {
            var excludedRole = request.role switch
            {
                "Admin" => new[] { 1, 2 },
                _ => new[] { 1, 2 }
            };

            var result = await _masterRepository.GetRoleDepartmentSpecialityHierarchyAsync();

            var returnData = result
                .GroupBy(x => new { x.RoleId, x.RoleName })
                .Select(r => new RoleHierarchyModel
                {
                    RoleId = r.Key.RoleId,
                    RoleName = r.Key.RoleName,

                    Designations = r
                        .Where(d => d.DesignationId.HasValue)
                        .GroupBy(d => new {d.DesignationId, d.DesignationName})
                        .Select(d => new DesignationModel
                        {
                            DesignationId = d.Key.DesignationId!.Value,
                            DesignationName = d.Key.DesignationName
                        })
                        .DistinctBy(d => d.DesignationId)
                        .ToList(),

                    Departments = r
                        .Where(d => d.DepartmentId.HasValue)
                        .GroupBy(d => new { d.DepartmentId, d.DepartmentName })
                        .Select(d => new DepartmentHierarchyModel
                        {
                            DepartmentId = d.Key.DepartmentId!.Value,
                            DepartmentName = d.Key.DepartmentName,
                            Specialities = d
                                .Where(s => s.SpecialityId.HasValue)
                                .GroupBy(s => new { s.SpecialityId, s.SpecialityName })
                                .Select(s => new SpecialityHierarchyModel
                                {
                                    SpecialityId = s.Key.SpecialityId!.Value,
                                    SpecialityName = s.Key.SpecialityName
                                })
                                .DistinctBy(s => s.SpecialityId)
                                .ToList()
                        })
                        .DistinctBy(d => d.DepartmentId)
                        .ToList()
                })
                .ToList();

            return returnData.Where(r => !excludedRole.Contains(r.RoleId)).ToList();
        }
    }
}
