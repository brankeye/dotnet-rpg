using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Api.Controllers.Weapon.Mapper;
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
        private readonly IWeaponMapper _weaponMapper;

        public WeaponsController(IWeaponService weaponService) 
        {
            _weaponService = weaponService;
            _weaponMapper = new WeaponMapper();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _weaponService.GetAllAsync();
            var response = result.Select(_weaponMapper.Map);
            return Ok(response);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _weaponService.GetByIdAsync(id);
            var response = _weaponMapper.Map(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWeaponRequest request) 
        {
            var dto = _weaponMapper.Map(request);
            var result = await _weaponService.CreateAsync(dto);
            var location = Url.Link(GetByIdRouteName, new { id = result.Id });
            var response = _weaponMapper.Map(result);
            return Created(location, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWeaponRequest request)
        {
            var dto = _weaponMapper.Map(request);
            var result = await _weaponService.UpdateAsync(id, dto);
            var response = _weaponMapper.Map(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var result = await _weaponService.DeleteAsync(id);
            var response = _weaponMapper.Map(result);
            return Ok(response);
        }
    }
}