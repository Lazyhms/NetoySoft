using Microsoft.Extensions.DependencyInjection;
using Netoysoft.EntityFrameworkCore.Metadata.Conventions;

namespace Netoysoft.EntityFrameworkCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCoreServices(this IServiceCollection serviceCollection)
    {
        new EntityFrameworkRelationalServicesBuilder(serviceCollection)
            .TryAdd<IConventionSetPlugin, ConventionSetPlugin>();

        return serviceCollection;
    }
}
