using Microsoft.AspNetCore.Mvc;
using FieldFinder.BLL.Interfaces;
using FieldFinder.BLL.Repositories;
using FieldFinder.DAL.Entities;
using System.Net;

namespace FieldFinder.PL.Areas.Admin.Controllers
{
    public class CoachController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoachController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork; 
            
        }
        public async Task<IActionResult> Index()
        {
            var coachs = await _unitOfWork.Coaches.GetAll();
            return View(coachs);    
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Coach coach)
        {
            if (ModelState.IsValid)//server side validation
            {
                _unitOfWork.Coaches.Add(coach);
                await _unitOfWork.Save();   
                return RedirectToAction("Index");   
            }
            return View(coach);  

        }
        //url/Coach/Details/id
        public async Task<IActionResult> Details(int id)
        {

            var coach = await _unitOfWork.Coaches.GetById(d => d.Id == id);
            if (coach is null)
                return NotFound();
            return View(coach);
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
           
            var coach = await _unitOfWork.Coaches.GetById(c=>c.Id==id);
            if (coach is null)
                return NotFound();
            return View(coach);         
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(Coach coach) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Coaches.Update(coach);
                await _unitOfWork.Save();

           
                return RedirectToAction("Index");   

            }
            return View(coach);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var coach = await _unitOfWork.Coaches.GetById (c => c.Id == id);
            if (coach is null)
                return NotFound();
            return View(coach); 

        }
        // POST: Coach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( Coach coach )
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Coaches.Delete(coach);
                await _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(coach);

        }


    }
}
