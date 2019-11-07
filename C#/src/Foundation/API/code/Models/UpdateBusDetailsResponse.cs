namespace Foundation.API.Models
{
    public class UpdateBusDetailsResponse : GraphQlResponse
    {
        public int BusVersion { get; set; }

        public UpdateBusDetailsResponse(bool result) : base(result)
        {
            BusVersion = 1200;
        }
    }
}