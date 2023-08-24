using PrimeiraAPI.Model;

namespace PrimeiraAPI.DTOs
{
    public class DepartmentDTO : Entity
    {
        public DepartmentDTO() { }

        public DepartmentDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
