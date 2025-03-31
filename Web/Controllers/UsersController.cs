using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public string _name = "";
        public string _role = "";

        public UsersController(AppDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            UserVM uservm = new UserVM();
            return View(uservm);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Password2,Email,Phone,Role")] UserVM uservm)
        {
            bool userExist = false;
            userExist = _context.Users.Any(r => r.Name.ToLower() == uservm.Name.ToLower());
            if (userExist)
            {
                TempData["error"] = "Name already exists.";
                return View(uservm);
            }
            userExist = _context.Users.Any(r => r.Email.ToLower() == uservm.Email.ToLower());
            if (userExist)
            {
                TempData["error"] = "Email already exists.";
                return View(uservm);
            }
            if (uservm.Password != uservm.Password2)
            {
                TempData["error"] = "Passwords entered are not the same!";
                return View(uservm);
            }

            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Name = uservm.Name,
                    Email = uservm.Email,
                    Password = Encrypass(uservm.Password),
                    Phone = uservm.Phone,
                    Role = uservm.Role
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(uservm);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            User usr = new User {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                Role = user.Role
            };
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Email,Phone,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            // ----- check for same user
            bool userExist = false;
            userExist = _context.Users.Any(r => r.Id != id && r.Name.ToLower() == user.Name.ToLower());
            if (userExist)
            {
                TempData["error"] = "Name already exists.";
                return View(user);
            }
            userExist = _context.Users.Any(r => r.Id != id && r.Email.ToLower() == user.Email.ToLower());
            if (userExist)
            {
                TempData["error"] = "Email already exists.";
                return View(user);
            }

            User usr = new User();
            usr.Id = id;
            usr.Name = user.Name;
            usr.Email = user.Email;
            usr.Phone = user.Phone;
            usr.Role = user.Role;
            usr.Password = user.Password;

            _context.Update(usr);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);

        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? txtDelete)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (txtDelete != "delete")
            {
                return View(user);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }


        // ----- Logging -----
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!_context.Users.Any())
            {
                User adUser = new User
                {
                    Name = "Admin",
                    Email = "admin@email.com",
                    Password = Encrypass("@dmin"),
                    Phone = "416 111 2222",
                    Role = Role.Admin
                };
                _context.Users.Add(adUser);
                _context.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(n => n.Email.Equals(loginModel.Email) && n.Password == Encrypass(loginModel.Password));

                if (user != null)
                {
                    loginModel.ReturnUrl = (loginModel?.ReturnUrl ?? "/");
                    loginModel.Role = user.Role;

                    HttpContext.Session.SetInt32("_id", user.Id);
                    HttpContext.Session.SetString("_name", user.Name);
                    HttpContext.Session.SetString("_email", user.Email);
                    HttpContext.Session.SetString("_role", user.Role.ToString());

                    return Redirect(loginModel.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Invalid Name or Password");
            return View(loginModel);
        }


        // Log Out
        public RedirectResult Logout(string returnUrl = "/")
        {
            HttpContext.Session.SetInt32("_id", 0);
            HttpContext.Session.SetString("_name", "");
            HttpContext.Session.SetString("_email", "");
            HttpContext.Session.SetString("_role", "");

            return Redirect(returnUrl);
        }

        // Reset Password
        public async Task<IActionResult> ResetPassword(int id)
        {
            User user = _context.Users.FirstOrDefault(r => r.Id == id);
            if (user == null)
            {
                TempData["message"] = "User Not Found!";
                return View("_Error");
            }
            ResetPasswordVM resetPasswordVM= new ResetPasswordVM();
            resetPasswordVM.Id = id;
            resetPasswordVM.Name = user.Name;
            resetPasswordVM.Email = user.Email;
            return View(resetPasswordVM);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM rp)
        {
            if (ModelState.IsValid)
            {
                if (rp.Password1 == rp.Password2)
                {
                    User user = _context.Users.FirstOrDefault(r => r.Id == rp.Id);
                    if (user == null)
                    {
                        TempData["message"] = "User Not Found!";
                        return View("_Error");
                    }
                    user.Password = Encrypass(rp.Password1);
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    TempData["success"] = "Password has been changed successfully.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = "The entered passwords are not the same.";
                    return View(rp);
                }
            }
            ModelState.AddModelError("", "Invalid Name or Password");
            return View(rp);
        }


         // ------ public functions -------
        public string Encrypass(string password)
        {
            byte[] encPass_byte = new byte[password.Length];
            encPass_byte = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encPass_byte);
        }
    }


}

