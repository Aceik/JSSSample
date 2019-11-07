using System;
using GraphQL.Types;

namespace Foundation.API.Models
{
    public class CarDetailsInput : InputObjectGraphType<MyCarDetails>
    {
        public CarDetailsInput()
        {
            Field(i => i.Colour, type: typeof(StringGraphType));
            //Field(i => i.Make, type: typeof(StringGraphType));
            //Field(i => i.Model, type: typeof(StringGraphType));
            //Field(i => i.Version, type: typeof(IntGraphType));
            //Field(i => i.DealerAddress, type: typeof(AddressType));
        }
    }
}