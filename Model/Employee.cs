using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrimeiraAPI.Model
{
    public class Employee : Entity
    {
        public Employee() { }
        public Employee(int id, string cpf, string name, DateTime date, int departmentId, double salary, int age, Office office)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepartmentId = departmentId;
            Salary = salary;
            Age = age;
            OfficeId = office;
        }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [ForeignKey("DepartmentId")]
        public Department Departament { get; set; }
        public int DepartmentId { get; set; }
        public double Salary { get; set; }
        public int Age { get; set; }
        public Office OfficeId { get; set; }
        
    }
    public enum Office : ushort
    {
        JovemAprendiz,
        CLT,
        PJ
    }
}
