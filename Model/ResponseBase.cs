using System.Text.Json.Serialization;

namespace PrimeiraAPI.Model
{

    public class ResponseBase<T>
    {

        public IEnumerable<T> Items { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotJAprendiz { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotCLT { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotPJ { get; set; }
        public int TotalItems { get; set; }


    }
}
