using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTest.Helper;
using ProjectTest.Models;
using ProjectTest.Repository;

namespace ProjectTest.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger<EmployeeController> _logger;
        private ApplicationDbContext _context;

        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            var NewEmployee = new Employee();

            //List<Positions> positions =  _context.positions.ToList();

            NewEmployee.Position = _context.positions.ToList().Select(p => new SelectListItem
            {
                Text = p.PositionName,
                Value = p.PositionId
            }).ToList();

            NewEmployee.LanguageCheckboxOptions = _context.CheckboxOptions.ToList();

            NewEmployee.GenderRadioButtonBtns = _context.RadioButtonBtns.ToList();

            return View(NewEmployee);
        }



    }
}
