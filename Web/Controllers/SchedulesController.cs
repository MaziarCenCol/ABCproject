using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Data;
using Web.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "schedule.json");


        public SchedulesController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ------------ Configuration ------------
        public IActionResult Configureation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Configureation(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    // Save or process the uploaded files here
                }
                ViewBag.Message = "Files uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select files to upload.";
            }

            return View();
        }


        // ------------ Configuration ------------
        public IActionResult Status()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Status(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    // Save or process the uploaded files here
                }
                ViewBag.Message = "Files uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select files to upload.";
            }

            return View();
        }

        // ------------ Create Schedule ------------
        public IActionResult CreateSchedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSchedule(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    // Save or process the uploaded files here
                }
                ViewBag.Message = "Files uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select files to upload.";
            }

            return View();
        }



        // ------------ Scheduling ------------
        public IActionResult Schedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Schedule(IFormFile file, bool Confirm)
        {
            if (!Confirm)
            {
                ViewBag.Message = "You must confirm the rescheduling.";
                return View();
            }

            if (file != null && file.Length > 0)
            {
                string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "schedule.json");

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var jsonData = reader.ReadToEnd();
                    System.IO.File.WriteAllText(jsonFilePath, jsonData); // Save JSON
                }

                // Run Python script to generate Plotly graph
                RunPythonScript();

                return RedirectToAction("DisplaySchedule"); // Redirect to new view
        }

            return View();
    }

        // Run Python Script to Generate Plot
        private string RunPythonScript()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "PythonScripts/plot.py",  // Run script from PythonScripts folder
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd().Trim(); // Read output (HTML file path)
            }
        }

        // Display Schedule Graph
        public IActionResult DisplaySchedule()
        {
            ViewBag.PlotPath = "/plots/plot.html"; // Pass plot path to view
            return View();
        }


        // ---------- Rescheduling ----------
        public IActionResult Reschedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reschedule(string Machine, string Job, int Hours, string Reason, bool Confirm)
        {
            if (!Confirm)
            {
                ViewBag.Message = "You must confirm the rescheduling.";
                return View();
            }

            // Handle the form submission (e.g., save to DB, process request)
            ViewBag.Message = "Rescheduling request submitted successfully!";
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


    }
}
