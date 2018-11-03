using System;
using System.Configuration;
using MealTracking.Contract.Models.Days;
using MealTracking.Contract.Models.Foods;
using MealTracking.Contract.Models.Meals;
using MealTracking.Contract.Repositories;
using MealTracking.Repository;
using MealTracking.Repository.Repositories;
using Unity;
using Unity.Lifetime;

namespace MealTracking.Config
{
    internal static class Bootstrap
    {
        private static DatabaseConfiguration GetDatabaseConfiguration()
        {
            var fileName = ConfigurationManager.AppSettings["LiteDbFileName"];

            return new DatabaseConfiguration
            {
                FileName = fileName
            };
        }
        
        public static void Register(IUnityContainer container)
        {
            var bootstrapper = new Bootstrapper(container);

            bootstrapper
                .RegisterInstance(GetDatabaseConfiguration())

                .RegisterType<Repository<Food>, FoodRepository>()
                .RegisterType<Repository<MealTemplate>, MealRepository>()
                .RegisterType<Repository<EatingDay>, EatingDayRepository>();
        }
        
        private class Bootstrapper
        {
            private readonly IUnityContainer _container;

            public Bootstrapper(IUnityContainer container)
            {
                _container = container;
            }

            public Bootstrapper RegisterType<T>()
            {
                _container.RegisterType<T>(new SingletonLifetimeManager());
            
                return this;
            }

            public Bootstrapper RegisterType(Type type)
            {
                _container.RegisterType(type, new SingletonLifetimeManager());

                return this;
            }
            
            public Bootstrapper RegisterType<TResolveFrom, TResolveTo>()
                where TResolveTo : TResolveFrom
            {
                _container.RegisterType<TResolveFrom, TResolveTo>(new SingletonLifetimeManager());
                
                return this;
            }
            
            public Bootstrapper RegisterInstance<TInstance>(TInstance instance)
            {
                _container.RegisterInstance(instance, new SingletonLifetimeManager());
                
                return this;
            }
        }
    }
}