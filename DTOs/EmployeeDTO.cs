using PrimeiraAPI.Model;
using PrimeiraAPI.Model.Enums;

namespace PrimeiraAPI.DTOs
{
    public class EmployeeDTO : Entity
    {
        public EmployeeDTO()
        {

        }
        public EmployeeDTO(int id, string cpf, string name, DateTime date, int depatmentId, string departmentName, double salary, Office office)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepatmentId = depatmentId;
            DepartmentName = departmentName;
            Salary = salary;
            OfficeId = office;
        }

        public EmployeeDTO(int id, string cpf, string name,int age, DateTime date, int depatmentId, string departmentName, double salary, Office office)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            Age = age;
            DateNasc = date;
            DepatmentId = depatmentId;
            DepartmentName = departmentName;
            Salary = salary;
            OfficeId = office;
        }

        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }
        public int MyProperty { get; set; }
        public int DepatmentId { get; set; }
        public string DepartmentName { get; set; }
        public double Salary { get; set; }
        public int Age { get; set; }
        public string NameOffice => OfficeId.ToString();
        public Office OfficeId { get; set; }
    }
}
