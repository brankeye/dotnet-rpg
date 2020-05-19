using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using dotnet_rpg.Api.Services.Weapon;
using dotnet_rpg.Api.Services.Weapon.Dtos;

namespace dotnet_rpg.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("weapons")]
    public class WeaponsController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponsController(IWeaponService weaponService) 
        {
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _weaponService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _weaponService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWeaponDto dto) 
        {
            var response = await _weaponService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWeaponDto dto) 
        {
            var response = await _weaponService.UpdateAsync(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var response = await _weaponService.DeleteAsync(id);
            return Ok(response);
        }
    }
}