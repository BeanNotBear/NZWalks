using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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

		// POST to Create a New Region
		// POST: https://localhost:portnumber/api/regions
		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map DTO to Domain model
			var regionDomainModel = new Region()
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use Domain Model to create Region
			_dbcontext.Regions.Add(regionDomainModel);
			_dbcontext.SaveChanges();

			// Map Domain Model to DTO
			var regionDto = new RegionDto()
			{
				Id = regionDomainModel.Id,
				Name = regionDomainModel.Name,
				Code = regionDomainModel.Code,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
		}

	}
}
