using AutoMapper;
using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Entities;
using FieldFinder.PL.Helpers;
using FieldFinder.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace FieldFinder.PL.Areas.Admin.Controllers
{
    public class FieldController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FieldController> _logger;

        public FieldController(IUnitOfWork unitOfWork, IMapper mapper , ILogger<FieldController> logger)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _logger=logger;
            _logger.LogInformation("Field Controller Called Successfully");
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Redirect To Index Page In Field Controller ");
            var Fields = await _unitOfWork.Fields.GetAll();
            return View(Fields);
        }

        #endregion


        #region Create

        public async Task<IActionResult> Create()
        {
            //var Categories = await _unitOfWork.Categories.GetAll();
            //IEnumerable<SelectListItem> CategoryList = Categories.Select(
            //    c => new SelectListItem()
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()
            //    });
            //ViewBag.CategoryList = CategoryList;
            TempData["Operation"] = "Create Field";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FieldViewModel fieldViewModel)
        {
            if (ModelState.IsValid)
            {
                fieldViewModel.ImageName = DocumetSetting.UploadDocument(fieldViewModel.Image, "Images");
                var MappedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                _unitOfWork.Fields.Add(MappedField);
                await _unitOfWork.Save();
                _logger.LogInformation("A New Field Added Successfully to Database ");
                TempData["Success"] = "Field Added Succcessfully";
                return RedirectToAction(nameof(Index));
            }

            return View(fieldViewModel);
        }

        #endregion


        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null || id==0)
                return NotFound();

            var Field = await _unitOfWork.Fields.GetById(f => f.Id == id);
            var FieldToEdit = _mapper.Map<Field, FieldViewModel>(Field);
            TempData["Operation"] = "Edit Field";
            return View(FieldToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, FieldViewModel fieldViewModel)
        {
            if (fieldViewModel.Id != id)
                return BadRequest();

            if ((ModelState.TryGetValue("Image", out ModelStateEntry entry)) && entry.Errors.Count>0)
            {
                var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                _unitOfWork.Fields.Update(CastedField);
                await _unitOfWork.Save();
                _logger.LogInformation("Field Updated Successfully ");
                TempData["Success"] = "Field Updated Succcessfully With its Old Image";
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    DocumetSetting.DeleteFile(fieldViewModel.ImageName, "Images");
                    _logger.LogInformation("Field Old Image Deleted Successfully Before Uploading The New One ");

                    fieldViewModel.ImageName =  DocumetSetting.UploadDocument(fieldViewModel.Image, "Images");
                    _logger.LogInformation("Field New Image Is Uploaded Successfully");

                    var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                    _unitOfWork.Fields.Update(CastedField);
                    await _unitOfWork.Save();
                    _logger.LogInformation("Field Updated Successfully With New Image");

                    TempData["Success"] = "Field Updated Succcessfully";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(fieldViewModel);
                }
            }


            return View(fieldViewModel);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id ==0)
                return NotFound();

            var Categories = await _unitOfWork.Categories.GetAll();
            var Locations = await _unitOfWork.Locations.GetAll();
            IEnumerable<SelectListItem> CategoryList = Categories.Select(c =>
            new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            IEnumerable<SelectListItem> LocationList = Locations.Select(l =>
            new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;
            ViewBag.LocationList = LocationList;

            var Field = await _unitOfWork.Fields.GetById(f => f.Id == id);
            var CastedField = _mapper.Map<Field, FieldViewModel>(Field);
            return View(CastedField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, FieldViewModel fieldViewModel)
        {
            if (fieldViewModel.Id!=id)
                return BadRequest();


            var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
            DocumetSetting.DeleteFile(fieldViewModel.ImageName, "Images");
            _logger.LogInformation("Field Image Deleted Successfully ");
            _unitOfWork.Fields.Delete(CastedField);
            await _unitOfWork.Save();
            _logger.LogInformation("Field Deleted From DB ");
            TempData["Success"]="Field Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }
    
        #endregion
    }

}
