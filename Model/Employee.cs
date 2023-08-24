namespace PrimeiraAPI.Model
{
    public class Employee : Entity
    {
        public Employee() { }
        public Employee(int id, string cpf, string name, DateTime date, int depatmentId)
        { 
            Id = id;
            Cpf = cpf;
            Name = name;
            DateNasc = date;
            DepatmentId = depatmentId;
        }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime DateNasc { get; set; }
        public Department Departament { get; set; }
        public int DepatmentId { get; set; }
    }
}
