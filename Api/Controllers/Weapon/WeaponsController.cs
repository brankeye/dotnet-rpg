using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Api.Controllers.Weapon.Mapper;
using dotnet_rpg.Service.Core.Weapon;
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
        public async Task<ApiResponse<IEnumerable<WeaponResponse>>> GetAll()
        {
            var result = await _weaponService.GetAllAsync();
            var data = result.Select(_weaponMapper.Map);
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<WeaponResponse>> GetById(Guid id)
        {
            var result = await _weaponService.GetByIdAsync(id);
            var data = _weaponMapper.Map(result);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<WeaponResponse>> Create(CreateWeaponRequest request) 
        {
            var dto = _weaponMapper.Map(request);
            var result = await _weaponService.CreateAsync(dto);
            var location = Url.Link(GetByIdRouteName, new { id = result.Id });
            var data = _weaponMapper.Map(result);
            return ApiResponse.Created(location, data);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<WeaponResponse>> Update(Guid id, UpdateWeaponRequest request)
        {
            var dto = _weaponMapper.Map(request);
            var result = await _weaponService.UpdateAsync(id, dto);
            var data = _weaponMapper.Map(result);
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