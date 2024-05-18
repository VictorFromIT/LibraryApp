using LibraryApp.Models;
using LibraryApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.Controllers
{
    public class ClientController : Controller
    {
		private readonly IClientModelRepository _clientModelRepository;

		public ClientController(IClientModelRepository clientModelRepository)
		{
			_clientModelRepository = clientModelRepository;
		}
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult AddClient()
		{
			return View();
		}

		[HttpGet]
		public JsonResult GetClients()
		{
			var books = _clientModelRepository.GetAllClients();
			return Json(books, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AddClient(Client model)
		{
			if (ModelState.IsValid)
			{
				var isMailExist = _clientModelRepository.IsEmailExists(model.Email);

				if (!isMailExist)
				{
					_clientModelRepository.AddClient(model);
					return Json(new { success = true });
				}
				else
				{
					return Json(new { success = false, message = "Такого Email уже существует!" });
				}
			}
			return Json(new { success = false, message = "Некорректные данные" });
		}
	}
}