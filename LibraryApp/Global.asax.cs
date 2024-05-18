using LibraryApp.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// Строка подключения из web.config
			string connectionString = ConfigurationManager.ConnectionStrings["LibraryTestDB"].ConnectionString;

			//Создание таблиц
			string scriptFilePathBooks = "~/SQL_Scripts/CreateTableBooks.sql"; // путь к файлу создания первой таблицы
			string scriptFilePathClients = "~/SQL_Scripts/CreateTableClients.sql"; // путь к файлу создания второй таблицы

			DatabaseInitializer.Initialize(connectionString, scriptFilePathClients);
			DatabaseInitializer.Initialize(connectionString, scriptFilePathBooks);

			AutofacConfig.RegisterDependencies();

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
