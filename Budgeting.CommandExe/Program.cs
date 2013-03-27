using System;
using System.Configuration;
using Budgeting.CommandExe.Models;
using Budgeting.CommandExe.Specifications;
using Common.Logging;
using Highway.Data;
using Highway.Data.EntityFramework.StructureMap;
using Highway.Data.EventManagement;
using Highway.Data.Interceptors;
using Highway.Data.Interceptors.Events;
using Highway.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;
using StructureMap.ServiceLocatorAdapter;

namespace Budgeting.CommandExe
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Beginning the application.");

            var program = new Program();
            program.SetUpIoc();
            program.Execute();

            Console.WriteLine("Hit ENTER to end.");
            Console.ReadLine();
        }

        public void Execute()
        {
            using (var context = ServiceLocator.Current.GetInstance<IDataContext>())
            {
                //var app1 = new Application {Name = "App1"};
                //var app2 = new Application {Name = "App2"};

                var repository = new Repository(context);

                var app1 = repository.Get(new ApplicationById(1));
                var app2 = repository.Get(new ApplicationById(2));

                //repository.Context.Add(app1);
                //repository.Context.Add(app2);

                //var facility1 = new Facility {Application = app2, Name = "Facility1"};
                //var facility2 = new Facility {Application = app1, Name = "Facility2"};

                //repository.Context.Add(facility1);
                //repository.Context.Add(facility2);

                var facility1 = repository.Get(new FacilityById(1));
                var facility2 = repository.Get(new FacilityById(2));
                facility1.Application = app1;
                facility2.Application = app2;

                repository.Context.Update(facility1);
                repository.Context.Update(facility2);
                
                repository.Context.Commit();
                
                var facility = repository.Get(new FacilityById(1));
            }
        }

        public void SetUpIoc()
        {
            var container = new StructureMap.Container();
            container.Configure(x =>
                {
                    x.AddRegistry<StructureMapRegistry>();
                    x.ForSingletonOf<IMappingConfiguration>()
                        .Use<MyMappings>();
                    x.SelectConstructor<DataContext>(() => new DataContext("", null as IMappingConfiguration));
                    x.For<IDataContext>()
                        .Use<DataContext>()
                        .Ctor<string>("connectionString")
                        .Is(ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString);

                });

            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(container));
        }
    }
}
