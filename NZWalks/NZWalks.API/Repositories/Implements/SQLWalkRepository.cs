using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
	public class SQLWalkRepository : IWalkRepository
	{

		private readonly NZWalksDbContext dbContext;

		public SQLWalkRepository(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Walk> CreateAsync(Walk walk)
		{
			await dbContext.Walks.AddAsync(walk);
			await dbContext.SaveChangesAsync();
			return walk;
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Walk>> GetAllAsync()
		{
			var walksDomain = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
			return walksDomain;
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			var walkDomain = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);

			if (walkDomain == null)
			{
				return null;
			}

			return walkDomain;
		}

		public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
		{
			var existingWalk = await dbContext.Walks.FindAsync(id);

			if (existingWalk == null)
			{
				return null;
			}

			existingWalk.Name = walk.Name;
			existingWalk.Description = walk.Description;
			existingWalk.LengthInKm = walk.LengthInKm;
			existingWalk.WalkImageUrl = walk.WalkImageUrl;
			existingWalk.RegionId = walk.RegionId;
			existingWalk.DifficultyId = walk.DifficultyId;

			await dbContext.SaveChangesAsync();
			return existingWalk;
		}
	}
}
