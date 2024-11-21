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
            //var parkingSpots = _context.ParkingSpot; // Hämtar alla parkeringsplatser från databasen
            var parkingSpots = _context.ParkingSpot.ToList(); // Hämtar alla parkeringsplatser från databasen
            //return View(parkingSpots); // Skickar data till vyn
            return View("Admin", parkingSpots);
        }

        [HttpPost]
        public IActionResult IncreaseParkingSpots()
        {
            int currentMax = _context.ParkingSpot.Any()
                ? _context.ParkingSpot.Max(p => p.SpotNumber)
                : 0;

            if (currentMax < ParkingSpot.MaxParkingSpots)
            {
                var newSpot = new ParkingSpot
                {
                    SpotNumber = currentMax + 1,
                    IsOccupied = false,
                    Location = $"Section {currentMax + 1}"
                };

                _context.ParkingSpot.Add(newSpot);
                _context.SaveChanges();
            }

            return RedirectToAction("ParkingSpots");
        }

        [HttpPost]
        public IActionResult DecreaseParkingSpots()
        {
            var lastSpot = _context.ParkingSpot
                .OrderByDescending(p => p.SpotNumber)
                .FirstOrDefault();

            if (lastSpot != null && !lastSpot.IsOccupied)
            {
                _context.ParkingSpot.Remove(lastSpot);
                _context.SaveChanges();
            }

            return RedirectToAction("ParkingSpots");
        }
    }

}
