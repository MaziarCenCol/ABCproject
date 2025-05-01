using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Data;
using System.Text.Json;
using Web.Models;
using System.Text;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "schedule.json");
        private readonly IWebHostEnvironment _env;

        public SchedulesController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
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

        // API to save JSON data
        [HttpPost("/api/file/save_schedule_json")]
        public async Task<IActionResult> SaveScheduleJson([FromBody] object jsonData)
        {
            try
            {
                var filesPath = Path.Combine(_env.WebRootPath, "files");

                if (!Directory.Exists(filesPath))
                {
                    Directory.CreateDirectory(filesPath);
                }

                var filePath = Path.Combine(filesPath, "schedule.json");

                // Serialize and save
                await System.IO.File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true }));

                return Ok(new { message = "File saved successfully.", path = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Saving failed.", error = ex.Message });
            }
        }


        // ------------ Scheduling ------------
        public IActionResult Schedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Schedule(bool Confirm)
        {
            if (!Confirm)
            {
                ViewBag.Message = "You must confirm the rescheduling.";
                return View();
            }

            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "schedule.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                RunPythonScript(); // Generate Plotly graph based on existing file
                return RedirectToAction("DisplaySchedule");
            }
            else
            {
                ViewBag.Message = "schedule.json file not found in /wwwroot/files.";
                return View();
            }

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
            MachineJobTask machineJobTask = GetMJT();
            return View(machineJobTask);
        }

        [HttpPost]
        public async Task<IActionResult> Reschedule_down(string machine, DateTime start_date, DateTime finish_date, string description)
        {
            var apiUrl = "http://127.0.0.1:8000/api/add-rescheduling-changes";

            var payload = new
            {
                machine_downs = new[]
                {
            new
            {
                machine_id = int.Parse(machine),
                down_start_datetime = start_date.ToString("yyyy-MM-ddTHH:mm:ss"),
                down_end_datetime = finish_date.ToString("yyyy-MM-ddTHH:mm:ss"),
                name = _context.Machines.FirstOrDefault(m => m.Id == int.Parse(machine))?.Name,
                description = description
            }
        }
            };

            using (var httpClient = new HttpClient())
            {
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Rescheduling request submitted successfully!";
                    }
                    else
                    {
                        ViewBag.Message = $"API Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error calling API: {ex.Message}";
                }
            }

            MachineJobTask machineJobTask = GetMJT();
            return View("Reschedule", machineJobTask);
        }


        [HttpPost]
        public async Task<IActionResult> Reschedule_delay(string job, string task, DateTime ready_datetime, string description)
        {
            var apiUrl = "http://127.0.0.1:8000/api/add-rescheduling-changes";

            var payload = new
            {
                material_delays = new[]
                {
                    new
                    {
                job_no = int.Parse(job),
                task_seq = int.Parse(task),
                material_ready_datetime = ready_datetime.ToString("yyyy-MM-ddTHH:mm:ss"),
                description = description
            }
        }
            };

            using (var httpClient = new HttpClient())
            {
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Rescheduling request submitted successfully!";
                    }
                    else
                    {
                        ViewBag.Message = $"API Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error calling API: {ex.Message}";
                }
            }

            MachineJobTask machineJobTask = GetMJT();
            return View("Reschedule", machineJobTask);
        }


        public MachineJobTask GetMJT()
        {
            MachineJobTask machineJobTask = new MachineJobTask();
            machineJobTask.Machines = _context.Machines
                .Where(m => _context.TaskSchedules.Any(ts => ts.MachineId == m.Id))
                .ToList();
            machineJobTask.Tasks = _context.Tasks
                .Where(m => _context.TaskSchedules.Any(ts => ts.TaskId == m.Id))
                .ToList();
            machineJobTask.Jobs = _context.Jobs
                .Where(j => _context.Tasks.Any(t => t.JobId == j.Id && _context.TaskSchedules.Any(ts => ts.TaskId == t.Id)))
                .ToList();


            return machineJobTask;
        }

        /* AJAX
        [HttpGet]
        public JsonResult GetJobsByMachine(int machineId)
        {
            var jobs = _context.Jobs
                .Where(j => _context.Tasks.Any(t => t.JobId == j.Id && t.MachineId == machineId))
                .Select(j => new { j.Id, j.Name })
                .Distinct()
                .ToList();

            return Json(jobs);
        }

        [HttpGet]
        public JsonResult GetTasksByJob(int jobId)
        {
            var tasks = _context.Tasks
                .Where(t => t.JobId == jobId)
                .Select(t => new { t.Id, t.Name })
                .ToList();

            return Json(tasks);
        }
        */

    }
}
