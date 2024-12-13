using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Data;
using StudentPortal.web.Models.Entities;
using StudentPortal.web.Models;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Registration model)
    {
        if (ModelState.IsValid)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registration successful! You can now log in.";

            // Optionally, sign in the user after registration by saving username to session
            HttpContext.Session.SetString("Username", user.Username);
            var username = HttpContext.Session.GetString("Username");
            return RedirectToAction("Login", "Account");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(User model)
    {
        if (ModelState.IsValid)
        {
            // Find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            // Check if the user exists and if the password matches
            if (user != null && user.Password == model.Password) // Note: Avoid plain-text password checks in production
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Student"); // Redirect to the home page after successful login
            }

            // If the user does not exist or the password does not match
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
        }

        // If we got this far, something failed; redisplay the form
        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Find the user by their username or email (you might need to adjust this based on your User entity)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user != null)
            {
                // Check if the new password and confirm password match
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return View(model);
                }

                // Update the user's password (store it hashed in production)
                user.Password = model.NewPassword; // Ensure to hash the password in production
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            // Optionally, add a message if the username/email does not exist
            ModelState.AddModelError(string.Empty, "User not found.");
        }

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public  async Task<IActionResult> Logout()
    {
        // Clear the session on logout
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Optional: Add logging to ensure this code is running
        Console.WriteLine("User logged out successfully.");

        // Redirect to the login page
        return RedirectToAction("Login", "Account");
    }
}
