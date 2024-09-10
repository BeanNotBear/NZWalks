using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

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

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<RegionDto> response = new List<RegionDto>();
			try
			{
				// Get all Regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:7076/api/Regions");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
			}
			catch (Exception ex)
			{
				// Logging
			}

			return View(response);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
		{
			var client = httpClientFactory.CreateClient();

			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://localhost:7076/api/Regions"),
				Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json"),
			};

			var httpResponseMessage = await client.SendAsync(httpRequestMessage);

			httpResponseMessage.EnsureSuccessStatusCode();

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

			if (response is not null)
			{
				return RedirectToAction("Index", "Regions");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetSingleRegion(Guid id)
		{
			var client = httpClientFactory.CreateClient();

			var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7076/api/Regions/{id.ToString()}");
			if (response is not null)
			{
				return View(response);
			}

			return View(null);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var client = httpClientFactory.CreateClient();
			var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7076/api/Regions/{id.ToString()}");
			if (response is not null)
			{
				return View(response);
			}

			return View(null);
		}

		//[HttpPut]
		//public async Task<IActionResult> Update(Guid id, RegionDto regionDto)
		//{

		//}

	}
}
