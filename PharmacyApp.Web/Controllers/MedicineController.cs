using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Domain.Entities;
using System.Threading.Tasks;

namespace PharmacyApp.Web.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IDataStorage<Medicine> _medicineService;

        public MedicineController(IDataStorage<Medicine> medicineService)
        {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetAllAsync();
            return View(medicines);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Medicine model)
        {
            if (ModelState.IsValid)
            {

                model.IconUrl = $"/images/medicines/{model.Name}.png";
                await _medicineService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var medicine = await _medicineService.GetAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Medicine model)
        {
            if (ModelState.IsValid)
            {
            
                model.IconUrl = $"/images/medicines/{model.Name}.png";
                await _medicineService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
