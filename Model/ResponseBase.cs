namespace PrimeiraAPI.Model
{
    public class ResponseBase<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotJAprendiz { get; set; }
        public int TotCLT { get; set; }
        public int TotPJ { get; set; }

        public int TotalItems { get; set; }


    }
}
