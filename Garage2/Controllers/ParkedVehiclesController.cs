using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;
using Garage2.Models.ViewModels;

namespace Garage2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage301Context _context;

        public ParkedVehiclesController(Garage301Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index(string searchField, int type, string sortBy, string currentFilter, int currentType)
        {
            var vehicles = await _context.ParkedVehicle.Include(v => v.ParkingSpot).Include(v => v.VehicleType).ToListAsync();
            ViewData["TypeSortParam"] = sortBy == "type_desc" ? "type_asc" : "type_desc";
            ViewData["RegNrSortParam"] = sortBy == "regNr_desc" ? "regNr_asc" : "regNr_desc";
            ViewData["ArrivalTimeSortParam"] = sortBy == "at_desc" ? "at_asc" : "at_desc";
            ViewData["ParkedDurationSortParam"] = sortBy == "pd_desc" ? "pd_asc" : "pd_desc";

            if (string.IsNullOrEmpty(searchField))
            {
                searchField = currentFilter;
            }
            if (type == 0)
            {
                type = currentType;
            }

            ViewData["CurrentFilter"] = searchField;
            ViewData["CurrentType"] = type;

            if (!string.IsNullOrEmpty(searchField))
            {
                searchField = searchField.ToUpper();
                vehicles = vehicles.Where(e =>
                    (type == 1 && e.RegistrationNumber.ToUpper().Contains(searchField)) ||
                    (type == 2 && e.VehicleType.Name.ToUpper() == searchField) ||
                    (type == 3 && e.Color.ToUpper() == searchField) ||
                    (type == 4 && e.Make.ToUpper() == searchField) ||
                    (type == 5 && e.Model.ToUpper() == searchField)
                ).ToList();
            }

            switch (sortBy)
            {
                case "type_desc":
                    vehicles = vehicles.OrderByDescending(e => e.VehicleType.Name).ToList();
                    break;
                case "type_asc":
                    vehicles = vehicles.OrderBy(e => e.VehicleType.Name).ToList();
                    break;
                case "regNr_desc":
                    vehicles = vehicles.OrderByDescending(e => e.RegistrationNumber).ToList();
                    break;
                case "regNr_asc":
                    vehicles = vehicles.OrderBy(e => e.RegistrationNumber).ToList();
                    break;
                case "at_desc":
                    vehicles = vehicles.OrderByDescending(e => e.ArrivalTime).ToList();
                    break;
                case "at_asc":
                    vehicles = vehicles.OrderBy(e => e.ArrivalTime).ToList();
                    break;
                case "pt_desc":
                    vehicles = vehicles.OrderByDescending(e => e.ParkedDuration).ToList();
                    break;
                case "pt_asc":
                    vehicles = vehicles.OrderBy(e => e.ParkedDuration).ToList();
                    break;
            }

            ViewData["ParkingSpots"] = await _context.ParkingSpot.OrderBy(s => s.SpotNumber).ToListAsync();
            return View(vehicles);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.Include(v => v.ParkingSpot).Include(v => v.VehicleType).FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/CheckIn
        public async Task<IActionResult> CheckIn()
        {
            var viewModel = new ParkedVehicleCheckInViewModel();
            ViewData["VehicleTypes"] = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name");
            ViewData["ParkingSpots"] = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied).ToListAsync(), "Id", "SpotNumber");

            return View(viewModel);
        }


        // POST: ParkedVehicles/CheckIn
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CheckIn(ParkedVehicleCheckInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if a vehicle with the same RegistrationNumber already exists
                var existingVehicle = await _context.ParkedVehicle
                    .FirstOrDefaultAsync(v => v.RegistrationNumber == viewModel.RegistrationNumber);

                if (existingVehicle != null)
                {
                    // Add an error to the ModelState if the registration number already exists
                    ModelState.AddModelError("RegistrationNumber", "This registration number already exists.");
                }

                if (ModelState.IsValid) // Re-check if the model state is valid after adding the error
                {
                    var parkingSpot = await _context.ParkingSpot.FindAsync(viewModel.ParkingSpotId);
                    if (parkingSpot != null && !parkingSpot.IsOccupied)
                    {
                        parkingSpot.IsOccupied = true;
                        _context.Update(parkingSpot);

                        var parkedVehicle = new ParkedVehicle
                        {
                            VehicleTypesId = viewModel.VehicleTypesId,
                            RegistrationNumber = viewModel.RegistrationNumber,
                            Color = viewModel.Color,
                            Make = viewModel.Make,
                            Model = viewModel.Model,
                            NumberOfWheels = viewModel.NumberOfWheels,
                            ArrivalTime = DateTime.Now,
                            ParkingSpotId = viewModel.ParkingSpotId
                        };

                        await _context.AddAsync(parkedVehicle);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("ParkingSpotId", "Selected parking spot is not available.");
                    }
                }
            }

            // Repopulate the dropdown lists if ModelState is invalid
            ViewData["VehicleTypes"] = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", viewModel.VehicleTypesId);
            ViewData["ParkingSpots"] = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied).ToListAsync(), "Id", "SpotNumber", viewModel.ParkingSpotId);

            return View(viewModel);
        }



        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(v => v.VehicleType)
                .Include(v => v.ParkingSpot)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var viewModel = new EditParkedVehicleViewModel
            {
                Id = parkedVehicle.Id,
                VehicleTypesId = parkedVehicle.VehicleTypesId,
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                Color = parkedVehicle.Color,
                Make = parkedVehicle.Make,
                Model = parkedVehicle.Model,
                NumberOfWheels = parkedVehicle.NumberOfWheels,
                ArrivalTime = parkedVehicle.ArrivalTime,
                ParkingSpotId = parkedVehicle.ParkingSpotId,
                VehicleTypes = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", parkedVehicle.VehicleTypesId),
                ParkingSpots = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied || s.Id == parkedVehicle.ParkingSpotId).ToListAsync(), "Id", "SpotNumber", parkedVehicle.ParkingSpotId)
            };

            return View(viewModel);
        }


        // POST: ParkedVehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditParkedVehicleViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVehicle = await _context.ParkedVehicle
                        .Include(v => v.ParkingSpot)
                        .FirstOrDefaultAsync(v => v.Id == id);

                    if (existingVehicle == null)
                    {
                        return NotFound();
                    }

                    // Handle changes to ParkingSpotId
                    if (existingVehicle.ParkingSpotId != viewModel.ParkingSpotId)
                    {
                        // If there was a previous parking spot, mark it as not occupied
                        if (existingVehicle.ParkingSpot != null)
                        {
                            existingVehicle.ParkingSpot.IsOccupied = false;
                            _context.Update(existingVehicle.ParkingSpot);
                        }

                        // Assign new parking spot and mark it as occupied
                        if (viewModel.ParkingSpotId.HasValue)
                        {
                            var newParkingSpot = await _context.ParkingSpot.FindAsync(viewModel.ParkingSpotId);
                            if (newParkingSpot != null && !newParkingSpot.IsOccupied)
                            {
                                newParkingSpot.IsOccupied = true;
                                _context.Update(newParkingSpot);
                            }
                            else
                            {
                                ModelState.AddModelError("ParkingSpotId", "The selected parking spot is not available.");
                                viewModel.VehicleTypes = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", viewModel.VehicleTypesId);
                                viewModel.ParkingSpots = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied || s.Id == viewModel.ParkingSpotId).ToListAsync(), "Id", "SpotNumber", viewModel.ParkingSpotId);
                                return View(viewModel);
                            }
                        }
                    }

                    // Update vehicle properties
                    existingVehicle.VehicleTypesId = viewModel.VehicleTypesId;
                    existingVehicle.RegistrationNumber = viewModel.RegistrationNumber;
                    existingVehicle.Color = viewModel.Color;
                    existingVehicle.Make = viewModel.Make;
                    existingVehicle.Model = viewModel.Model;
                    existingVehicle.NumberOfWheels = viewModel.NumberOfWheels;
                    existingVehicle.ArrivalTime = viewModel.ArrivalTime;
                    existingVehicle.ParkingSpotId = viewModel.ParkingSpotId;

                    _context.Update(existingVehicle);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            viewModel.VehicleTypes = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", viewModel.VehicleTypesId);
            viewModel.ParkingSpots = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied || s.Id == viewModel.ParkingSpotId).ToListAsync(), "Id", "SpotNumber", viewModel.ParkingSpotId);

            return View(viewModel);
        }




        // GET: ParkedVehicles/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.Include(v => v.ParkingSpot).FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.Include(v => v.ParkingSpot).FirstOrDefaultAsync(v => v.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var checkoutTime = DateTime.Now;
            var parkingDuration = checkoutTime - parkedVehicle.ArrivalTime;
            decimal pricePerHour = 10;
            decimal cost = (decimal)Math.Ceiling(parkingDuration.TotalHours) * pricePerHour;

            var receipt = new Receipt
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ArrivalTime = parkedVehicle.ArrivalTime,
                CheckoutTime = checkoutTime,
                ParkingCost = cost
            };

            var parkingSpot = parkedVehicle.ParkingSpot;
            if (parkingSpot != null)
            {
                parkingSpot.IsOccupied = false;
                _context.Update(parkingSpot);
            }
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();

            return View("Receipt", receipt);
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Stats()
        {
            decimal pricePerHour = 10;

            var parkedVehicles = await _context.ParkedVehicle
                .Where(v => v.CheckoutTime == null)
                .ToListAsync();

            var totalRevenue = parkedVehicles
                .Select(v => (decimal)(DateTime.Now - v.ArrivalTime).TotalHours * pricePerHour)
                .Sum();

            var stats = new Stats
            {
                TotalVehicles = await _context.ParkedVehicle.CountAsync(),
                Motorcycles = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 2), // Assuming 2 is Motorcycle
                Cars = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 1),        // Assuming 1 is Car
                Trucks = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 3),
                Buses = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 4),
                Vans = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 5),
                Bicycles = await _context.ParkedVehicle.CountAsync(v => v.VehicleTypesId == 6),
                Wheels = await _context.ParkedVehicle.SumAsync(v => v.NumberOfWheels),
                Revenue = totalRevenue
            };

            if (stats == null)
            {
                return NotFound(); // This will return a "Not Found" error if the stats object is null
            }

            return View(stats);
        }

    }
}
