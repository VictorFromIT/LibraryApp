using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
	public class Book
	{
		// ID книги
		public int Id { get; set; }
		// Название книги
		public string Name { get; set; }
		// Автор книги
		public string Author { get; set; }
		// Описание книги
		public string Description { get; set; }
		//Выдана книга или доступна
		public bool IsAvailable { get; set; }
		//Id клиента, который арендует книгу
		public int ClientId { get; set; }
	}
}