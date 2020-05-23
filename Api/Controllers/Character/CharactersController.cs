using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Controllers.Character.Mapper;
using dotnet_rpg.Api.Services.Character.Dtos;
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
        private readonly ICharacterMapper _characterMapper;

        public CharacterController(ICharacterService characterService) 
        {
            _characterService = characterService;
            _characterMapper = new CharacterMapper();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _characterService.GetAllAsync();

            var response = result.Select(_characterMapper.Map);
            
            return Ok(response);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _characterService.GetByIdAsync(id);
            var response = _characterMapper.Map(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterRequest request)
        {
            var dto = _characterMapper.Map(request);
            var result = await _characterService.CreateAsync(dto);
            var location = Url.Link(GetByIdRouteName, new { id = result.Id });
            var response = _characterMapper.Map(result);
            return Created(location, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCharacterRequest request)
        {
            var dto = _characterMapper.Map(request);
            var result = await _characterService.UpdateAsync(id, dto);
            var response = _characterMapper.Map(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var result = await _characterService.DeleteAsync(id);
            var response = _characterMapper.Map(result);
            return Ok(response);
        }

        [HttpPut("{id}/weapon/{weaponId}")]
        public async Task<IActionResult> EquipWeapon(Guid id, Guid weaponId)
        {
            var result = await _characterService.EquipWeaponAsync(id, weaponId);
            var response = _characterMapper.Map(result);
            return Ok(response);
        }

        [HttpDelete("{id}/weapon")]
        public async Task<IActionResult> UnequipWeapon(Guid id)
        {
            var result = await _characterService.UnequipWeaponAsync(id);
            var response = _characterMapper.Map(result);
            return Ok(response);
        }
    }
}