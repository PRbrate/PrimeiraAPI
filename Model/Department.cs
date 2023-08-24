namespace PrimeiraAPI.Model
{
    public class Department : Entity
    {

        public Department()
        {
            Employees = new List<Employee>();
        }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
