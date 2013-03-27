using System.Linq;
using Budgeting.CommandExe.Models;
using Highway.Data.QueryObjects;

namespace Budgeting.CommandExe.Specifications
{
    public class FacilityById : Scalar<Facility>
    {
        public FacilityById(int id)
        {
            ContextQuery = m => m.AsQueryable<Facility>().FirstOrDefault(f => f.Id == id);
        }
    }
}