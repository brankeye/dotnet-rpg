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
            var serviceResponse = await _characterService.GetAllAsync();
            return Ok(serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var serviceResponse = await _characterService.GetByIdAsync(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WriteCharacterDto character) {
            var serviceResponse = await _characterService.CreateAsync(character);
            return Ok(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(Guid id, WriteCharacterDto character) {
            var serviceResponse = await _characterService.UpdateAsync(id, character);
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var serviceResponse = await _characterService.DeleteAsync(id);
            return Ok(serviceResponse);
        }
    }
}