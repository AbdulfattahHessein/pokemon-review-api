using AutoMapper;
using PokemonReviewApp.DTOs;
using ServiceCollectionAccessorService;

namespace PokemonReviewApp.Helpers
{
    public static class MapperHelper
    {
        private static readonly IMapper _mapper = ServiceCollectionAccessor
                                                        .Services
                                                        .BuildServiceProvider()
                                                        .GetRequiredService<IMapper>();
        public static TDest MapTo<TDest>(this object entity) //where TDest : IDto
        {
            return (TDest)_mapper.Map(entity, entity.GetType(), typeof(TDest));
        }

        public static IEnumerable<TDest> MapTo<TDest>(this IEnumerable<object> entities) //where TDest : IDto
        {
            return (IEnumerable<TDest>)_mapper.Map(entities, entities.First().GetType(), typeof(IEnumerable<TDest>));
        }

    }
}
