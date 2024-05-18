using LibraryApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Configuration;

namespace LibraryApp
{
	public class AutofacConfig
	{
		public static void RegisterDependencies()
		{
			var builder = new ContainerBuilder();

			// Строка подключения из web.config
			string connectionString = ConfigurationManager.ConnectionStrings["LibraryTestDB"].ConnectionString; 

			// Регистрируем реализацию моделей данный с передачей строки подключения
			builder.Register(c => new BookModelRepository(connectionString)).As<IBookModelRepository>();
			builder.Register(c => new ClientModelRepository(connectionString)).As<IClientModelRepository>();

			// Регистрируем контроллеры
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}