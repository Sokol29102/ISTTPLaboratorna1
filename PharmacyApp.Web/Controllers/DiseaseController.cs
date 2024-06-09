
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Domain.Entities;
using PharmacyApp.Infrastructure.Data;
using System.Linq;

namespace PharmacyApp.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    [Authorize]
    public class DiseasesController : ControllerBase
	{
		private readonly PharmacyDbContext _context;

		public DiseasesController(PharmacyDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetDiseases()
		{
			var diseases = _context.Diseases.ToList();
			return Ok(diseases);
		}

		[HttpPost]
		public IActionResult CreateDisease([FromBody] Disease disease)
		{
			_context.Diseases.Add(disease);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetDiseases), new { id = disease.Id }, disease);
		}
	}
}
