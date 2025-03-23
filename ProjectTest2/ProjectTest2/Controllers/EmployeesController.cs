using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTest2.Data;
using ProjectTest2.Models;
using ProjectTest2.Models.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTest2.Models.Helper;

namespace ProjectTest2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ApplicationDbContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Gender)
                .ToListAsync();

            return View(employees);
        }

        // GET: Employees/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Gender)
                .Include(e => e.EmployeeLanguages)
                    .ThenInclude(el => el.Language)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View(PopulateFormOptions(new EmployeeViewModel()));
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        _logger.LogError($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
                return View(PopulateFormOptions(viewModel));
            }

            viewModel.Employee.Languages = viewModel.SelectedLanguages;
            viewModel.Employee.CreatedAt = DateTime.UtcNow;

            _context.Add(viewModel.Employee);
            await _context.SaveChangesAsync();

            await UpdateEmployeeLanguagesAsync(viewModel.Employee.Id, viewModel.SelectedLanguages);

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmployeeLanguages)
                    .ThenInclude(el => el.Language)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = PopulateFormOptions(new EmployeeViewModel { Employee = employee });
            viewModel.SelectedLanguages = employee.Languages;

            return View(viewModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel viewModel)
        {
            if (id != viewModel.Employee.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        _logger.LogError($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
                return View(PopulateFormOptions(viewModel));
            }

            try
            {
                viewModel.Employee.Languages = viewModel.SelectedLanguages;

                var existingEmployee = await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);

                viewModel.Employee.CreatedAt = existingEmployee.CreatedAt;
                viewModel.Employee.UpdatedAt = DateTime.UtcNow;

                _context.Update(viewModel.Employee);
                await _context.SaveChangesAsync();

                await UpdateEmployeeLanguagesAsync(viewModel.Employee.Id, viewModel.SelectedLanguages);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(viewModel.Employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            var employeeLanguages = await _context.EmployeeLanguages
                .Where(el => el.EmployeeId == id)
                .ToListAsync();

            _context.EmployeeLanguages.RemoveRange(employeeLanguages);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateEmployeeLanguagesAsync(int employeeId, List<string> selectedLanguages)
        {
            var existingRelationships = await _context.EmployeeLanguages
                .Where(el => el.EmployeeId == employeeId)
                .ToListAsync();

            _context.EmployeeLanguages.RemoveRange(existingRelationships);
            await _context.SaveChangesAsync();

            if (selectedLanguages != null && selectedLanguages.Any())
            {
                var languageIds = await _context.Languages
                    .Where(l => selectedLanguages.Contains(l.Name))
                    .Select(l => l.Id)
                    .ToListAsync();

                foreach (var languageId in languageIds)
                {
                    _context.EmployeeLanguages.Add(new EmployeeLanguage
                    {
                        EmployeeId = employeeId,
                        LanguageId = languageId
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        private EmployeeViewModel PopulateFormOptions(EmployeeViewModel viewModel)
        {
            viewModel.PositionOptions = _context.Positions
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            viewModel.AvailableLanguages = _context.Languages
                .Select(l => new SelectListItem
                {
                    Value = l.Name,
                    Text = l.Name
                })
                .ToList();

            viewModel.GenderOptions = _context.Genders
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToList();

            return viewModel;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}