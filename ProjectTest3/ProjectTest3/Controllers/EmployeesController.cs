using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTest3.Dto;
using ProjectTest3.Models;
using ProjectTest3.Repositories.Interface;


namespace ProjectTest3.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeRepository employeeRepository, IGenderRepository genderRepository, IPositionRepository positionRepository, ILanguageRepository languageRepository, ILogger<EmployeesController> logger)
        {
            _employeeRepository = employeeRepository;
            _genderRepository = genderRepository;
            _positionRepository = positionRepository;
            _languageRepository = languageRepository;
            _logger = logger;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            var employeeDto = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                GenderId = e.GenderId,
                PositionId = e.PositionId,
                GenderName = e.Gender.Name,
                PositionName = e.Position.Name,
                LanguageNames = e.EmployeeLanguages.Select(el => el.Language.Name).ToList()
            }).ToList();

            _logger.LogInformation($"Employee list retrieved successfully => {employeeDto.ToString()}");


            return View(employeeDto);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeWithDetailsAsync(id); 

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                GenderId = employee.GenderId,
                PositionId = employee.PositionId,
                GenderName = employee.Gender.Name,
                PositionName = employee.Position.Name,
                LanguageNames = employee.EmployeeLanguages.Select(el => el.Language.Name).ToList()
            };

            _logger.LogInformation($"Employee details retrieved successfully => {employeeDto.ToString()}");

            return View(employeeDto);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var newEmployeeDto = new EmployeeDto();
            await PopulateDropdownsAsync(newEmployeeDto);
            return View(newEmployeeDto);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    GenderId = employeeDto.GenderId,
                    PositionId = employeeDto.PositionId
                };

                await _employeeRepository.AddEmployeeAsync(employee);
                await _employeeRepository.SaveAsync();

                // Now that we have the employee ID, we can add the language associations
                if (employeeDto.SelectedLanguageIds != null && employeeDto.SelectedLanguageIds.Any())
                {
                    await _employeeRepository.UpdateEmployeeLanguagesAsync(employee.Id, employeeDto.SelectedLanguageIds);
                    await _employeeRepository.SaveAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownsAsync(employeeDto);
            return View(employeeDto);
        }

        // GET: Employees/Edit/{5}
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeWithDetailsAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                GenderId = employee.GenderId,
                PositionId = employee.PositionId,
                SelectedLanguageIds = employee.EmployeeLanguages.Select(el => el.LanguageId).ToList()
            };

            await PopulateDropdownsAsync(employeeDto);
            return View(employeeDto);
        }

        // POST: Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    // Update employee properties
                    employee.Name = employeeDto.Name;
                    employee.Email = employeeDto.Email;
                    employee.PhoneNumber = employeeDto.PhoneNumber;
                    employee.GenderId = employeeDto.GenderId;
                    employee.PositionId = employeeDto.PositionId;

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                    // Update language associations
                    await _employeeRepository.UpdateEmployeeLanguagesAsync(id, employeeDto.SelectedLanguageIds);

                    await _employeeRepository.SaveAsync();
                    _logger.LogInformation($"Employee updated successfully => {employee.ToString()}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await _employeeRepository.EmployeeExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            await PopulateDropdownsAsync(employeeDto);
            return View(employeeDto);
        }

        // GET: Employees/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeWithDetailsAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                GenderName = employee.Gender.Name,
                PositionName = employee.Position.Name,
                LanguageNames = employee.EmployeeLanguages.Select(el => el.Language.Name).ToList()
            };

            return View(employeeDto);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Deleted {id} Employee");
            await _employeeRepository.DeleteEmployeeAsync(id);
            await _employeeRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to populate dropdown lists
        private async Task PopulateDropdownsAsync(EmployeeDto employeeDto)
        {
            var genders = await _genderRepository.GetAllGendersAsync();
            var positions = await _positionRepository.GetAllPositionsAsync();
            var languages = await _languageRepository.GetAllLanguagesAsync();

            employeeDto.GenderOptions = new SelectList(genders, "Id", "Name", employeeDto.GenderId);
            employeeDto.PositionOptions = new SelectList(positions, "Id", "Name", employeeDto.PositionId);
            employeeDto.LanguageOptions = new MultiSelectList(languages, "Id", "Name", employeeDto.SelectedLanguageIds);
        }
    }
}