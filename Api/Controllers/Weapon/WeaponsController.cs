using System;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Weapon
{
    [Authorize]
    [ApiController]
    [Route("weapons")]
    public class WeaponsController : ControllerBase
    {
        private const string GetByIdRouteName = "get_weapon";
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

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _weaponService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWeaponRequest request) 
        {
            var response = await _weaponService.CreateAsync(new CreateWeaponDto
            {
                Name = request.Name,
                Damage = request.Damage
            });
            
            var location = Url.Link(GetByIdRouteName, new { id = response.Id });
            return Created(location, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWeaponRequest request) 
        {
            var response = await _weaponService.UpdateAsync(id, new UpdateWeaponDto
            {
                Name = request.Name,
                Damage = request.Damage
            });
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