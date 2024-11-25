using Core.Application.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class ServiceRegistration
    {
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

        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
        }
    }
}
