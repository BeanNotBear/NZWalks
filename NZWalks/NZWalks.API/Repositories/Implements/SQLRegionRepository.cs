using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
	public class SQLRegionRepository : IRegionRepository
	{

		private readonly NZWalksDbContext dbContext;

		public SQLRegionRepository(NZWalksDbContext nZWalksDbContext)
		{
			this.dbContext = nZWalksDbContext;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.AddAsync(region);
			await dbContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var existingRegion = await dbContext.Regions.FindAsync(id);
			if (existingRegion == null)
			{
				return null;
			}

			dbContext.Regions.Remove(existingRegion);
			await dbContext.SaveChangesAsync();
			return existingRegion;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			var regionsDomain = await dbContext.Regions.ToListAsync();

			return regionsDomain;
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			var regionDomain = await dbContext.Regions.FindAsync(id);
			return regionDomain;
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await dbContext.Regions.FindAsync(id);
			if (existingRegion == null)
			{
				return null;
			}

			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			existingRegion.RegionImageUrl = region.RegionImageUrl;

			await dbContext.SaveChangesAsync();

			return existingRegion;
		}
	}
}
