namespace Foundation.API.Models
{
    public class GraphQlResponse
    {
        public GraphQlResponse(bool result)
        {
            Result = result;
        }

        public bool Result { get; set; }
    }
}