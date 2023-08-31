using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PrimeiraAPI.Migrations;
using PrimeiraAPI.Model.Enums;

namespace PrimeiraAPI.Model
{
    public class Employee : Entity
    {
        public Employee() { }
        public Employee(int id, string cpf, string name, DateTime date, int departmentId, double salary, Office office)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepartmentId = departmentId;
            Salary = salary;
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
        public int Age { get; private set; }
        public Office OfficeId { get; set; }

        public void UpdateOffice()
        {
            if (OfficeId != Office.PJ)
            {
                if (Age > 14 & Age < 24)
                {
                    OfficeId = Office.JovemAprendiz;
                }
                else if (Age < 14)
                {
                    throw new Exception($"Idade não permitida o Funcionário {Name}");
                }
                else
                {
                    OfficeId = Office.CLT;
                }
            }


        }

        public int CountAge()
        {
            var dataAtual = DateTime.Now;
            Age = dataAtual.Year - DateNasc.Year;
            if (dataAtual.Month < DateNasc.Month ||
                (dataAtual.Month <= DateNasc.Month && dataAtual.Day < DateNasc.Day))
            {
                Age--;
            }
            return Age;
        }

    }
}
