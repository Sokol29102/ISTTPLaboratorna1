// MedicinesController.cs
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
    public class MedicinesController : ControllerBase
	{
		private readonly PharmacyDbContext _context;

		public MedicinesController(PharmacyDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetMedicines()
		{
			var medicines = _context.Medicines.ToList();
			return Ok(medicines);
		}

		[HttpPost]
		public IActionResult CreateMedicine([FromBody] Medicine medicine)
		{
			_context.Medicines.Add(medicine);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetMedicines), new { id = medicine.Id }, medicine);
		}
	}
}
