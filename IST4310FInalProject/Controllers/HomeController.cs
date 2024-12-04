using System.Diagnostics;
using IST4310FInalProject.Data;
using IST4310FInalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IST4310FInalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private string _userId;  // Class-level field for userId
        private StudentInfo _studentInfoExists;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        private async Task InitializeUserDataAsync()
        {
            if (_userId == null)
            {
                _userId = _userManager.GetUserId(User);  // Get userId once
            }

            if (_studentInfoExists == null && _userId != null)
            {
                _studentInfoExists = await _context.StudentInfos.FirstOrDefaultAsync(x => x.UserId == _userId);  // Get studentInfo once
            }
        }
        public async Task<IActionResult> Index()
        {
            await InitializeUserDataAsync();

            if (_studentInfoExists != null)
            {
                return View(_studentInfoExists);
            }
            return View();
        }
       
        public async Task<IActionResult> UpsertStudentInfo()
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (_studentInfoExists != null)
            {
                return View(_studentInfoExists);
            }

            var model = new StudentInfo { UserId = _userId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertStudentInfo(StudentInfo model)
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (ModelState.IsValid)
            {
                if (_studentInfoExists != null)
                {
                    _studentInfoExists.FirstName = model.FirstName;
                    _context.StudentInfos.Update(_studentInfoExists);
                }
                else
                {
                    _context.StudentInfos.Add(model);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> AddProject()
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (_studentInfoExists != null)
            {
                var model = new Project { StudentInfoId = _studentInfoExists.Id };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
