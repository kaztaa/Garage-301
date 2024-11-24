using Garage301.Data;
using Garage301.Models;
using Garage301.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Garage301.Controllers
{
    public class AdminController : Controller
    {
        private readonly Garage301Context _context;

        public AdminController(Garage301Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageParkingSpots(string searchTerm, string sortBy = "SpotNumber", string sortDirection = "asc")
        {
            // Fetch the parking spots from the database
            var parkingSpots = _context.ParkingSpot
                .Include(p => p.ParkedVehicle)
                .AsQueryable();

            // Apply filtering based on searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                parkingSpots = parkingSpots.Where(p =>
                    p.SpotNumber.ToString().Contains(searchTerm) ||
                    p.Location.Contains(searchTerm) ||
                    (p.ParkedVehicle != null && p.ParkedVehicle.RegistrationNumber.Contains(searchTerm))
                );
            }

            // Apply sorting logic (existing logic remains the same)
            switch (sortBy)
            {
                case "SpotNumber":
                    parkingSpots = sortDirection == "asc"
                        ? parkingSpots.OrderBy(p => p.SpotNumber)
                        : parkingSpots.OrderByDescending(p => p.SpotNumber);
                    break;
                case "Status":
                    parkingSpots = sortDirection == "asc"
                        ? parkingSpots.OrderBy(p => p.IsOccupied ? 0 : 1)
                        : parkingSpots.OrderByDescending(p => p.IsOccupied ? 0 : 1);
                    break;
                case "Registration Number":
                    parkingSpots = sortDirection == "asc"
                        ? parkingSpots.OrderBy(p => p.ParkedVehicle.RegistrationNumber)
                        : parkingSpots.OrderByDescending(p => p.ParkedVehicle.RegistrationNumber);
                    break;
                default:
                    parkingSpots = parkingSpots.OrderBy(p => p.SpotNumber);
                    break;
            }

            // Pass data to the view
            var parkingSpotList = await parkingSpots.ToListAsync();
            ViewData["searchTerm"] = searchTerm;
            ViewData["SortDirection"] = sortDirection;
            ViewData["SortBy"] = sortBy;

            return View(parkingSpotList);
        }





        public async Task<IActionResult> ManageParkedVehicles(string searchTerm, string sortBy = "RegistrationNumber", string sortDirection = "asc")
        {
            // Apply filtering based on search term
            var vehicles = _context.ParkedVehicle
                .Include(v => v.VehicleType) // Eager load VehicleType
                .Include(v => v.ParkingSpot) // Eager load ParkingSpot
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                vehicles = vehicles.Where(v =>
                    v.RegistrationNumber.Contains(searchTerm) ||
                    v.VehicleType.Name.Contains(searchTerm) ||
                    v.Color.Contains(searchTerm) ||
                    (v.Make + " " + v.Model).Contains(searchTerm));
            }

            // Apply sorting logic
            vehicles = sortBy switch
            {
                "RegistrationNumber" => sortDirection == "asc"
                    ? vehicles.OrderBy(v => v.RegistrationNumber)
                    : vehicles.OrderByDescending(v => v.RegistrationNumber),
                "VehicleType" => sortDirection == "asc"
                    ? vehicles.OrderBy(v => v.VehicleType.Name)
                    : vehicles.OrderByDescending(v => v.VehicleType.Name),
                "Color" => sortDirection == "asc"
                    ? vehicles.OrderBy(v => v.Color)
                    : vehicles.OrderByDescending(v => v.Color),
                "MakeModel" => sortDirection == "asc"
                    ? vehicles.OrderBy(v => v.Make + " " + v.Model)
                    : vehicles.OrderByDescending(v => v.Make + " " + v.Model),
                "ParkingSpot" => sortDirection == "asc"
                    ? vehicles.OrderBy(v => v.ParkingSpot.SpotNumber)
                    : vehicles.OrderByDescending(v => v.ParkingSpot.SpotNumber),
                _ => vehicles.OrderBy(v => v.RegistrationNumber) // Default sort
            };

            // Fetch the filtered and sorted data asynchronously
            var vehicleList = await vehicles.ToListAsync();

            // Return the correct model type (IEnumerable<ParkedVehicle>)
            return View(vehicleList);
        }





        //[Authorize(Roles = "Admin")]
        //public IActionResult ManageParkingSpots(string searchTerm)
        //{
        //    // Fetch parking spots from the database (use your data access logic here)
        //    var parkingSpots = _context.ParkingSpot.Include(ps => ps.ParkedVehicle).ToList();

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        // Perform case-insensitive search by spot number or location
        //        parkingSpots = parkingSpots.Where(ps =>
        //            ps.SpotNumber.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        //            ps.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        //        ).ToList();
        //    }

        //    // Set the search term to the ViewData to retain the value in the search field
        //    ViewData["searchTerm"] = searchTerm;

        //    return View(parkingSpots);
        //}


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ParkingSpots(string searchTerm)
        {
            // Fetching parking spots and sorting them by SpotNumber asynchronously
            var parkingSpots = await _context.ParkingSpot
                .OrderBy(p => p.SpotNumber) // Sort by SpotNumber in ascending order
                .ToListAsync();

            // Fetching parked vehicles with their associated VehicleType asynchronously
            var parkedVehiclesQuery = _context.ParkedVehicle
                .Include(v => v.VehicleType)  // Ensure VehicleType is loaded
                .AsQueryable();

            // If search term is provided, filter the results
            if (!string.IsNullOrEmpty(searchTerm))
            {
                parkedVehiclesQuery = parkedVehiclesQuery.Where(v =>
                    v.RegistrationNumber.Contains(searchTerm) ||
                    v.VehicleType.Name.Contains(searchTerm) ||
                    v.Color.Contains(searchTerm) ||
                    (v.Make + " " + v.Model).Contains(searchTerm)
                );
            }

            // Execute the query and fetch the results asynchronously
            var parkedVehicles = await parkedVehiclesQuery.ToListAsync();

            // Create and populate the view model
            var viewModel = new AdminViewModel
            {
                ParkingSpots = parkingSpots,
                ParkedVehicles = parkedVehicles
            };

            // Return the view with the populated model
            return View("Admin", viewModel);
        }




        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult IncreaseParkingSpots()
        {
            // Get all existing spot numbers from the database
            var existingSpotNumbers = _context.ParkingSpot
                .Select(p => p.SpotNumber)
                .OrderBy(p => p) // Ensure they are ordered
                .ToList();

            int nextAvailableSpotNumber = 1;

            // Look for the first available spot number that's not already in the database
            while (existingSpotNumbers.Contains(nextAvailableSpotNumber))
            {
                nextAvailableSpotNumber++;
            }

            // Create a new parking spot with the first available number
            var newSpot = new ParkingSpot
            {
                SpotNumber = nextAvailableSpotNumber,
                IsOccupied = false,
                Location = $"Location {nextAvailableSpotNumber}"
            };

            // Add the new spot to the database and save changes
            _context.ParkingSpot.Add(newSpot);
            _context.SaveChanges();

            return RedirectToAction("ManageParkingSpots");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DecreaseParkingSpots(int id)
        {
            // Find the ParkingSpot by Id
            var parkingSpot = _context.ParkingSpot
                .FirstOrDefault(p => p.Id == id); // Retrieve the specific ParkingSpot

            if (parkingSpot != null && !parkingSpot.IsOccupied)
            {
                // Remove the ParkingSpot if it's not occupied
                _context.ParkingSpot.Remove(parkingSpot);
                _context.SaveChanges();
            }

            // Redirect back to the ParkingSpots view
            return RedirectToAction("ManageParkingSpots");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Redirect to ParkedVehiclesController's Details action
            return RedirectToAction("Details", "ParkedVehicles", new { id = id });
        }

        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Redirect to the CheckOut action of ParkedVehiclesController
            return RedirectToAction("CheckOut", "ParkedVehicles", new { id = id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Edit", "ParkedVehicles", new { id = id });
        }
    }

}
