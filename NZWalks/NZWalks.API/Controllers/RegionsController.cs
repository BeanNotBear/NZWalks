using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
	// https://localhost:portnumber/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{

		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
		private readonly ILogger<RegionsController> logger;

		public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
		{
			this.regionRepository = regionRepository;
			this.mapper = mapper;
			this.logger = logger;
		}

		// GET ALL REGIONS
		// GET: https://localhost:portnumber/api/regions
		[HttpGet]
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetAll()
		{
			logger.LogInformation("GetAllRegions Action Method was invoked");

			// Get data from database - Domain models
			var regionsDomain = await regionRepository.GetAllAsync();

			logger.LogInformation($"Finished GetAllRegions request with data {JsonSerializer.Serialize(regionsDomain)}");
			// Return DTOs
			return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
		}

		// GET SINGLE REGION (Get region by id)
		// GET: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:guid}")]
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			// Get Region Domain Model From Database
			var regionDomain = await regionRepository.GetByIdAsync(id);

			// Check Region Domain Model is null
			if (regionDomain == null)
			{
				return NotFound();
			}

			// Return Region Dto
			return Ok(mapper.Map<RegionDto>(regionDomain));
		}

		// POST to Create a New Region
		// POST: https://localhost:portnumber/api/regions
		[HttpPost]
		[ValidateModel]
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{

			// Map DTO to Domain model
			var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

			// Use Domain Model to create Region
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, mapper.Map<RegionDto>(regionDomainModel));
		}

		// PUT to Update a Region
		// PUT: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:guid}")]
		[ValidateModel]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			// Map Update Region Request to Region Domain Model
			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

			// Check Region Domain Model if existed
			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Return region dto
			return Ok(mapper.Map<RegionDto>(regionDomainModel));

		}

		// DELETE A REGION
		// DELETE: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:guid}")]
		[Authorize(Roles = "Writer")]
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
