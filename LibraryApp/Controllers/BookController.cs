using LibraryApp.Models;
using LibraryApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
		private readonly IBookModelRepository _bookModelRepository;

		public BookController(IBookModelRepository bookModelRepository)
		{
			_bookModelRepository = bookModelRepository;
		}

		public ActionResult Index()
        {
            return View();
        }

		public ActionResult AddBook()
		{
			return View();
		}

		[HttpPost]
		public JsonResult AddBook(Book model)
		{
			if (ModelState.IsValid)
			{
				_bookModelRepository.AddBook(model);
				return Json(new { success = true });
			}
			return Json(new { success = false, message = "Некорректные данные" });
		}
	}
}