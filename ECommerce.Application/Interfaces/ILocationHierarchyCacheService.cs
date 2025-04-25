namespace ECommerce.Application.Interfaces
{
    public interface ILocationHierarchyCacheService
    {
        Task<bool> IsValidLocationAsync(Guid countryId, Guid stateId, Guid cityId);

        void ClearCache();
    }
}