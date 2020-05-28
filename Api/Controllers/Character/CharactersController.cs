using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Controllers.Character.Mapper;
using dotnet_rpg.Service.Core.Character;
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
        private readonly ICharacterMapper _characterMapper;

        public CharacterController(ICharacterService characterService) 
        {
            _characterService = characterService;
            _characterMapper = new CharacterMapper();
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CharacterResponse>>> GetAll()
        {
            var result = await _characterService.GetAllAsync();
            var data = result.Select(_characterMapper.Map);
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<CharacterResponse>> GetById(Guid id)
        {
            var result = await _characterService.GetByIdAsync(id);
            var data = _characterMapper.Map(result);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<CharacterResponse>> Create(CreateCharacterRequest request)
        {
            var dto = _characterMapper.Map(request);
            var result = await _characterService.CreateAsync(dto);
            var location = Url.Link(GetByIdRouteName, new { id = result.Id });
            var data = _characterMapper.Map(result);
            return ApiResponse.Created(location, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<CharacterResponse>> Update(Guid id, UpdateCharacterRequest request)
        {
            var dto = _characterMapper.Map(request);
            var result = await _characterService.UpdateAsync(id, dto);
            var data = _characterMapper.Map(result);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id) 
        {
            await _characterService.DeleteAsync(id);
            return ApiResponse.Ok();
        }

        [HttpPut("{id}/weapon/{weaponId}")]
        public async Task<ApiResponse<CharacterResponse>> EquipWeapon(Guid id, Guid weaponId)
        {
            var result = await _characterService.EquipWeaponAsync(id, weaponId);
            var data = _characterMapper.Map(result);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}/weapon")]
        public async Task<ApiResponse<CharacterResponse>> UnequipWeapon(Guid id)
        {
            var result = await _characterService.UnequipWeaponAsync(id);
            var data = _characterMapper.Map(result);
            return ApiResponse.Ok(data);
        }
    }
}