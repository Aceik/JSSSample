using GraphQL.Types;

namespace Foundation.API.Models
{
    public class Address
    {
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
    }

    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Name = "Address";
            Description = "An address object.";
            Field("aa", i => i.AddressLineOne,  nullable:true, type:typeof(StringGraphType)).Description("this is the description");
            //Field("addressLineOne", i => i.AddressLineTwo);
            //Field("suburb", i => i.Suburb);
            //Field("postcode", i => i.Postcode);
            //Field("state", i => i.State);
        }
    }
}