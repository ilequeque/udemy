using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace udemy.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitofwork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.CoverType.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverType = _unitofwork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CoverType == null)
            {
                return NotFound();
            }

            return View(CoverType);
        }

        //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.CoverType.Update(obj);
                _unitofwork.Save();
                TempData["success"] = "Cover Type updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverType = _unitofwork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CoverType == null)
            {
                return NotFound();
            }

            return View(CoverType);
        }

        //GET
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var temp = _unitofwork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (temp == null)
            {
                return NotFound();
            }

            _unitofwork.CoverType.Remove(temp);
            _unitofwork.Save();
            TempData["success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
