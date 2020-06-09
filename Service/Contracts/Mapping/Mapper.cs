using System;

namespace dotnet_rpg.Service.Contracts.Mapping
{
    public abstract class Mapper<TInput, TOutput> : IMapper<TInput, TOutput>
        where TInput : class 
        where TOutput: class
    {
        public TOutput Map(TInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return HandleMapping(input);
        }

        protected abstract TOutput HandleMapping(TInput input);
    }
}