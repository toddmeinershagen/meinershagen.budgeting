using System.Data.Entity;
using Budgeting.CommandExe.Models;
using Highway.Data;

namespace Budgeting.CommandExe
{
    public class MyMappings : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>();
            modelBuilder.Entity<Application>();
        }
    }
}