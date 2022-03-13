using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }

        public DeleteModel(ApplicationDbContext db)    //DI
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);
            //Category = _db.Category.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            var FoodTypeFromDb = _db.FoodType.Find(FoodType.Id);
            if (FoodTypeFromDb != null)   //check post isValid (blank => not valid)
            {
                _db.FoodType.Remove(FoodTypeFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
