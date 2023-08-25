using PrimeiraAPI.Model;

namespace PrimeiraAPI.DTOs
{
    public class EmployeeDTO : Entity
    {
        public EmployeeDTO()
        {

        }
        public EmployeeDTO(int id, string cpf, string name, DateTime date, int depatmentId, string departmentName/*, double salary*/)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepatmentId = depatmentId;
            DepartmentName = departmentName;
            //Salary = salary;
        }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }
        public int DepatmentId { get; set; }
        public string DepartmentName { get; set; }
        //public double Salary { get; set; }
    }
}
