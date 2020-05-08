using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;
using System;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("character")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService) {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _characterService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var serviceResponse = await _characterService.GetByIdAsync(id);
            
            if (!serviceResponse.Success) {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WriteCharacterDto character) {
            var serviceResponse = await _characterService.CreateAsync(character);

            if (!serviceResponse.Success) {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(Guid id, WriteCharacterDto character) {
            var serviceResponse = await _characterService.UpdateAsync(id, character);

            if (!serviceResponse.Success) {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var serviceResponse = await _characterService.DeleteAsync(id);

            if (!serviceResponse.Success) {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}