using IST4310FInalProject.Data;
using IST4310FInalProject.Models;
using IST4310FInalProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using IST4310FInalProject.Models.ViewModel;

namespace IST4310FInalProject.Controllers
{
    [Authorize]
    public class AIController : Controller
    {
        private readonly ILogger<AIController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatService;
        private readonly IChatHistoryService _chatHistory;

        private string _userId;  // Class-level field for userId
        private StudentInfo _studentInfoExists;
        public AIController(Kernel kernel, ILogger<AIController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager,
             IChatCompletionService chatService, IChatHistoryService chatHistory)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _kernel = kernel;
            _chatService = chatService;
            _chatHistory = chatHistory;
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


        public IActionResult AnalyzeAI()
        {
            var viewModel = new DreamJobVM
            {
                Job = new DreamJob(),
                Response = string.Empty
            };
            return View(viewModel);
        }
        //[HttpPost]
        //public async Task<IActionResult> AnalyzeAI(DreamJob model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Job))
        //    {
        //        return BadRequest("Question cannot be empty.");
        //    }

        //    _chatHistory.AddMessage(model.Job, true);



        //    var responseBuilder = new System.Text.StringBuilder();
        //    await foreach (var response in _chatService.GetStreamingChatMessageContentsAsync(_chatHistory.GetChatHistory()))
        //    {
        //        responseBuilder.Append(response);
        //    }


        //    var viewModel = new DreamJobVM
        //    {
        //        Job = model,
        //        Response = responseBuilder.ToString()
        //    };


        //    return View(viewModel);
        //}
        [HttpPost]
        public async Task<IActionResult> AnalyzeAI(DreamJob model)
        {
            if (string.IsNullOrWhiteSpace(model.Job))
            {
                return BadRequest("The job description cannot be empty.");
            }

            // Initialize user data (userId and StudentInfo)
            await InitializeUserDataAsync();

            if (_studentInfoExists == null)
            {
                return NotFound("Resume information not found for the current user.");
            }

            // Step 1: Gather data about the user (resume) and their dream job
            var studentInfo = await _context.StudentInfos
               .Include(s => s.WorkExperiences)
               .Include(s => s.Educations)
               .Include(s => s.Projects)
               .Include(s => s.Skills)
               .FirstOrDefaultAsync(s => s.Id == _studentInfoExists.Id);
            var resumeSummary = GenerateResumeSummary(studentInfo);
            //ViewBag.ResumeSummary = resumeSummary;

            // Step 2: Ask Semantic Kernel to review the resume and provide feedback
            var prompt = @$"
    A user is applying for their dream job as: {model.Job}.
    Below is their resume information:
    {resumeSummary}

    Please provide a comprehensive analysis of their resume, focusing on the following:

    1. **Skills**: 
       - Identify how their current skills align with the requirements and expectations of the dream job.
       - Suggest additional skills or certifications they can acquire to strengthen their application and improve their suitability for the role.

    2. **Projects**:
       - Evaluate the relevance and impact of their listed projects in the context of the dream job's responsibilities and requirements.
       - Highlight specific aspects of their projects that showcase their readiness for the role.
       - Provide suggestions on how they can present or expand upon their project experience to better align with the job.

    3. **Work Experience**:
       - Analyze the applicability of their work experience to the dream job, including transferable skills and relevant accomplishments.
       - Identify any potential gaps or areas for improvement in their experience and recommend actionable steps to address them.

    4. **Overall Recommendations**:
       - Offer tailored advice on how to refine their resume and tailor their application to maximize their chances of securing the dream job.
       - Suggest strategies for enhancing their professional profile, including networking tips, interview preparation, or additional training.

    Provide a detailed, constructive, and actionable analysis to help the user understand how their background positions them for the dream job and what steps they can take to increase their chances of success.
    ";




            // Save user question in chat history
            _chatHistory.AddMessage(prompt, true);

            string response;
            var responseBuilder = new System.Text.StringBuilder();
            await foreach (var responses in _chatService.GetStreamingChatMessageContentsAsync(_chatHistory.GetChatHistory()))
            {
                responseBuilder.Append(responses);
            }

            // Prepare ViewModel for the view
            var viewModel = new DreamJobVM
            {
                Job = model,
                Response = responseBuilder.ToString(),
            };

            return View(viewModel);
        }
        private string GenerateResumeSummary(StudentInfo studentInfo)
        {
            var summary = new System.Text.StringBuilder();

            summary.AppendLine($"Name: {studentInfo.FirstName} {studentInfo.LastName}");
            summary.AppendLine($"Major: {studentInfo.Major}");
            summary.AppendLine($"Degree Type: {studentInfo.DegreeType}");
            summary.AppendLine($"Phone Number: {studentInfo.PhoneNumber}");
            summary.AppendLine($"City: {studentInfo.City}");
            summary.AppendLine("\nEducation:");
            foreach (var education in studentInfo.Educations)
            {
                summary.AppendLine($"- {education.Degree} in {education.Major} from {education.SchoolName}, {education.EndDate}");
            }

            summary.AppendLine("\nWork Experience:");
            foreach (var experience in studentInfo.WorkExperiences)
            {
                summary.AppendLine($"- {experience.JobTitle} at {experience.CompanyName} ({experience.StartDate} to {experience.EndDate})");
                summary.AppendLine($"  Responsibilities: {experience.Responsibilities}");
            }

            summary.AppendLine("\nSkills:");
            foreach (var skill in studentInfo.Skills)
            {
                summary.AppendLine($"- {skill.Heading} ({skill.ToolSkills})");
            }

            summary.AppendLine("\nProjects:");
            foreach (var project in studentInfo.Projects)
            {
                summary.AppendLine($"- {project.Title}: {project.Description} :Technologies Used {project.TechnologiesUsed}");
            }

            return summary.ToString();
        }

    }
}
