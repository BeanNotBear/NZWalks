using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
	public class RegionsController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IConfiguration configuration;

		public RegionsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			this.httpClientFactory = httpClientFactory;
			this.configuration = configuration;
		}

		public async Task<IActionResult> Index()
		{
			List<RegionDto> response = new List<RegionDto>();
			try
			{
				// Get all Regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync(configuration["Api:GetAllRegions"]);

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());  
			}
			catch (Exception ex)
			{
				// Logging
			}

			return View(response);
		}
	}
}
