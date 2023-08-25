using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrimeiraAPI.Model
{
    public class Employee : Entity
    {
        public Employee() { }
        public Employee(int id, string cpf, string name, DateTime date, int departmentId, double salary)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepartmentId = departmentId;
            Salary = salary;

        }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [ForeignKey("DepartmentId")]
        public Department Departament { get; set; }
        public int DepartmentId { get; set; }
        public double Salary { get; set; }
    }
}
