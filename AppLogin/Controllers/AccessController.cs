using Microsoft.EntityFrameworkCore; // If you are using Entity Framework Core for database access
using AppLogin.Api; // Assuming you have a namespace for your API models or services
using AppLogin.ViewModels; // Assuming you have a namespace for your view models
using Microsoft.AspNetCore.Mvc;

/* To authentication */
using Microsoft.AspNetCore.Authentication; // For authentication-related functionality
using Microsoft.AspNetCore.Authentication.Cookies; // For cookie authentication
using System.Security.Claims;

namespace AppLogin.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDBContext _appDBContext;
        public AccessController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();


        /** 
         * This method handles user loggin by validating the provided information on form.
         * It also checks for existing of cookies to handle authentication.
         */
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user)
        {
            // Validate the model state
            if (!ModelState.IsValid) return View(user); // Return the view with validation errors if the model state is invalid
            // Check if the user exists in the database
            var existingUser = await _appDBContext.User.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (existingUser == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                ViewData["ErrorMessage"] = "Invalid email or password."; // Set an error message to be displayed in the view
                return View(user);
            }
            // Create a claims identity for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Username),
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim("UserId", existingUser.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties() { AllowRefresh = true, /* Allow the authentication cookie to be refreshed */ };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);


            // Redirect to a success page or dashboard if login is successful
            return RedirectToAction("Index", "Home"); // Assuming you have a HomeController with an Index action
        }

        /** 
         * This method handles user registration by validating the provided information and saving it to the database.
         * It also checks for existing users with the same email.
         */
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM user)
        {
            // Validate the model state
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords do not match.");
                ViewData["ErrorMessage"] = "Passwords do not match."; // Set an error message to be displayed in the view
                return View(user);
            }
            if (!ModelState.IsValid) View(user); // Return the view with validation errors if the model state is invalid

            // Verify if email exist
            var existingUser = await _appDBContext.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("email", "Email already exists.");
                ViewData["ErrorMessage"] = "Email already exists.";
                return View(user);
            }

            // first of all, create a new user object
            var newUser = new Models.User()
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };

            // Add the new user to the database
            _appDBContext.User.Add(newUser);
            await _appDBContext.SaveChangesAsync();

            // Redirect to a success page or login page if registration is successful
            if (newUser.Id != 0) return RedirectToAction("Login", "Access");
            ModelState.AddModelError("", "Registration failed. Please try again.");
            ViewData["ErrorMessage"] = "Registration failed. Please try again."; // Set an error message to be displayed in the view
            return View();
        }
    }
}
