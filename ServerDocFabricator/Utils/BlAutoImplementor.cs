using ServerDocFabricator.BL.Utils.Attributes;
using ServerDocFabricator.Utils.Attributes;
using System.Reflection;

namespace ServerDocFabricator.Utils
{
    public static class BlAutoImplementor
    {
        /// <summary>
        /// Random class from Assembly whit BL
        /// </summary>
      
        public static void Implement<T>(IServiceCollection serviceProvider) {
            
            
            var assemblyFullName = typeof(T).Assembly.FullName;
            Assembly.Load(assemblyFullName);

            var requiredAssembly = AppDomain.CurrentDomain.GetAssemblies().First(t => t.FullName == assemblyFullName);
            var assemblyTypes = requiredAssembly.GetTypes().Where(t => t.GetCustomAttribute<BuisnessAttribute>() != null);

            foreach (var blType in assemblyTypes)
            {
                foreach (var @interface in blType.GetInterfaces() )
                {
                    serviceProvider.AddScoped(@interface, blType);
                }
            }
        }
    }
}
