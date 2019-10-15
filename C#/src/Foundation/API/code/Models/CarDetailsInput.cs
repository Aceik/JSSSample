using GraphQL.Types;

namespace Foundation.API.Models
{
    public class CarDetailsInput : InputObjectGraphType<MyCarDetails>
    {
        public CarDetailsInput()
        {
            Field(i => i.Colour);
            Field(i => i.Make);
            Field(i => i.Model);
            Field(i => i.DealerAddress, type: typeof(Address));
        }
    }
}