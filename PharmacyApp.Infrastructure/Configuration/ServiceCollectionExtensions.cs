using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Infrastructure.Data;

namespace PharmacyApp.Infrastructure.Configuration
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInMemoryDbContext(this IServiceCollection services)
		{
			services.AddDbContext<PharmacyDbContext>(options =>
				options.UseInMemoryDatabase("PharmacyDb"));

			return services;
		}
	}
}


