using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace LibraryApp.Database
{
	public class DatabaseInitializer
	{
		public static void Initialize(string connectionString, string scriptFilePath)
		{
			var script = File.ReadAllText(HttpContext.Current.Server.MapPath(scriptFilePath));

			using (var connection = new SqlConnection(connectionString))
			{
				var command = new SqlCommand(script, connection);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}
	}
}