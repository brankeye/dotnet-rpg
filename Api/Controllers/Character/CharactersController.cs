using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Character;
using dotnet_rpg.Service.Core.Character.Dtos;
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
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService) 
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CharacterDto>>> GetAll()
        {
            var data = await _characterService.GetAllAsync();
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<CharacterDto>> GetById(Guid id)
        {
            var data = await _characterService.GetByIdAsync(id);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<CharacterDto>> Create(CreateCharacterDto request)
        {
            var data = await _characterService.CreateAsync(request);
            var location = Url.Link(GetByIdRouteName, new { id = data.Id });
            return ApiResponse.Created(location, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<CharacterDto>> Update(Guid id, UpdateCharacterDto request)
        {
            var data = await _characterService.UpdateAsync(id, request);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id) 
        {
            await _characterService.DeleteAsync(id);
            return ApiResponse.Ok();
        }

        [HttpPost("{id}/weapon/{weaponId}")]
        public async Task<ApiResponse<CharacterDto>> EquipWeapon(Guid id, Guid weaponId)
        {
            var data = await _characterService.EquipWeaponAsync(id, weaponId);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}/weapon")]
        public async Task<ApiResponse<CharacterDto>> UnequipWeapon(Guid id)
        {
            var data = await _characterService.UnequipWeaponAsync(id);
            return ApiResponse.Ok(data);
        }
        
        [HttpPost("{id}/skill/{skillId}")]
        public async Task<ApiResponse<CharacterDto>> LearnSkill(Guid id, Guid skillId)
        {
            var data = await _characterService.LearnSkillAsync(id, skillId);
            return ApiResponse.Ok(data);
        }
        
        [HttpDelete("{id}/skill/{skillId}")]
        public async Task<ApiResponse<CharacterDto>> UnlearnSkill(Guid id, Guid skillId)
        {
            var data = await _characterService.UnlearnSkillAsync(id, skillId);
            return ApiResponse.Ok(data);
        }
    }
}