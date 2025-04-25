using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.CacheServices
{
    public class LocationHierarchyCacheService : ILocationHierarchyCacheService
    {
        private readonly ICacheService _cache;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<State> _stateRepository;
        private readonly IRepository<City> _cityRepository;
        private const string CacheKey = "LocationHierarchy";

        public LocationHierarchyCacheService(ICacheService cache, IRepository<Country> countryRepository, IRepository<State> stateRepository, IRepository<City> cityRepository)
        {
            _cache = cache;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
        }

        public async Task<bool> IsValidLocationAsync(Guid countryId, Guid stateId, Guid cityId)
        {
            var hierarchy = await _cache.GetOrCreateAsync(CacheKey, BuildHierarchyAsync, TimeSpan.FromHours(12));

            return hierarchy!.Contains((countryId, stateId, cityId));
        }

        private async Task<HashSet<(Guid countryId, Guid stateId, Guid cityId)>> BuildHierarchyAsync()
        {
            var countries = await _countryRepository.GetQuery()
                .Select(c => c.Id)?.ToListAsync()!;

            var states = await _stateRepository.GetQuery()
                .Select(s => new { s.Id, s.CountryId })?.ToListAsync()!;

            var cities = await _cityRepository.GetQuery()
                .Select(c => new { c.Id, c.StateId })?.ToListAsync()!;

            var stateMap = states.ToDictionary(s => s.Id, s => s.CountryId);
            var result = new HashSet<(Guid countryId, Guid stateId, Guid cityId)>();

            foreach (var city in cities)
            {
                if (stateMap.TryGetValue(city.StateId, out var countryId))
                    result.Add((countryId, city.StateId, city.Id));
            }

            return result;
        }

        public void ClearCache()
        {
            _cache.Remove(CacheKey);
        }
    }
}