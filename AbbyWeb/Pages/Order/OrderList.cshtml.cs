using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Order
{
    public class OrderListModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
        }
    }
}
