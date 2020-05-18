using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Api.Services.CharacterService;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using dotnet_rpg.Api.Dtos.Character;

namespace dotnet_rpg.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("characters")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService) 
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _characterService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _characterService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterDto dto) 
        {
            var response = await _characterService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCharacterDto dto) 
        {
            var response = await _characterService.UpdateAsync(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var response = await _characterService.DeleteAsync(id);
            return Ok(response);
        }

        [HttpPost("{id}/weapon")]
        public async Task<IActionResult> EquipWeapon(Guid id, Guid weaponId)
        {
            var response = await _characterService.EquipWeaponAsync(id, weaponId);
            return Ok(response);
        }

        [HttpDelete("{id}/weapon")]
        public async Task<IActionResult> UnequipWeapon(Guid id)
        {
            var response = await _characterService.UnequipWeaponAsync(id);
            return Ok(response);
        }
    }
}