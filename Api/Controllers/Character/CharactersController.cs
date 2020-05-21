using System;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Character.Dtos;
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

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _characterService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterRequest request) 
        {
            var response = await _characterService.CreateAsync(new CreateCharacterDto
            {
                Name = request.Name,
                Class = request.Class
            });
            
            var location = Url.Link(GetByIdRouteName, new { id = response.Id });
            return Created(location, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCharacterRequest request) 
        {
            var response = await _characterService.UpdateAsync(id, new UpdateCharacterDto
            {
                Name = request.Name,
                Class = request.Class
            });
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