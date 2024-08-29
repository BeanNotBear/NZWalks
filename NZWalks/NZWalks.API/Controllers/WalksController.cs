using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			if (ModelState.IsValid)
			{
				var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

				await walkRepository.CreateAsync(walkDomain);

				return CreatedAtAction(nameof(GetById), new { id = walkDomain.Id }, mapper.Map<WalkDto>(walkDomain));
			}
			return BadRequest(ModelState);
		}

		// GET all Walks
		// GET: /api/walks
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var walksDomain = await walkRepository.GetAllAsync();
			return Ok(mapper.Map<List<WalkDto>>(walksDomain));
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
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
		{
			if (!ModelState.IsValid)
			{
				var walkDomain = await walkRepository.UpdateAsync(id, mapper.Map<Walk>(updateWalkRequestDto));
				if (walkDomain == null)
				{
					return NotFound();
				}

				return Ok(walkDomain);
			}
			return BadRequest(ModelState);
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
