

namespace Core.Application.Attributes
{
    /// <summary>
    /// Automatically performs Injection to the default IoC structure that comes with Microsoft .net technology.
    /// 
    /// We use it for our concrete classes that we want to add to Microsoft.Extensions.DependencyInjection.AddScopet.
    /// Example:
    /// 
    /// [AddScopedService(Interface = “ICompanyRepository”)]
    /// public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    /// 
    /// As mentioned above, we have added the attribute to our concrete class and if we want which interface we have added the attribute from IoC
    /// If we want the class to be passed to us, we pass the relevant interface value.
    /// 
    /// Conclusion:
    /// In the constructor of any class in the system, ICompanyService is the concrete class to which the attribute is added when requested, i.e. 
    /// ICompanyService will be forwarded.
    /// 
    /// The point where this process is executed is in each layer's own Microsoft.Extensions.DependencyInjection extension method.
    /// Example: Infrastructure.Persistence.AddAttributesPersistenceServices
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AddScopedService : Attribute
    {
        public string Interface { get; set; }
    }
}
