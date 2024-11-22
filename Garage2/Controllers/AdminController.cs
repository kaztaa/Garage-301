﻿using Garage301.Data;
using Garage301.Models;
using Garage301.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult ParkingSpots()
        {
            // Fetching parking spots and sorting them by SpotNumber
            var parkingSpots = _context.ParkingSpot
                .OrderBy(p => p.SpotNumber) // Sort by SpotNumber in ascending order
                .ToList();

            // Fetching parked vehicles with their associated VehicleType
            var parkedVehicles = _context.ParkedVehicle
                .Include(v => v.VehicleType)  // Ensure VehicleType is loaded
                .ToList();

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
                Location = $"Section {nextAvailableSpotNumber}"
            };

            // Add the new spot to the database and save changes
            _context.ParkingSpot.Add(newSpot);
            _context.SaveChanges();

            return RedirectToAction("ParkingSpots");
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
            return RedirectToAction("ParkingSpots");
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
