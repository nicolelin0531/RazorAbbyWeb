using Abby.DataAccess.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }

        public CreateModel(ApplicationDbContext db)    //DI
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)   //check post isValid (blank => not valid)
            {
                await _db.FoodType.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
