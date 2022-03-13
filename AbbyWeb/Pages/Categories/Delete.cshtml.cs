using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category{ get; set; }

        public DeleteModel(ApplicationDbContext db)    //DI
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);
            //Category = _db.Category.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            var categoryFromDb = _db.Category.Find(Category.Id);
            if (categoryFromDb != null)   //check post isValid (blank => not valid)
            {
                _db.Category.Remove(categoryFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
