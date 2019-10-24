using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
    public static class InitialRepository
    {
        public static void AddRepositories(this IServiceCollection services, string[] path, Type dbContextType,
            ServiceLifetime serviceLifetime)
        {
            var classWhereBaseTypeNotNull = new List<Type>();
            foreach (string item in path)
                classWhereBaseTypeNotNull.AddRange(Assembly.Load(item).GetTypes()
                    .Where(q => q.BaseType != null && !q.IsAbstract && !q.IsInterface));

            IEnumerable<Type> entities =
                classWhereBaseTypeNotNull.Where(q => q.BaseType.FullName.Equals(typeof(Entity).FullName));
            foreach (Type item in entities)
            {
                Type repositoryType = typeof(IRepository<>).MakeGenericType(item);
                Type efRepositoryImplType = typeof(EfRepositoryImpl<,>).MakeGenericType(item, dbContextType);
                services.Add(new ServiceDescriptor(repositoryType, efRepositoryImplType, serviceLifetime));
            }
        }
    }
}