using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk?> GetByIdAsync(Guid id);
		Task<Walk> CreateAsync(Walk walk);
		Task<PaginatedList<Walk>> GetAllAsync(QueryParameters queryParameters);
		Task<Walk?> UpdateAsync(Guid id, Walk walk);

		Task<Walk?> DeleteAsync(Guid id);
	}
}
