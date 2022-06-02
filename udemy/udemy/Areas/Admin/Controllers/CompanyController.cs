using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;

namespace udemy.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                //create product
                return View(company);
            }
            else
            {
                company = _unitofwork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
                
                //update product
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                
                if (obj.Id == 0)
                {
                    _unitofwork.Company.Add(obj);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitofwork.Company.Update(obj);
                    TempData["success"] = "Product updated successfully";
                }
                _unitofwork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitofwork.Company.GetAll();
            return Json(new {data = companyList});
        }
        //Post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var temp = _unitofwork.Company.GetFirstOrDefault(c => c.Id == id);
            if (temp == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitofwork.Company.Remove(temp);
            _unitofwork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            
        }
        #endregion
    }
}
