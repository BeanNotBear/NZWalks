using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Text.RegularExpressions;

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

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var walkDomain = await dbContext.Walks.FindAsync(id);
			if (walkDomain == null)
			{
				return null;
			}

			dbContext.Walks.Remove(walkDomain);
			await dbContext.SaveChangesAsync();
			return walkDomain;
		}

		public async Task<PaginatedList<Walk>> GetAllAsync(QueryParameters queryParameters)
		{
			var walksDomain = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

			// Filtering by Name
			if (!string.IsNullOrWhiteSpace(queryParameters.Name))
			{
				walksDomain = walksDomain.Where(x => x.Name == queryParameters.Name);
			}

			// Filtering by range of length
			if (queryParameters.MinLength != null && queryParameters.MaxLength != null)
			{
				walksDomain = walksDomain.Where(x => (x.LengthInKm >= queryParameters.MinLength && x.LengthInKm <= queryParameters.MaxLength));
			}

			// Sorting
			if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
			{
				if (queryParameters.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walksDomain = queryParameters.IsAscending ? walksDomain.OrderBy(x => x.Name) : walksDomain.OrderByDescending(x => x.Name);
				}
				else if (queryParameters.SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walksDomain = queryParameters.IsAscending ? walksDomain.OrderBy(x => x.LengthInKm) : walksDomain.OrderByDescending(x => x.LengthInKm);
				}

			}

			// Pagination
			var skipResult = (queryParameters.PageNumber - 1) * queryParameters.PageSize;

			var numberOfRecords = await walksDomain.CountAsync();
			var totalPage = (int)Math.Ceiling((double)numberOfRecords / queryParameters.PageSize);
			var items = await walksDomain.Skip(skipResult).Take(queryParameters.PageSize).ToListAsync();

			var result = new PaginatedList<Walk>(items, queryParameters.PageNumber, totalPage);
			return result;
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
