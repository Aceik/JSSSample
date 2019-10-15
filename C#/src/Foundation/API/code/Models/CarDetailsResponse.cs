namespace Foundation.API.Models
{
    public class CarDetailsResponse : GraphQlResponse
    {
        public Address Address { get; set; }

        public CarDetailsResponse(bool result) : base(result)
        {
            Address = new Address();
        }
    }
}