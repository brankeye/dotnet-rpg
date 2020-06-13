using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Skill.Dtos;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Core.Skill.CreateSkillCommand;
using dotnet_rpg.Service.Core.Skill.DeleteSkillCommand;
using dotnet_rpg.Service.Core.Skill.GetAllSkillsQuery;
using dotnet_rpg.Service.Core.Skill.GetSkillQuery;
using dotnet_rpg.Service.Core.Skill.UpdateSkillCommand;
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
        private readonly IOperationMediator _operationMediator;

        public SkillsController(IOperationMediator operationMediator) 
        {
            _operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GetSkillQueryResult>>> GetAll()
        {
            var query = new GetAllSkillsQuery();
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<GetSkillQueryResult>> GetById(Guid id)
        {
            var query = new GetSkillQuery
            {
                Id = id
            };
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<object>> Create(CreateSkillDto request)
        {
            var command = new CreateSkillCommand
            {
                Name = request.Name,
                Damage = request.Damage
            };
            await _operationMediator.HandleAsync(command);
            var location = Url.Link(GetByIdRouteName, new { id = command.GeneratedId });
            return ApiResponse.Created(location, command.GeneratedId);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, UpdateSkillDto request)
        {
            var command = new UpdateSkillCommand
            {
                Id = id,
                Name = request.Name,
                Damage = request.Damage
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            var command = new DeleteSkillCommand
            {
                Id = id
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }
    }
}