using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace GStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        ApplicationDbContext dbContext;
        private readonly ICookieService _cookieService;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db,
            ICookieService cookieService)
        {
            
            _logger = logger;
            dbContext = db;
            _cookieService = cookieService;
        }

        public IActionResult Index()
        {

            string myCookieValue = _cookieService.GetCookieValue();


            //var student = _db.Products
            //    .Include(s => s.SizeSets)
            //    .Include(s => s.ColorSets)
            //    .ToList();

            //    .ThenInclude(e => e.Course)
            //.AsNoTracking()
            //.FirstOrDefaultAsync(m => m.ID == id);
            //IdentityRole identityRole = new IdentityRole
            //{
            //    Name = Utils.Constants.Roles.Guest
            //};

            //// Saves the role in the underlying AspNetRoles table
            //IdentityResult result = await _roleManager.CreateAsync(identityRole);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShirtFullPreview(int? id)
        {
            SqlParameter shirtIdParam = new SqlParameter("@ShirtId", SqlDbType.Int);
            shirtIdParam.Value = id.Value;

            List<spShirtFullShopPreviewModel> spShirtSqlShort =
                dbContext.spShirtFullShopPreview
                .FromSqlInterpolated($"spShirtFullShopPreview {shirtIdParam}")
                .AsNoTracking().ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}