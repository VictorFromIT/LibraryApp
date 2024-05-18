using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
	public class Client
	{
		// ID клиента
		public int Id { get; set; }
		// Имя клиента
		public string Name { get; set; }
		//Почта клиента
		public string Email { get; set; }
	}
}