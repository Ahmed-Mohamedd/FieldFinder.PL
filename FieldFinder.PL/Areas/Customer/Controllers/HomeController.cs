using FieldFinder.BLL.Interfaces;
using FieldFinder.BLL.Repositories;
using FieldFinder.DAL.Context;
using FieldFinder.DAL.Entities;
using FieldFinder.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FieldFinder.PL.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork=unitOfWork;
        }

        //ApplicationDbContext _context = new ApplicationDbContext();
        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                Locations = await _unitOfWork.Locations.GetAll(),
                Categories = await _unitOfWork.Categories.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var field = await _unitOfWork.Fields.GetById(f=> f.Id == id);
            return View(field);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // api call GetFields Based on Category & Location

        #region Api Calls
            public async Task<IActionResult> GetFields(int? locationId ,int? categoryId)
            {
                var fields =  await _unitOfWork.Fields.GetAll();
            
                if(locationId is null)
                    return Ok(fields.Where(f => (f.CategoryId == categoryId) ));
                else if (categoryId is null)
                    return Ok(fields.Where(f =>  (f.LocationId == locationId)));
                else
                    return Ok(fields.Where(f=> (f.CategoryId == categoryId) && (f.LocationId == locationId)));
            }

        #endregion

    }
}
