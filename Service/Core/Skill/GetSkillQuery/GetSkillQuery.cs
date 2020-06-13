using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Core.Skill.GetSkillQuery
{
    public class GetSkillQuery : IQuery<GetSkillQueryResult>, IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}