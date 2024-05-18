using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
	public interface IBookModelRepository
	{
		//Вывод всех книг
		IEnumerable<Book> GetAllBooks();
		//Добавление новой книги
		void AddBook(Book book);
		//Обновление описания книги
		void UpdateBookDescription(int id, string description);
		//Аренда книги
		void RentBook(int bookId, string clientEmail);
		//Возврат книги
		void ReturnBook(int bookId);
		//Проверка наличия книги
		bool IsBookAvailable(int bookId);
	}
}
