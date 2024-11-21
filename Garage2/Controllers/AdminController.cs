using Garage301.Data;
using Garage301.Models;
using Microsoft.AspNetCore.Mvc;

namespace Garage301.Controllers
{
    public class AdminController : Controller
    {
        private readonly Garage301Context _context;

        public AdminController(Garage301Context context)
        {
            _context = context;
        }

        // Visa lista över parkeringsplatser
        public IActionResult ParkingSpots()
        {
            var parkingSpots = _context.ParkingSpot.ToList();
            return View(parkingSpots);
        }

        // Lägg till en parkeringsplats
        [HttpPost]
        public async Task<IActionResult> AddParkingSpot()
        {
            if (_context.ParkingSpot.Count() < ParkingSpot.MaxParkingSpots)
            {
                var newSpot = new ParkingSpot
                {
                    SpotNumber = _context.ParkingSpot.Count() + 1,
                    IsOccupied = false
                };

                _context.ParkingSpot.Add(newSpot);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ParkingSpots));
        }

        // Ta bort en parkeringsplats
        [HttpPost]
        public async Task<IActionResult> RemoveParkingSpot(int id)
        {
            var spot = await _context.ParkingSpot.FindAsync(id);
            if (spot != null && !spot.IsOccupied)
            {
                _context.ParkingSpot.Remove(spot);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ParkingSpots));
        }
    }

}
