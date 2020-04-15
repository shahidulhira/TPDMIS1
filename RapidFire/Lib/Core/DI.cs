using Microsoft.Extensions.DependencyInjection;

namespace RapidFireLib.Lib.Core
{
    public class DI<TService, IImplementation>
    {
        public Injector<TService> Inflate
        {
            get
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient(typeof(TService), typeof(IImplementation));
                var serviceProvider = serviceCollection.BuildServiceProvider();
                var ret = ActivatorUtilities.CreateInstance<Injector<TService>>(serviceProvider);
                return ret;
            }
        }
    }

    public class Injector<T>
    {
        public Injector(T pointer)
        {
            Members = pointer;
        }

        public T Members { get; }
    }
}
