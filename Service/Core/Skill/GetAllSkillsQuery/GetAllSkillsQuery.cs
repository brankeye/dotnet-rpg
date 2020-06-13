using System;
using System.Collections.Generic;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Core.Skill.GetSkillQuery;

namespace dotnet_rpg.Service.Core.Skill.GetAllSkillsQuery
{
    public class GetAllSkillsQuery : 
        IQuery<IEnumerable<GetSkillQueryResult>>, 
        IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
    }
}