using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FieldFinder.PL.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Index
        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.Categories.GetAll();
            return View(categories);
        }
        #endregion


        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Add(category);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #endregion


        #region Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); // Status Code 400

            var categories = await _unitOfWork.Categories.GetById(C => C.Id == id);

            if (categories is null)
                return NotFound();
            return View(ViewName, categories);
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category, [FromRoute] int id)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Categories.Update(category);
                    await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(category);
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category, [FromRoute] int id)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Categories.Delete(category);
                    await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(category);
        }
        #endregion
    }
}
       
