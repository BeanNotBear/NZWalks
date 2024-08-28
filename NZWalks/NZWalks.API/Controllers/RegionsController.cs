using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	// https://localhost:portnumber/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{

		private readonly NZWalksDbContext _dbcontext;
		private readonly IRegionRepository regionRepository;

		public RegionsController(NZWalksDbContext dbcontext, IRegionRepository regionRepository)
		{
			_dbcontext = dbcontext;
			this.regionRepository = regionRepository;
		}

		// GET ALL REGIONS
		// GET: https://localhost:portnumber/api/regions
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			// Get data from database - Domain models
			var regionsDomain = await regionRepository.GetAllAsync();

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
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			// Get Region Domain Model From Database
			var regionDomain = await regionRepository.GetByIdAsync(id);

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
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map DTO to Domain model
			var regionDomainModel = new Region()
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use Domain Model to create Region
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

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

		// PUT to Update a Region
		// PUT: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{

			// Map Update Region Request to Region Domain Model
			var regionDomainModel = new Region()
			{
				Code = updateRegionRequestDto.Code,
				Name = updateRegionRequestDto.Name,
				RegionImageUrl = updateRegionRequestDto.RegionImageUrl
			};

			// Check Region Domain Model if existed
			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Map Region Domain Model to DTO
			var regionDto = new RegionDto()
			{
				Id = regionDomainModel.Id,
				Name = regionDomainModel.Name,
				Code = regionDomainModel.Code,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return Ok(regionDto);
		}

		// DELETE A REGION
		// DELETE: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			// Find region domain by id
			var regionDomain = await regionRepository.DeleteAsync(id);

			// check region domain is existed
			if (regionDomain == null)
			{
				return NotFound();
			}

			return NoContent();
		}
		 
	}
}
