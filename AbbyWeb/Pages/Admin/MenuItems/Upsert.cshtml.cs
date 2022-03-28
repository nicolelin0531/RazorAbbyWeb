using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbbyWeb.Pages.Admin.MenuItems;
[BindProperties]
public class UpsertModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    public MenuItem MenuItem { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> FoodTypeList { get; set; }

    public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)    //DI
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
        MenuItem = new();
    }
    public void OnGet(int? id)
    {
        if (id != null)
        {
            //Edit
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id);
        }

        CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
        {
            Text = i.Name,
            Value = i.Id.ToString()
        });

        FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem()
        {
            Text = i.Name,
            Value = i.Id.ToString()
        });

    }

    public async Task<IActionResult> OnPost()
    {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (MenuItem.Id == 0)
        {
            //create
            string filename_new = Guid.NewGuid().ToString();   //���W�r new filename, make sure all the file names are unique
            var uploads = Path.Combine(webRootPath, @"images\menuItems");  //���|��www/images���U�� find out folder that are upload
            var extension = Path.GetExtension(files[0].FileName);  //make soure files have same extension

            using (var fileStream = new FileStream(Path.Combine(uploads, filename_new + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            MenuItem.Image = @"\images\menuItems\" + filename_new + extension;
            _unitOfWork.MenuItem.Add(MenuItem);
            _unitOfWork.Save();
        }
        else
        {

        }
        return RedirectToPage("./Index");
    }
}

