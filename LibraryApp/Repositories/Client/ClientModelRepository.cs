using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryApp.Repositories
{
	public class ClientModelRepository : IClientModelRepository
	{
		private readonly string _connectionString;

		public ClientModelRepository(string connectionString)
		{
			_connectionString = connectionString;
		}
		//Получение списка клиентов
		public IEnumerable<Client> GetAllClients()
		{
			var clients = new List<Client>();

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var command = new SqlCommand("SELECT Id, Name, Email FROM Clients", connection);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var client = new Client
						{
							Id = reader.GetInt32(0),
							Name = reader.GetString(1),
							Email = reader.GetString(2)
						};
						clients.Add(client);
					}
				}
			}
			return clients;
		}
		//Добавление клиента
		public void AddClient(Client client)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var command = new SqlCommand("INSERT INTO Clients (Name, Email) VALUES (@Name, @Email)", connection);
				command.Parameters.AddWithValue("@Name", client.Name);
				command.Parameters.AddWithValue("@Email", client.Email);

				command.ExecuteNonQuery();
			}
		}
		//Сущетсвует ли такой Email
		public bool IsEmailExists(string email)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();

					string query = "SELECT * FROM Clients WHERE Email = @Email";
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@Email", email);

					object result = command.ExecuteScalar();
					return result != null ? true : false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}