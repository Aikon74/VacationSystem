using Microsoft.AspNetCore.Mvc;
using VacationSystem.Data;
using VacationSystem.Models;

namespace VacationSystem.Controllers
{
    public class PlanningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Suggest()
        {
            ViewBag.Institutions = GetInstitutions();
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Suggest(DateTime date, string institution)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(institution))
            {
                ViewBag.Error = "Du må velge en institusjon.";
            }
            else
            {
                var suggestion = new PlanningSuggestion
                {
                    UserId = userId.Value,
                    Date = date,
                    Institution = institution,
                    Approved = false
                };

                _context.PlanningSuggestions.Add(suggestion);
                _context.SaveChanges();

                ViewBag.Success = "Forslaget er sendt inn for godkjenning.";
            }

            ViewBag.Institutions = GetInstitutions();
            return View();
        }

        private List<string> GetInstitutions()
        {
            return new List<string>
    {
        // Haugesund – Barnehager
        "Bleikemyr barnehage",
        "Hemmingstad barnehage",
        "Lysskar barnehage",
        "Sagatun barnehage",
        "Solandsbakken barnehage",
        "Espira Bråsteintunet",
        "Espira Bjørgene",
        "Små Barnehager Sentrum AS",
        "St. Olavs barnehage",
        "Amanda foreldrelagsbarnehage BA",
        "Ona FUS barnehage AS",
        "Balder FUS barnehage AS",
        "Haugaland Idrettsbarnehage AS",
        "Nøtteliten familiebarnehage",

        // Haugesund – Skoler
        "Austrheim skole",
        "Brakahaug skole",
        "Gard skole",
        "Hauge skole",
        "Håvåsen skole",
        "Lillesund skole",
        "Rossabø skole",
        "Røvær skole",
        "Saltveit skole",
        "Skåredalen skole",
        "Solvang skole",
        "Haraldsvang ungdomsskole",
        "Breidablik læringssenter",
        "Steinerskolen i Haugesund",
        "Danielsen ungdomsskole",

        // Tysvær – Barnehager
        "Aksdal barnehage",
        "Frakkagjerd barnehage",
        "Førland barnehage",
        "Grinde barnehage",
        "Nedstrand barnehage",
        "Skjoldastraumen barnehage",
        "Tysværvåg barnehage",
        "Espira Garhaug",
        "FUS barnehager (Tysvær)",

        // Tysvær – Skoler
        "Frakkagjerd barneskole",
        "Frakkagjerd ungdomsskole",
        "Førland skule",
        "Førre skole",
        "Grinde skule",
        "Nedstrand barne- og ungdomsskule",
        "Tysværvåg barne- og ungdomsskule",
        "Straumen skule",
        "Tysvær opplæringssenter",

        // Karmøy – Barnehager
        "Aski barnehage",
        "Avaldsnes barnehage",
        "Bygnes vitenbarnehage",
        "Espira Karmsund barnehage",
        "Espira Litlasund barnehage",
        "Espira Sletten barnehage",
        "Torvastad barnehage",
        "Små Barnehager AS (Karmøy)",

        // Karmøy – Skoler
        "Åkra skole",
        "Avaldsnes skole",
        "Bygnes skole",
        "Kopervik skole",
        "Norheim skole",
        "Skudenes skole",
        "Stangeland skole",
        "Torvastad skole",
        "Vea skole",
        "Vormedal skole",

        // Sveio – Barnehager
        "Espira Solkroken",
        "Ekrene barnehage",
        "Sveio barnehage",
        "Auklandshamn barnehage",

        // Sveio – Skoler
        "Auklandshamn skule",
        "Førde skule",
        "Våga skule",
        "Sveio skule",
        "Rød skule"
    };
        }


        // GET: /Planning/AdminPanel
        public IActionResult AdminPanel()
        {
            var allSuggestions = _context.PlanningSuggestions
                .OrderByDescending(p => p.Date)
                .ToList();

            return View(allSuggestions);
        }

        public IActionResult Approve(int id)
        {
            var suggestion = _context.PlanningSuggestions.Find(id);
            if (suggestion != null)
            {
                suggestion.Approved = true;
                _context.SaveChanges();
            }
            return RedirectToAction("AdminPanel");
        }

        public IActionResult Reject(int id)
        {
            var suggestion = _context.PlanningSuggestions.Find(id);
            if (suggestion != null)
            {
                _context.PlanningSuggestions.Remove(suggestion);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminPanel");
        }

    }
}
