using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Film.Entity
{
    public static class ScopeCreator
    {
        public static void GerarDependencias(IServiceCollection services)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            loadedAssemblies
                .SelectMany(x => x.GetReferencedAssemblies())
                .Distinct()
                .Where(y => y.FullName.Contains("Film"))
                .Where(y => loadedAssemblies.Any((a) => a.FullName == y.FullName) == false)
                .ToList()
                .ForEach(x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x)));

            var fullList = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.Contains("Film"))
                    .SelectMany(a => a.GetTypes().Where(t => IsClassOrSubclassOf(t, typeof(EntityBase))));

            var registerList = fullList.Except(new List<Type>() { typeof(EntityBase) });
            foreach (var entidade in registerList)
                services.AddScoped(typeof(Repository<>).MakeGenericType(entidade));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        private static bool IsClassOrSubclassOf(Type t, Type typeToCompare)
        {
            if (t == null) throw new ArgumentNullException("t");
            if (t == typeToCompare) return true;
            if (t.BaseType == null) return false;
            if (t.BaseType == typeToCompare) return true;
            if (t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeToCompare) return true;

            return IsClassOrSubclassOf(t.BaseType, typeToCompare);
        }
    }
}
