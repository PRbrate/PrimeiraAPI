namespace PrimeiraAPI.Data.Filters
{
    public class EmployeeFilter
    {
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }

    }
}
