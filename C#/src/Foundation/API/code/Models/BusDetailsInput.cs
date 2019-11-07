using GraphQL.Types;

namespace Foundation.API.Models
{
    public class BusDetailsInput : InputObjectGraphType<MyBusDetails>
    {
        public BusDetailsInput()
        {
            Field(i => i.Make);
        }
    }
}