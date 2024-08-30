using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

	// /api/walks 
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{

		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;

		public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		// CREATE Walk
		// POST: /api/walks
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

			await walkRepository.CreateAsync(walkDomain);

			return CreatedAtAction(nameof(GetById), new { id = walkDomain.Id }, mapper.Map<WalkDto>(walkDomain));
		}

		// GET all Walks
		// GET: /api/walks/filterOn=name&filterQuery=track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] QueryParameters queryParameters)
		{
			var walksDomain = await walkRepository.GetAllAsync(queryParameters);
			var apiResponse = new APIResponse(true, "OK", mapper.Map<PaginatedList<WalkDto>>(walksDomain));
			return Ok(apiResponse);
		}

		// GET Walk by Id
		// GET: /api/walks/{id}
		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomain = await walkRepository.GetByIdAsync(id);

			if (walkDomain == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<WalkDto>(walkDomain));
		}

		// UPDATE Walk
		// PUT: /api/walks/{id}
		[HttpPut]
		[Route("{id:guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
		{

			var walkDomain = await walkRepository.UpdateAsync(id, mapper.Map<Walk>(updateWalkRequestDto));
			if (walkDomain == null)
			{
				return NotFound();
			}

			return Ok(walkDomain);

		}

		// DELETE Walk
		// DELETE: /api/walks/{id}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var deletedWalk = await walkRepository.DeleteAsync(id);
			if (deletedWalk == null)
			{
				return NotFound();
			}
			return NoContent();
		}

	}
}
