using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAttributesPersistenceServices();

        }

        /// <summary>
        /// Logic:
        /// Core.Application.Attributes.AddScopetService class with attributes value added and interface specified in attributes
        /// automatically adds the type values to AddScopet by finding the value in the existing assembly.
        /// Detailed information is given in :Core.Application.Attributes.AddScopetService.
        ///
        /// </summary>
        /// <param name="services"></param>
        public static void AddAttributesPersistenceServices(this IServiceCollection services)
        {
           
            List<Type> dynamicServiceList = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetCustomAttribute<AddScopedService>() != null).ToList();

            if (dynamicServiceList != null)
            {
                dynamicServiceList.ForEach(Class => {
                   
                    Attribute attributes = Class.GetCustomAttribute<AddScopedService>();
                    Type attributesType = attributes.GetType();
                    string attributesPropValue = attributesType.GetProperty(nameof(AddScopedService.Interface)).GetValue(attributes)?.ToString();
                    if (!string.IsNullOrEmpty(attributesPropValue))
                    {
                       
                        Type interfaceType = Class.GetInterface(attributesPropValue);
                        
                        if (interfaceType != null)
                        {
                            services.AddScoped(interfaceType, Class);
                        }

                    }

                });
            }
        }

    }
}
