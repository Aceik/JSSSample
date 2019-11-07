namespace Foundation.API.Models
{
    public class BusDetailsResponse : GraphQlResponse
    {
        public int Passengers { get; set; }
        public string Model { get; set; }

        public BusDetailsResponse(bool result) : base(result)
        {
        }
    }
}