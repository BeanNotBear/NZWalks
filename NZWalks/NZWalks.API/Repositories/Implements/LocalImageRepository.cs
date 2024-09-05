using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly NZWalksDbContext nZWalksDbContext;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext nZWalksDbContext)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.nZWalksDbContext = nZWalksDbContext;
		}

		public async Task<Image> Upload(Image image)
		{
			var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

			// Upload Image to Local
			using var stream = new FileStream(localFilePath, FileMode.Create);
			await image.File.CopyToAsync(stream);

			var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

			image.FilePath = urlFilePath;

			// Add Image to the Images table
			await nZWalksDbContext.Images.AddAsync(image);
			await nZWalksDbContext.SaveChangesAsync();

			return image;
		}
	}
}
