using FieldFinder.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;


namespace Pharmacy.PL.Areas.Admin.Controllers
{
  
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public DashboardController(IUnitOfWork unitOfwork )
        {
            _unitOfwork = unitOfwork;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region Api CAll for FieldData
        
        public  IActionResult GetFields()
        {
            var FieldsList =  _unitOfwork.Fields.GetAll();
            //var json = JsonSerializer.Serialize(FieldsList, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            return Json(new { data = FieldsList });
        }
        #endregion
    }
}
