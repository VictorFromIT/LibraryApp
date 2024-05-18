using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
	public interface IClientModelRepository
	{
		//Получение списка клиентов
		IEnumerable<Client> GetAllClients();
		//Добавление клиента
		void AddClient(Client client);

		//Проверка, существует клиент с таким Email
		bool IsEmailExists(string email);
	}
}
