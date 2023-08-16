namespace PrimeiraAPI.Model
{
    public class Employee : Entity
    {
        public int Cpf { get; set; }
        public string name { get; set; }
        public DateTime DateNasc { get; set; }
        public Departament DepartamentFunc { get; set; }
    }
}
