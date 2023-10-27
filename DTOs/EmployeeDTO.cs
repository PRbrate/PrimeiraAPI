using PrimeiraAPI.Model;
using PrimeiraAPI.Model.Enums;

namespace PrimeiraAPI.DTOs
{
    public class EmployeeDTO : Entity
    {
        public EmployeeDTO()
        {

        }
       
        public EmployeeDTO(int id, string cpf, string name,int age, DateTime date, int depatmentId, string departmentName, decimal salary, Office office)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            Age = age;
            DateNasc = date;
            DepartmentId = depatmentId;
            DepartmentName = departmentName;
            Salary = salary;
            OfficeId = office;
        }

        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }
        public int MyProperty { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string NameOffice => OfficeId.ToString();
        public Office OfficeId { get; set; }
    }
}
