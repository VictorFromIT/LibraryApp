using LibraryApp.Models;
using LibraryApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBookModelRepository _bookModelRepository;
		private readonly IClientModelRepository _clientModelRepository;

		public HomeController(IBookModelRepository bookModelRepository, IClientModelRepository clientModelRepository)
		{
			_bookModelRepository = bookModelRepository;
			_clientModelRepository = clientModelRepository;
		}

		public ActionResult Index()
		{
			var books = _bookModelRepository.GetAllBooks();
			return View(books);
		}

		[HttpGet]
		public JsonResult GetBooks()
		{
			var books = _bookModelRepository.GetAllBooks();
			return Json(books, JsonRequestBehavior.AllowGet);
		}
		

		[HttpPost]
		public JsonResult UpdateBookDescription(int id, string description)
		{
			try
			{
				_bookModelRepository.UpdateBookDescription(id, description);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public ActionResult RentOrReturnBook(int bookId, string clientEmail, bool isAvailable)
		{
			try
			{
				if (isAvailable)
				{
					var isMailExist = _clientModelRepository.IsEmailExists(clientEmail);

					if (isMailExist)
					{
						// Если книга доступна, арендуем её
						_bookModelRepository.RentBook(bookId, clientEmail);
					}
					else
					{
						return Json(new { success = false, message = "Такого Email не существует!" });
					}
					
				}
				else
				{
					// Если книга уже арендована, возвращаем её
					_bookModelRepository.ReturnBook(bookId);
				}
				return Json(new { success = true, isAvailable = !isAvailable });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}
	}
}