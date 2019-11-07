namespace Foundation.API.Models
{
    public class CarDetailsResponse : GraphQlResponse
    {
        public Address HousedAddress { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool HasAlarm { get; set; }

        public CarDetailsResponse(bool result) : base(result)
        {
            HousedAddress = new Address();
        }
    }
}