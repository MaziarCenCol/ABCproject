using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Data;
using System.Text.Json;
using Web.Models;
using System.Text;
using Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            var relativePath = "/plots/plot.html";
            var wwwrootPath = _env.WebRootPath; // Inject IWebHostEnvironment
            var expectedFilePath = Path.Combine(wwwrootPath, relativePath.TrimStart('/'));

            // Check if file exists and log the result
            bool fileExists = System.IO.File.Exists(expectedFilePath);

            ViewBag.PlotPath = relativePath;
            ViewBag.AbsolutePlotPath = expectedFilePath;
            ViewBag.FileExists = fileExists;
            return View();
        }


        // ---------- Rescheduling ----------
        public IActionResult Reschedule()
        {
            MachineJobTask machineJobTask = GetMJT();

            // Initialize session variables if they don't exist
            if (HttpContext.Session.GetString("MachineDowns") == null)
            {
                HttpContext.Session.SetString("MachineDowns", JsonSerializer.Serialize(new List<MachineDown>()));
            }

            if (HttpContext.Session.GetString("MaterialDelays") == null)
            {
                HttpContext.Session.SetString("MaterialDelays", JsonSerializer.Serialize(new List<MaterialDelay>()));
            }

            // Pass the current collections to the view
            ViewBag.MachineDowns = JsonSerializer.Deserialize<List<MachineDown>>(HttpContext.Session.GetString("MachineDowns"));
            ViewBag.MaterialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(HttpContext.Session.GetString("MaterialDelays"));

            // ViewBag.MachineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
            //     HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));
            // ViewBag.MaterialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
            //     HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

            return View(machineJobTask);
        }

        [HttpPost]
        public IActionResult Reschedule_down(string machine, DateTime start_date, DateTime finish_date, string description)
        {
            // Get current machine downs from session
            var machineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
                HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));

            // Add new machine down to the list
            machineDowns.Add(new MachineDown
            {
                MachineId = int.Parse(machine),
                DownStartDateTime = start_date,
                DownEndDateTime = finish_date,
                MachineName = _context.Machines.FirstOrDefault(m => m.Id == int.Parse(machine))?.Name,
                Description = description
            });

            // Save back to session
            HttpContext.Session.SetString("MachineDowns", JsonSerializer.Serialize(machineDowns));

            // Prepare data for view
            MachineJobTask machineJobTask = GetMJT();
            ViewBag.MachineDowns = machineDowns;
            ViewBag.MaterialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
                HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

            ViewBag.Message = "Machine down added to queue. Submit all changes when ready.";
            return View("Reschedule", machineJobTask);
        }

        [HttpPost]
        public IActionResult Reschedule_delay(string job, string task, DateTime ready_datetime, string description)
        {
            // Get current material delays from session
            var materialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
                HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

            // Add new material delay to the list
            materialDelays.Add(new MaterialDelay
            {
                JobNo = int.Parse(job),
                TaskSeq = int.Parse(task),
                MaterialReadyDateTime = ready_datetime,
                JobName = _context.Jobs.FirstOrDefault(j => j.JobNo == int.Parse(job))?.Name,
                TaskDescription = _context.Tasks.FirstOrDefault(t => t.TaskSeq == int.Parse(task))?.Description,
                Description = description
            });

            // Save back to session
            HttpContext.Session.SetString("MaterialDelays", JsonSerializer.Serialize(materialDelays));

            // Prepare data for view
            MachineJobTask machineJobTask = GetMJT();
            ViewBag.MachineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
                HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));
            ViewBag.MaterialDelays = materialDelays;

            ViewBag.Message = "Material delay added to queue. Submit all changes when ready.";
            return View("Reschedule", machineJobTask);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReschedulingChanges(string selectedMachineDownIds, string selectedMaterialDelayIds)
        {
            try
            {
                // Get saved data from session
                var allMachineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
                    HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));

                var allMaterialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
                    HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

                // If no selections, use all items
                List<int> machineDownIds = new List<int>();
                List<int> materialDelayIds = new List<int>();

                if (string.IsNullOrEmpty(selectedMachineDownIds) && allMachineDowns.Count > 0)
                {
                    // If nothing explicitly selected, select all
                    machineDownIds = Enumerable.Range(0, allMachineDowns.Count).ToList();
                }
                else if (!string.IsNullOrEmpty(selectedMachineDownIds))
                {
                    machineDownIds = selectedMachineDownIds.Split(',').Select(int.Parse).ToList();
                }

                if (string.IsNullOrEmpty(selectedMaterialDelayIds) && allMaterialDelays.Count > 0)
                {
                    // If nothing explicitly selected, select all
                    materialDelayIds = Enumerable.Range(0, allMaterialDelays.Count).ToList();
                }
                else if (!string.IsNullOrEmpty(selectedMaterialDelayIds))
                {
                    materialDelayIds = selectedMaterialDelayIds.Split(',').Select(int.Parse).ToList();
                }

                // Debug information
                _logger.LogInformation($"Machine Down IDs: {string.Join(", ", machineDownIds)}");
                _logger.LogInformation($"Material Delay IDs: {string.Join(", ", materialDelayIds)}");

                // Filter to get only selected items
                var selectedMachineDowns = allMachineDowns
                    .Where((m, index) => machineDownIds.Contains(index))
                    .Select(m => new
                    {
                        machine_id = m.MachineId,
                        down_start_datetime = m.DownStartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        down_end_datetime = m.DownEndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        description = m.Description
                    }).ToList();

                var selectedMaterialDelays = allMaterialDelays
                    .Where((m, index) => materialDelayIds.Contains(index))
                    .Select(m => new
                    {
                        job_no = m.JobNo,
                        task_seq = m.TaskSeq,
                        material_ready_datetime = m.MaterialReadyDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        description = m.Description
                    }).ToList();

                // Debug logging
                _logger.LogInformation($"Selected Machine Downs: {JsonSerializer.Serialize(selectedMachineDowns)}");
                _logger.LogInformation($"Selected Material Delays: {JsonSerializer.Serialize(selectedMaterialDelays)}");

                // Create the payload
                var payload = new
                {
                    machine_downs = selectedMachineDowns,
                    material_delays = selectedMaterialDelays
                };

                // Call the API
                var apiUrl = "http://127.0.0.1:8000/api/add-rescheduling-changes";

                using (var httpClient = new HttpClient())
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    };

                    var json = JsonSerializer.Serialize(payload, options);
                    _logger.LogInformation($"Sending payload: {json}");

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await httpClient.PostAsync(apiUrl, content);
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"API Response: {responseContent}");

                        if (response.IsSuccessStatusCode)
                        {
                            // Clear the session data after successful submission
                            HttpContext.Session.SetString("MachineDowns", JsonSerializer.Serialize(new List<MachineDown>()));
                            HttpContext.Session.SetString("MaterialDelays", JsonSerializer.Serialize(new List<MaterialDelay>()));

                            ViewBag.Message = "Rescheduling changes submitted successfully!";
                        }
                        else
                        {
                            ViewBag.Message = $"API Error: {response.StatusCode} - {responseContent}";
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error calling API: {ex.Message}");
                        ViewBag.Message = $"Error calling API: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in SubmitReschedulingChanges: {ex.Message}");
                ViewBag.Message = $"Error processing data: {ex.Message}";
            }

            MachineJobTask machineJobTask = GetMJT();
            ViewBag.MachineDowns = new List<MachineDown>();
            ViewBag.MaterialDelays = new List<MaterialDelay>();

            return View("Reschedule", machineJobTask);
        }

        [HttpPost]
        public IActionResult RemoveMachineDown(int index)
        {
            var machineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
                HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));

            if (index >= 0 && index < machineDowns.Count)
            {
                machineDowns.RemoveAt(index);
                HttpContext.Session.SetString("MachineDowns", JsonSerializer.Serialize(machineDowns));
                ViewBag.Message = "Machine down removed from queue.";
            }

            MachineJobTask machineJobTask = GetMJT();
            ViewBag.MachineDowns = machineDowns;
            ViewBag.MaterialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
                HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

            return View("Reschedule", machineJobTask);
        }

        [HttpPost]
        public IActionResult RemoveMaterialDelay(int index)
        {
            var materialDelays = JsonSerializer.Deserialize<List<MaterialDelay>>(
                HttpContext.Session.GetString("MaterialDelays") ?? JsonSerializer.Serialize(new List<MaterialDelay>()));

            if (index >= 0 && index < materialDelays.Count)
            {
                materialDelays.RemoveAt(index);
                HttpContext.Session.SetString("MaterialDelays", JsonSerializer.Serialize(materialDelays));
                ViewBag.Message = "Material delay removed from queue.";
            }

            MachineJobTask machineJobTask = GetMJT();
            ViewBag.MachineDowns = JsonSerializer.Deserialize<List<MachineDown>>(
                HttpContext.Session.GetString("MachineDowns") ?? JsonSerializer.Serialize(new List<MachineDown>()));
            ViewBag.MaterialDelays = materialDelays;

            return View("Reschedule", machineJobTask);
        }

        public MachineJobTask GetMJT()
        {
            MachineJobTask machineJobTask = new MachineJobTask();
            machineJobTask.Machines = _context.Machines
                .ToList();
            machineJobTask.Tasks = _context.Tasks
                .Where(m => _context.TaskSchedules.Any(ts => ts.TaskId == m.Id))
                .ToList();
            machineJobTask.Jobs = _context.Jobs
                .Where(j => _context.Tasks.Any(t => t.JobId == j.Id && _context.TaskSchedules.Any(ts => ts.TaskId == t.Id)))
                .OrderBy(j => j.JobNo)
                .ToList();


            return machineJobTask;
        }

        /* AJAX */

        [HttpGet]
        public JsonResult GetTasksByJob(int jobNo)
        {
            // Check if we're getting the right parameter
            _logger.LogInformation($"GetTasksByJob called with jobNo={jobNo}");

            var job = _context.Jobs.FirstOrDefault(j => j.JobNo == jobNo);

            if (job == null)
            {
                _logger.LogError($"Job with jobNo={jobNo} not found");
                return Json(new List<object>());
            }

            // Examine what's in the Tasks table for this job
            var tasks = _context.Tasks
                .Where(t => t.JobId == job.Id)
                .Include(t => t.Operation)
                .OrderBy(t => t.TaskSeq)
                .ToList();

            _logger.LogInformation($"Found {tasks.Count} tasks for job {jobNo}");

            // Check all tasks to help debug
            var allTasks = _context.Tasks.ToList();
            _logger.LogInformation($"Total tasks in database: {allTasks.Count}");

            // Return simplified task objects for the dropdown
            return Json(tasks.Select(t => new
            {
                t.TaskSeq,
                t.Description,
                OperationCode = t.Operation?.OperationCode,
                OperationDescription = t.Operation?.OperationDescription
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReschedule(DateTime rescheduleStartDate)
        {
            try
            {
                // Format the datetime to ISO format
                var formattedDateTime = rescheduleStartDate.ToString("yyyy-MM-ddTHH:mm:ssZ");

                // Create the payload
                var payload = new
                {
                    rescheduling_start_datetime = formattedDateTime
                };

                // Call the API
                var apiUrl = "http://127.0.0.1:8000/api/reschedule";

                using (var httpClient = new HttpClient())
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    };

                    var json = JsonSerializer.Serialize(payload, options);
                    _logger.LogInformation($"Sending reschedule payload: {json}");

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"API Response: {responseContent}");

                    // Return the API response directly
                    return Content(responseContent, "application/json");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateReschedule: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    // Add model classes for session storage
    public class MachineDown
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public DateTime DownStartDateTime { get; set; }
        public DateTime DownEndDateTime { get; set; }
        public string Description { get; set; }
    }

    public class MaterialDelay
    {
        public int JobNo { get; set; }
        public string JobName { get; set; }
        public int TaskSeq { get; set; }
        public string TaskDescription { get; set; }
        public DateTime MaterialReadyDateTime { get; set; }
        public string Description { get; set; }
    }
}
