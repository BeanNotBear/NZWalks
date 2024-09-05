using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext : DbContext
	{
		public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
		{
		}

		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }
		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data for Difficulties
			var difficulties = new List<Difficulty>()
			{
				new Difficulty()
				{
					Id = Guid.Parse("a28f34ab-9898-4a70-b057-fc2a98470aeb"),
					Name = "Easy"
				},
				new Difficulty()
				{
					Id = Guid.Parse("36f72fe7-43d6-4e3f-8934-d05b13a0e7de"),
					Name = "Medium"
				},
				new Difficulty()
				{
					Id = Guid.Parse("9ea58ba1-094a-4d5d-b8d9-39ce0ec0a3f4"),
					Name = "Hard"
				}
			};

			modelBuilder.Entity<Difficulty>().HasData(difficulties);

			// Seed data for Regions
			var regions = new List<Region>()
			{
				new Region()
				{
					Id = Guid.Parse("ac8acc08-94ff-47ff-6862-08dcc349a7bc"),
					Code = "REG002",
					Name = "Europe",
					RegionImageUrl = "http://example.com/region/europe.png"
				},
				new Region()
				{
					Id = Guid.Parse("7d388ef1-33a0-478d-778e-08dcc5f1aee4"),
					Code = "VN",
					Name = "Vietnam",
					RegionImageUrl = "https://example.com/images/vietnam.png"
				},
				new Region()
				{
					Id = Guid.Parse("55bb048f-9d92-4cbe-a2dd-08dcc73d9c77"),
					Code = "AF",
					Name = "Africa",
					RegionImageUrl = "https://example.com/images/africa.png"
				},
				new Region()
				{
					Id = Guid.Parse("8a577820-7d28-4bae-a2df-08dcc73d9c77"),
					Code = "EG",
					Name = "Egypt",
					RegionImageUrl = "https://example.com/images/egypt.png"
				},
				new Region()
				{
					Id = Guid.Parse("36cad70d-0f48-4b63-729e-08dcc742cc2c"),
					Code = "CN",
					Name = "China",
					RegionImageUrl = "https://example.com/images/china.png"
				},
				new Region()
				{
					Id = Guid.Parse("f79b9c8d-d7e3-4375-9134-24782dbd7f68"),
					Code = "REG006",
					Name = "Australia",
					RegionImageUrl = "http://example.com/region/australia.png"
				}
			};

			modelBuilder.Entity<Region>().HasData(regions);
		}
	}
}
