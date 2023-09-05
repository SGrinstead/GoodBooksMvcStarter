using GoodBooksMvc.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoodBooksMvc.Controllers
{
	public class BooksController : Controller
	{
		private readonly GoodBooksMvcContext _context;

		public BooksController(GoodBooksMvcContext context)
		{
			_context = context;
		}

		public IActionResult Index(string? selectedBookId)
		{
			var books = _context.Books.ToList();
			ViewData["SelectedStringIds"] = new List<string>();
			List<string> selectedStringIds;
			if (int.TryParse(selectedBookId, out int bookId))
			{		
				if (!Request.Cookies.ContainsKey("SelectedBooks"))
				{
					Response.Cookies.Append("SelectedBooks", bookId.ToString());
					selectedStringIds = new List<string> { bookId.ToString() };
				}
				else
				{
					string newValue = Request.Cookies["SelectedBooks"] + "," + bookId.ToString();
					Response.Cookies.Append("SelectedBooks", newValue);
					selectedStringIds = Request.Cookies["SelectedBooks"].Split(",").ToList();
				}
				
				ViewData["SelectedStringIds"] = selectedStringIds;
			}
			else if(selectedBookId is null && Request.Cookies.ContainsKey("SelectedBooks"))
			{
				selectedStringIds = Request.Cookies["SelectedBooks"].Split(",").ToList();
				ViewData["SelectedStringIds"] = selectedStringIds;
			}

			return View(books);
		}
	}
}
