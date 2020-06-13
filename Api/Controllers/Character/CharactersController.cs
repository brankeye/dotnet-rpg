using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Core.Character.CreateCharacterCommand;
using dotnet_rpg.Service.Core.Character.DeleteCharacterCommand;
using dotnet_rpg.Service.Core.Character.EquipWeaponCommand;
using dotnet_rpg.Service.Core.Character.GetAllCharactersQuery;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;
using dotnet_rpg.Service.Core.Character.LearnSkillCommand;
using dotnet_rpg.Service.Core.Character.UnequipSkillCommand;
using dotnet_rpg.Service.Core.Character.UnequipWeaponCommand;
using dotnet_rpg.Service.Core.Character.UpdateCharacterCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Character
{
    [Authorize]
    [ApiController]
    [Route("characters")]
    public class CharacterController : ControllerBase
    {
        private const string GetByIdRouteName = "get_character";
        private readonly IOperationMediator _operationMediator;

        public CharacterController(IOperationMediator operationMediator) 
        {
            _operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GetCharacterQueryResult>>> GetAll()
        {
            var query = new GetAllCharactersQuery();
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<GetCharacterQueryResult>> GetById(Guid id)
        {
            var query = new GetCharacterQuery
            {
                Id = id
            };
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<object>> Create(CreateCharacterDto request)
        {
            var command = new CreateCharacterCommand
            {
                Name = request.Name,
                Class = request.Class
            };
            await _operationMediator.HandleAsync(command);
            var location = Url.Link(GetByIdRouteName, new { id = command.GeneratedId });
            return ApiResponse.Created(location, command.GeneratedId);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, UpdateCharacterDto request)
        {
            var command = new UpdateCharacterCommand
            {
                Id = id,
                Name = request.Name,
                Class = request.Class
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id) 
        {
            var command = new DeleteCharacterCommand
            {
                Id = id
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }

        [HttpPost("{id}/weapon/{weaponId}")]
        public async Task<ApiResponse> EquipWeapon(Guid id, Guid weaponId)
        {
            var command = new EquipWeaponCommand
            {
                CharacterId = id,
                WeaponId = weaponId
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }

        [HttpDelete("{id}/weapon")]
        public async Task<ApiResponse> UnequipWeapon(Guid id)
        {
            var command = new UnequipWeaponCommand
            {
                CharacterId = id
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }
        
        [HttpPost("{id}/skill/{skillId}")]
        public async Task<ApiResponse> LearnSkill(Guid id, Guid skillId)
        {
            var command = new LearnSkillCommand
            {
                CharacterId = id,
                SkillId = skillId
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }
        
        [HttpDelete("{id}/skill/{skillId}")]
        public async Task<ApiResponse> UnlearnSkill(Guid id, Guid skillId)
        {
            var command = new UnlearnSkillCommand
            {
                CharacterId = id,
                SkillId = skillId
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }
    }
}