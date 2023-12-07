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

        public FieldController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        #region Index
        public IActionResult Index()
        {
            var Fields = _unitOfWork.Fields.GetAll();
            return View(Fields);
        }

        #endregion


        #region Create

        public IActionResult Create()
        {
            //var CategoryList = _unitOfWork.Categories.GetAll().Select(
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
        public IActionResult Create(FieldViewModel fieldViewModel)
        {
            if (ModelState.IsValid)
            {
                fieldViewModel.ImageName = DocumetSetting.UploadDocument(fieldViewModel.Image, "Images");
                var MappedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                _unitOfWork.Fields.Add(MappedField);
                _unitOfWork.Save();
                TempData["Success"] = "Field Added Succcessfully";
                return RedirectToAction(nameof(Index));
            }

            return View(fieldViewModel);
        }

        #endregion


        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
                return NotFound();

            var Field = _unitOfWork.Fields.GetById(f => f.Id == id);
            var FieldToEdit = _mapper.Map<Field, FieldViewModel>(Field);
            TempData["Operation"] = "Edit Field";
            return View(FieldToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, FieldViewModel fieldViewModel)
        {
            if (fieldViewModel.Id != id)
                return BadRequest();

            if ((ModelState.TryGetValue("Image", out ModelStateEntry entry)) && entry.Errors.Count>0)
            {
                var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                _unitOfWork.Fields.Update(CastedField);
                _unitOfWork.Save();
                TempData["Success"] = "Field Updated Succcessfully";
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    DocumetSetting.DeleteFile(fieldViewModel.ImageName, "Images");
                    fieldViewModel.ImageName =  DocumetSetting.UploadDocument(fieldViewModel.Image, "Images");
                    var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
                    _unitOfWork.Fields.Update(CastedField);
                    _unitOfWork.Save();
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

        public IActionResult Delete(int? id)
        {
            if (id == null || id ==0)
                return NotFound();

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Categories.GetAll().Select(c =>
            new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            var Field = _unitOfWork.Fields.GetById(f => f.Id == id);
            var CastedField = _mapper.Map<Field, FieldViewModel>(Field);
            return View(CastedField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, FieldViewModel fieldViewModel)
        {
            if (fieldViewModel.Id!=id)
                return BadRequest();


            var CastedField = _mapper.Map<FieldViewModel, Field>(fieldViewModel);
            DocumetSetting.DeleteFile(fieldViewModel.ImageName, "Images");
            _unitOfWork.Fields.Delete(CastedField);
            _unitOfWork.Save();
            TempData["Success"]="Field Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }
    
        #endregion
    }

}
