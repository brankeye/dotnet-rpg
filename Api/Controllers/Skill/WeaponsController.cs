using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Skill;
using dotnet_rpg.Service.Core.Skill.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Skill
{
    [Authorize]
    [ApiController]
    [Route("skills")]
    public class SkillsController : ControllerBase
    {
        private const string GetByIdRouteName = "get_skill";
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService) 
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<SkillDto>>> GetAll()
        {
            var data = await _skillService.GetAllAsync();
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<SkillDto>> GetById(Guid id)
        {
            var data = await _skillService.GetByIdAsync(id);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<SkillDto>> Create(CreateSkillDto request) 
        {
            var data = await _skillService.CreateAsync(request);
            var location = Url.Link(GetByIdRouteName, new { id = data.Id });
            return ApiResponse.Created(location, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<SkillDto>> Update(Guid id, UpdateSkillDto request)
        {
            var data = await _skillService.UpdateAsync(id, request);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id) 
        {
            await _skillService.DeleteAsync(id);
            return ApiResponse.Ok();
        }
    }
}