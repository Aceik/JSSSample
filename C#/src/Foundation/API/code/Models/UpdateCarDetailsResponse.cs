namespace Foundation.API.Models
{
    public class UpdateCarDetailsResponse : GraphQlResponse
    {
        public int CarVersion { get; set; }

        public UpdateCarDetailsResponse(bool result) : base(result)
        {
            CarVersion = 100;
        }
    }
}