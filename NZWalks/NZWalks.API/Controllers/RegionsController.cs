using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

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
			var regions = _dbcontext.Regions.ToList();
			return Ok(regions);
		}

		// GET SINGLE REGION (Get region by id)
		// GET: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			var region = _dbcontext.Regions.Find(id);

			if (region == null)
			{
				return NotFound();
			}

			return Ok(region);
		}
	}
}
