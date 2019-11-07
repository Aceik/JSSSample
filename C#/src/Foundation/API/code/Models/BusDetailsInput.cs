using GraphQL.Types;

namespace Foundation.API.Models
{
    public class BusDetailsInput : InputObjectGraphType<MyCarDetails>
    {
        public BusDetailsInput()
        {
            Field(i => i.Make);
        }
    }
}