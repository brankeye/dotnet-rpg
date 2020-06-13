using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Core.Weapon.CreateWeaponCommand;
using dotnet_rpg.Service.Core.Weapon.DeleteWeaponCommand;
using dotnet_rpg.Service.Core.Weapon.GetAllWeaponsQuery;
using dotnet_rpg.Service.Core.Weapon.GetWeaponQuery;
using dotnet_rpg.Service.Core.Weapon.UpdateWeaponCommand;
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
        private readonly IOperationMediator _operationMediator;

        public WeaponsController(IOperationMediator operationMediator) 
        {
            _operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GetWeaponQueryResult>>> GetAll()
        {
            var query = new GetAllWeaponsQuery();
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpGet("{id}", Name = GetByIdRouteName)]
        public async Task<ApiResponse<GetWeaponQueryResult>> GetById(Guid id)
        {
            var query = new GetWeaponQuery
            {
                Id = id
            };
            var data = await _operationMediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpPost]
        public async Task<ApiResponse<object>> Create(CreateWeaponDto request)
        {
            var command = new CreateWeaponCommand
            {
                Name = request.Name,
                Damage = request.Damage
            };
            await _operationMediator.HandleAsync(command);
            var location = Url.Link(GetByIdRouteName, new { id = command.GeneratedId });
            return ApiResponse.Created(location, command.GeneratedId);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, UpdateWeaponDto request)
        {
            var command = new UpdateWeaponCommand
            {
                Id = id,
                Name = request.Name,
                Damage = request.Damage
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            var command = new DeleteWeaponCommand
            {
                Id = id
            };
            await _operationMediator.HandleAsync(command);
            return ApiResponse.Ok();
        }
    }
}