namespace Foundation.API.Models
{
    public class MyCarDetails
    {
        public string Colour { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Version { get; set; }
        public Address DealerAddress { get; set; }
    }
}