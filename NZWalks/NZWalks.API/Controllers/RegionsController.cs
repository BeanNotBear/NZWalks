using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
	// https://localhost:portnumber/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{

		private readonly NZWalksDbContext _dbcontext;

		public RegionsController(NZWalksDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		// GET ALL REGIONS
		// GET: https://localhost:portnumber/api/regions
		[HttpGet]
		public IActionResult GetAll()
		{
			// Get data from database - Domain models
			var regionsDomain = _dbcontext.Regions.ToList();

			// Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = regionDomain.Id,
					Name = regionDomain.Name,
					Code = regionDomain.Code,
					RegionImageUrl = regionDomain.RegionImageUrl
				});
			}

			// Return DTOs
			return Ok(regionsDto);
		}

		// GET SINGLE REGION (Get region by id)
		// GET: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			// Get Region Domain Model From Database
			var regionDomain = _dbcontext.Regions.Find(id);

			// Check Region Domain Model is null
			if (regionDomain == null)
			{
				return NotFound();
			}

			// Map Region Domain Model to Region DTO
			var regionDto = new RegionDto()
			{
				Id = regionDomain.Id,
				Name = regionDomain.Name,
				Code = regionDomain.Code,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			return Ok(regionDto);
		}
	}
}
