using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Domain.Entities;
using System.Threading.Tasks;

namespace PharmacyApp.Web.Controllers
{
    public class DiseaseController : Controller
    {
        private readonly IDataStorage<Disease> _diseaseService;

        public DiseaseController(IDataStorage<Disease> diseaseService)
        {
            _diseaseService = diseaseService;
        }

        public async Task<IActionResult> Index()
        {
            var diseases = await _diseaseService.GetAllAsync();
            return View(diseases);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Disease model)
        {
            if (ModelState.IsValid)
            {
                await _diseaseService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var disease = await _diseaseService.GetAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            return View(disease);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Disease model)
        {
            if (ModelState.IsValid)
            {
                await _diseaseService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
