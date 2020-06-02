using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ApiResponse<IEnumerable<WeaponDto>>> GetAll()
        {
            var data = await _weaponService.GetAllAsync();
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<WeaponDto>> GetById(Guid id)
        {
            var data = await _weaponService.GetByIdAsync(id);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<WeaponDto>> Create(CreateWeaponDto request) 
        {
            var data = await _weaponService.CreateAsync(request);
            var location = Url.Link(GetByIdRouteName, new { id = data.Id });
            return ApiResponse.Created(location, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<WeaponDto>> Update(Guid id, UpdateWeaponDto request)
        {
            var data = await _weaponService.UpdateAsync(id, request);
            return ApiResponse.Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id) 
        {
            await _weaponService.DeleteAsync(id);
            return ApiResponse.Ok();
        }
    }
}