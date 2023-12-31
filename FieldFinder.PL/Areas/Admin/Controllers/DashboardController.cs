﻿using FieldFinder.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;


namespace Pharmacy.PL.Areas.Admin.Controllers
{
  
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IUnitOfWork unitOfwork , ILogger<DashboardController> logger )
        {

            _unitOfwork = unitOfwork;
            _logger=logger;
            _logger.LogInformation("Dashboard Controller Called");
        }
        public IActionResult Index()
        {
            _logger.LogInformation("U transfered to Dashboard Home Page ");
            return View();
        }


        #region Api CAll for FieldData
        
        public  async Task<IActionResult> GetFields()
        {
            var FieldsList = await _unitOfwork.Fields.GetAll();
            //var json = JsonSerializer.Serialize(FieldsList, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            return Json(new { data = FieldsList });
        }
        #endregion
    }
}
