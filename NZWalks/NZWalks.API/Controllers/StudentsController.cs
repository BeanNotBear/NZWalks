using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
	// GET: https://localhost:portnumber/api/Students
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		// GET: https://localhost:portnumber/api/Students
		[HttpGet]
		public IActionResult GetAllStudents()
		{
			string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };
			
			return Ok(studentNames);
		}
	}
}
