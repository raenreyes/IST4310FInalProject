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
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
        [HttpPost]
        public async Task<IActionResult> AddProject(Project model)
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (ModelState.IsValid)
            {

                _context.Projects.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Project));
            }

            return View(model);
        }
        public async Task<IActionResult> Project()
        {
            await InitializeUserDataAsync();
            var studentInfo = await _context.StudentInfos
                                            .Include(s => s.Projects)
                                            .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);

            if (studentInfo == null)
            {
                return NotFound();
            }

            
            return View(studentInfo.Projects);
        }
        public async Task<IActionResult> Education()
        {
            await InitializeUserDataAsync();
            var studentInfo = await _context.StudentInfos
                                            .Include(s => s.Educations)
                                            .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);

            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo.Educations);
        }
        public async Task<IActionResult> AddEducation()
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (_studentInfoExists != null)
            {
                var model = new Education { StudentInfoId = _studentInfoExists.Id };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddEducation(Education model)
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (ModelState.IsValid)
            {

                _context.Educations.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Education));
            }

            return View(model);
        }
        public async Task<IActionResult> Work()
        {
            await InitializeUserDataAsync();
            var studentInfo = await _context.StudentInfos
                                            .Include(s => s.WorkExperiences)
                                            .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);

            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo.WorkExperiences);
        }
        public async Task<IActionResult> AddWork()
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (_studentInfoExists != null)
            {
                var model = new WorkExperience { StudentInfoId = _studentInfoExists.Id };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddWork(WorkExperience model)
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (ModelState.IsValid)
            {

                _context.WorkExperiences.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Work));
            }

            return View(model);
        }
        public async Task<IActionResult> Skill()
        {
            await InitializeUserDataAsync();
            var studentInfo = await _context.StudentInfos
                                            .Include(s => s.Skills)
                                            .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);

            if (studentInfo == null)
            {
                return NotFound();
            }


            return View(studentInfo.Skills);
        }
        public async Task<IActionResult> AddSkill()
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (_studentInfoExists != null)
            {
                var model = new Skill { StudentInfoId = _studentInfoExists.Id };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddSkill(Skill model)
        {
            await InitializeUserDataAsync(); // Initialize user and student info once

            if (ModelState.IsValid)
            {

                _context.Skills.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Skill));
            }

            return View(model);
        }
        [HttpDelete]
        [Route("api/deleteproject/{id}")]
        public IActionResult DeleteProject(int? id)
        {
            var productToBeDeleted = _context.Projects.FirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _context.Projects.Remove(productToBeDeleted);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        [HttpDelete]
        [Route("api/deleteeducation/{id}")]
        public IActionResult DeleteEducation(int? id)
        {
            var productToBeDeleted = _context.Educations.FirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _context.Educations.Remove(productToBeDeleted);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        [HttpDelete]
        [Route("api/deletework/{id}")]
        public IActionResult DeleteWork(int? id)
        {
            var productToBeDeleted = _context.WorkExperiences.FirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _context.WorkExperiences.Remove(productToBeDeleted);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        
        public async Task<IActionResult> Resume()
        {
            await InitializeUserDataAsync();

            // Include WorkExperiences, Educations, and Projects alongside StudentInfo
            var studentInfo = await _context.StudentInfos
                .Include(s => s.WorkExperiences)
                .Include(s => s.Educations)
                .Include(s => s.Projects)
                .Include(s => s.Skills)
                .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);

            if (studentInfo == null)
            {
                return NotFound();
            }

            // Pass the complete studentInfo to the view, which includes WorkExperiences, Educations, and Projects
            return View(studentInfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
