using Cars.Core.Domain;
using Cars.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarServices _carServices;

        public CarsController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carServices.GetAllCarsAsync();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _carServices.GetCarByIdAsync(id);
            if (car == null)
                return NotFound();
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carServices.CreateCarAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }
    }
}
