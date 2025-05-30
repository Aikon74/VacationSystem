using Microsoft.AspNetCore.Mvc;
using VacationSystem.Data;
using VacationSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace VacationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(int userId, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                ViewBag.Error = "Ugyldig bruker-ID";
                return View();
            }

            string hash = ComputeSha256Hash(password);
            if (user.PasswordHash != hash)
            {
                ViewBag.Error = "Feil passord";
                return View();
            }

            // Bruk session til å lagre pålogget bruker
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetInt32("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        // Logg ut
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }


        // GET: /Account/AddUser
        // GET
        public IActionResult AddUser()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult AddUser(string password, int role)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Passord kan ikke være tomt.";
                return View();
            }

            string hash = ComputeSha256Hash(password);

            int nextId = (_context.Users.Any())
                ? _context.Users.Max(u => u.UserId) + 1
                : 1001;

            var user = new User
            {
                UserId = nextId,
                PasswordHash = hash,
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ViewBag.Success = $"Bruker opprettet med ID: {user.UserId}";
            return View();
        }




        // Admin: Vis liste over brukere
        public IActionResult UserList()
        {
            var users = _context.Users.OrderBy(u => u.UserId).ToList();
            return View(users);
        }

        // Admin: Endre passord
        public IActionResult ChangePassword(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(int id, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null && !string.IsNullOrWhiteSpace(newPassword))
            {
                user.PasswordHash = ComputeSha256Hash(newPassword);
                _context.SaveChanges();
                ViewBag.Success = "Passord endret.";
            }
            ViewBag.UserId = id;
            return View();
        }

        // Admin: Slett bruker
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("UserList");
        }

    }
}

