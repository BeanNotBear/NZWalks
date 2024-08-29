using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk?> GetByIdAsync(Guid id);
		Task<Walk> CreateAsync(Walk walk);

		Task<List<Walk>> GetAllAsync();
	}
}
