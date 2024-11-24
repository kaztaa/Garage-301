using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage301.Data;
using Garage301.Models;
using Garage301.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Garage301.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage301Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParkedVehiclesController(Garage301Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchField, int type, string sortBy, string currentFilter, int currentType)
        {
            // Redirect admin users
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            // Set up searchField and type fallback
            if (string.IsNullOrEmpty(searchField))
            {
                searchField = currentFilter;
            }
            if (type == 0)
            {
                type = currentType;
            }

            var isAdmin = User.IsInRole("Admin");

            var query = _context.ParkedVehicle
                                .Include(v => v.ParkingSpot)
                                .Include(v => v.VehicleType)
                                .AsQueryable();

            // Filter for non-admin users
            if (!isAdmin)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(currentUserId))
                {
                    query = query.Where(e => e.ApplicationUserId == currentUserId);
                }
            }

            // Apply search filter if needed
            if (!string.IsNullOrEmpty(searchField))
            {
                searchField = searchField.ToUpper();
                query = query.Where(e =>
                    (type == 1 && e.RegistrationNumber.ToUpper().Contains(searchField)) ||
                    (type == 2 && e.VehicleType.Name.ToUpper() == searchField) ||
                    (type == 3 && e.Color.ToUpper() == searchField) ||
                    (type == 4 && e.Make.ToUpper() == searchField) ||
                    (type == 5 && e.Model.ToUpper() == searchField)
                );
            }

            // Sorting logic
            switch (sortBy)
            {
                case "type_desc":
                    query = query.OrderByDescending(e => e.VehicleType.Name);
                    break;
                case "type_asc":
                    query = query.OrderBy(e => e.VehicleType.Name);
                    break;
                case "regNr_desc":
                    query = query.OrderByDescending(e => e.RegistrationNumber);
                    break;
                case "regNr_asc":
                    query = query.OrderBy(e => e.RegistrationNumber);
                    break;
                case "at_desc":
                    query = query.OrderByDescending(e => e.ArrivalTime);
                    break;
                case "at_asc":
                    query = query.OrderBy(e => e.ArrivalTime);
                    break;
                default:
                    query = query.OrderBy(e => e.RegistrationNumber);
                    break;
            }

            // Fetch the list of vehicles (with optional sorting already applied)
            var vehicles = await query.ToListAsync();

            // Fetch parking spots for display
            var parkingSpots = await _context.ParkingSpot.OrderBy(s => s.SpotNumber).ToListAsync();

            // Pass data to the view using ViewData
            ViewData["Vehicles"] = vehicles;
            ViewData["SearchField"] = searchField;
            ViewData["CurrentFilter"] = currentFilter;
            ViewData["CurrentType"] = type;
            ViewData["SortBy"] = sortBy;
            ViewData["ParkingSpots"] = parkingSpots;

            // Sorting parameters for the view
            ViewData["TypeSortParam"] = "type_asc"; // Default sorting
            ViewData["RegNrSortParam"] = "regNr_asc"; // Default sorting
            ViewData["ArrivalTimeSortParam"] = "at_asc"; // Default sorting

            return View();
        }






        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Include the related ApplicationUser, VehicleType, and ParkingSpot entities
            var parkedVehicle = await _context.ParkedVehicle
                .Include(v => v.ParkingSpot)         // Include the ParkingSpot related to the ParkedVehicle
                .Include(v => v.User)                 // Include the ApplicationUser related to the ParkedVehicle
                .Include(v => v.VehicleType)          // Include the VehicleType related to the ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Members
        [Authorize(Roles = "Member")]
        [HttpGet]
        [Route("ParkedVehicles/Members")]
        public async Task<IActionResult> Members()
        {
            // Load users along with their vehicles (if any)
            var users = await _context.Users
                .Include(u => u.Vehicles)  // Eager load the Vehicles collection for each user
                .ToListAsync();

            // Set the vehicle count for each user and total vehicle count
            var model = new MemberDisplayViewModel
            {
                Users = users,
                VehicleCount = users.Sum(u => u.Vehicles.Count()) // Sum of vehicle counts for all users
            };

            return View(model);
        }



        // GET: ParkedVehicles/Members/{id}
        [Authorize(Roles = "Member")]
        [HttpGet]
        [Route("ParkedVehicles/Members/{id}")]
        public async Task<IActionResult> Members(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .Include(u => u.Vehicles) // Eager load Vehicles
                .ThenInclude(u => u.VehicleType)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new MemberDetailsViewModel
            {
                User = user,
                Vehicles = user.Vehicles
            };

            return View("MembersDetails", model); // Use a detailed view for individual users
        }


        // GET: ParkedVehicles/CheckIn
        [Authorize]
        public async Task<IActionResult> CheckIn()
        {
            var viewModel = new ParkedVehicleCheckInViewModel();
            ViewData["VehicleTypes"] = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name");
            ViewData["ParkingSpots"] = new SelectList(await _context.ParkingSpot.OrderBy(s => s.SpotNumber).Where(s => !s.IsOccupied).ToListAsync(), "Id", "SpotNumber");

            return View(viewModel);
        }


        // POST: ParkedVehicles/CheckIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CheckIn(ParkedVehicleCheckInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Fetch the user based on the logged-in user's ID
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                }
                else
                {
                    // Validate age based on Personnummer (format: YYMMDD-XXXX)
                    if (user.Personnummer.Length >= 6)
                    {
                        string datePart = user.Personnummer.Substring(0, 6);
                        if (DateTime.TryParseExact(datePart, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
                        {
                            int age = DateTime.Now.Year - birthDate.Year;
                            if (birthDate > DateTime.Now.AddYears(-age)) age--; // Adjust if birthday hasn't occurred this year

                            if (age < 18)
                            {
                                ModelState.AddModelError("Personnummer", "You must be at least 18 years old to park a vehicle.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Personnummer", "Invalid Personnummer format.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Personnummer", "Invalid Personnummer format.");
                    }
                }

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

                        // get user id
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        var parkedVehicle = new ParkedVehicle
                        {
                            VehicleTypesId = viewModel.VehicleTypesId,
                            RegistrationNumber = viewModel.RegistrationNumber,
                            Color = viewModel.Color,
                            Make = viewModel.Make,
                            Model = viewModel.Model,
                            NumberOfWheels = viewModel.NumberOfWheels,
                            ArrivalTime = DateTime.Now,
                            ParkingSpotId = viewModel.ParkingSpotId,
                            ApplicationUserId = userId!
                        };

                        parkingSpot.ParkedVehicle = parkedVehicle;

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
        [Authorize]
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

            // Get the current logged-in user's ID and compare to vehicle's owner
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var isAdmin = User.IsInRole("Admin");

            // If the vehicle's owner is not matched with current ID and current user is not admin
            // then do not allow edit
            if (parkedVehicle.ApplicationUserId != currentUserId && !isAdmin)
            {
                return Forbid();
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
        [Authorize]
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

                    // Get the current logged-in user's ID and compare to vehicle's owner
                    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                    var isAdmin = User.IsInRole("Admin"); 

                    // If the vehicle's owner is not matched with current ID and current user is not admin
                    // then do not allow edit
                    if (existingVehicle.ApplicationUserId != currentUserId && !isAdmin)
                    {
                        return Forbid(); // Prevent unauthorized edits
                    }

                    // Check if the registration number already exists in the database (excluding the current vehicle)
                    var registrationExists = await _context.ParkedVehicle
                        .AnyAsync(v => v.RegistrationNumber == viewModel.RegistrationNumber && v.Id != id);

                    if (registrationExists)
                    {
                        // Add a model error if the registration number is already taken
                        ModelState.AddModelError("RegistrationNumber", "The registration number is already in use.");
                        viewModel.VehicleTypes = new SelectList(await _context.VehicleTypes.ToListAsync(), "Id", "Name", viewModel.VehicleTypesId);
                        viewModel.ParkingSpots = new SelectList(await _context.ParkingSpot.Where(s => !s.IsOccupied || s.Id == viewModel.ParkingSpotId).ToListAsync(), "Id", "SpotNumber", viewModel.ParkingSpotId);
                        return View(viewModel);
                    }

                    // Handle changes to ParkingSpotId
                    if (existingVehicle.ParkingSpotId != viewModel.ParkingSpotId)
                    {
                        // Assign new parking spot and mark it as occupied
                        if (viewModel.ParkingSpotId.HasValue)
                        {
                            var newParkingSpot = await _context.ParkingSpot.FindAsync(viewModel.ParkingSpotId);
                            if (newParkingSpot != null && !newParkingSpot.IsOccupied)
                            {
                                // If there was a previous parking spot, mark it as not occupied
                                if (existingVehicle.ParkingSpot != null)
                                {
                                    existingVehicle.ParkingSpot.IsOccupied = false;
                                    existingVehicle.ParkingSpot.ParkedVehicle = null;
                                    _context.Update(existingVehicle.ParkingSpot);
                                }
                                newParkingSpot.IsOccupied = true;
                                _context.Update(newParkingSpot);
                                existingVehicle.ParkingSpot = newParkingSpot;
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
