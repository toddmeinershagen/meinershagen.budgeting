namespace Budgeting.CommandExe.Models
{
    public class Facility
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public virtual Application Application { get; set; }
    }
}