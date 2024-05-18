using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryApp.Repositories
{
	public class BookModelRepository : IBookModelRepository
	{
		private readonly string _connectionString;

		public BookModelRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IEnumerable<Book> GetAllBooks()
		{
			var books = new List<Book>();

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var command = new SqlCommand("SELECT Id, Name, Author, Description, IsAvailable FROM Books", connection);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var book = new Book
						{
							Id = reader.GetInt32(0),
							Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
							Author = reader.IsDBNull(reader.GetOrdinal("Author")) ? "" : reader.GetString(reader.GetOrdinal("Author")),
							Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
							IsAvailable = !reader.IsDBNull(reader.GetOrdinal("IsAvailable")) ? reader.GetBoolean(reader.GetOrdinal("IsAvailable")) : true
					};
						books.Add(book);
					}
				}
			}
			return books;
		}

		public void AddBook(Book book)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var command = new SqlCommand("INSERT INTO Books (Name, Author, Description, IsAvailable) VALUES (@Name, @Author, @Description, @IsAvailable)", connection);
				command.Parameters.AddWithValue("@Name", book.Name);
				command.Parameters.AddWithValue("@Author", book.Author);
				command.Parameters.AddWithValue("@Description", book.Description);
				command.Parameters.AddWithValue("@IsAvailable", true);

				command.ExecuteNonQuery();
			}
		}

		public void UpdateBookDescription(int id, string description)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				string sql = "UPDATE Books SET Description = @Description WHERE Id = @Id";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.Parameters.AddWithValue("@Description", description);
					command.ExecuteNonQuery();
				}
			}
		}

		public void RentBook(int bookId, string clientEmail)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				string sqlUpdateBook = "UPDATE Books SET IsAvailable = 0, Client_Id = (SELECT Id FROM Clients WHERE Email = @Email) WHERE Id = @BookId";
				SqlCommand command = new SqlCommand(sqlUpdateBook, connection);
				command.Parameters.AddWithValue("@Email", clientEmail);
				command.Parameters.AddWithValue("@BookId", bookId);
				command.ExecuteNonQuery();
			}
		}
		public void ReturnBook(int bookId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				string sql = "UPDATE Books SET IsAvailable = 1, Client_Id = NULL WHERE Id = @BookId";
				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddWithValue("@BookId", bookId);
				command.ExecuteNonQuery();
			}
		}
		public bool IsBookAvailable(int bookId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				string sql = "SELECT IsAvailable FROM Books WHERE Id = @BookId";
				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddWithValue("@BookId", bookId);
				return (bool)command.ExecuteScalar();
			}
		}
	}
}