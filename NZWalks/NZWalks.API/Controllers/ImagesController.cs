using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{

		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}

		// POST: /api/Images/Upload
		[HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
		{
			ValidateFileUpLoad(imageUploadRequestDto);

			if (ModelState.IsValid)
			{
				// Convert DTO to Domain Model
				var imageDomainModel = new Image
				{
					File = imageUploadRequestDto.File,
					FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
					FileSizeBytes = imageUploadRequestDto.File.Length,
					FileName = imageUploadRequestDto.FileName,
					FileDescription = imageUploadRequestDto.FileDescription,
				};

				// User repository to upload
				await imageRepository.Upload(imageDomainModel);
				return Ok(imageDomainModel);
			}

			return BadRequest(ModelState);
		}

		private void ValidateFileUpLoad(ImageUploadRequestDto imageUploadRequestDto)
		{
			var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

			if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
			{
				ModelState.AddModelError("file", "Unsupported file extension");
			}

			if (imageUploadRequestDto.File.Length > 10485760)
			{
				ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller file.");
			}
		}

	}
}
