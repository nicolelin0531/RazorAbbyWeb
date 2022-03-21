using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public FoodType FoodType { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)    //DI
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);
            //Category = _db.Category.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            var FoodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == FoodType.Id);
            if (FoodTypeFromDb != null)   //check post isValid (blank => not valid)
            {
                _unitOfWork.FoodType.Remove(FoodTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "FoodType deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
