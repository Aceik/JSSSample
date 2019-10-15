using System.Collections.Generic;
using Foundation.API.Models;
using Foundation.API.QueryArguments;
using GraphQL.Types;
using Sitecore.Services.GraphQL.Schemas;
using ResolveFieldContext = GraphQL.Types.ResolveFieldContext;

/// <summary>
/// Reference:  https://jss.sitecore.com/docs/techniques/graphql/graphql-overview
/// </summary>
namespace Foundation.API.Schemas
{
    /// <summary>
    /// Contact Info Schema provider. This allows the retrieval of contact information via a GraphQL Query.
    /// Example Mutation: 
    ///
    public class CarSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new ContactInfoQuery();
        }

        protected class ContactInfoQuery : RootFieldType<CarDetailsGraphType, CarDetailsResponse>
        {
            private const string CarDetailsQueryName = "contactinformation";
            private const string CarQueryDescription = "Retrieves a type of car";

            public ContactInfoQuery() : base(name: CarDetailsQueryName, description: CarQueryDescription)
            {
                Arguments = CommonQueryArguments.GetItemLookupArguments();
            }

            protected override CarDetailsResponse Resolve(ResolveFieldContext context)
            {
                if (context.HasArgument(ApiConstants.GraphqlArgumentNames.PATH))
                {
                    var value = context.Arguments[ApiConstants.GraphqlArgumentNames.PATH];
                    if (value != null && value == "Blue")
                    {
                        return new CarDetailsResponse(true){ Address = new Address()
                        {
                            AddressLineOne = "Blue Address",
                            Postcode = "4000",
                            State = "QLD",
                            Suburb = "Brisbane"
                        }};
                    }
                }
                return new CarDetailsResponse(true)
                {
                    Address = new Address()
                    {
                        AddressLineOne = "Unknown Address",
                        Postcode = "2000",
                        State = "NSW",
                        Suburb = "Sydney"
                    }
                };
            }
        }

        public override IEnumerable<FieldType> CreateRootMutations()
        {
            yield return new CarDetailsMutation();
        }

        protected class CarDetailsMutation : RootFieldType<UpdateCarDetailsGraphType, UpdateCarDetailsResponse>
        {
            private const string DetailsParameterName = "details";
            private const string DetailsParameterDescription = "details";
            private const string CarDetailsMutationName = "cardetailsmationmutation";
            private const string CarDetailsMutationDescription = "";

            public CarDetailsMutation() : base(name: CarDetailsMutationName, description: CarDetailsMutationDescription)
            {
                var detailsArgument = new QueryArgument<NonNullGraphType<CarDetailsInput>>
                {
                    Name = DetailsParameterName,
                    Description = DetailsParameterDescription
                };

                Arguments = new GraphQL.Types.QueryArguments(new QueryArgument[] { detailsArgument });
            }

            protected override UpdateCarDetailsResponse Resolve(ResolveFieldContext context)
            {
                UpdateCarDetailsResponse response = new UpdateCarDetailsResponse(true);

                var details = context.GetArgument<MyCarDetails>(DetailsParameterName);

                // Database update goes here.

                response.CarVersion = details.Version + 100;

                return response;
            }
        }

        /// <summary>
        /// Creates the GraphQL fields for member information queries
        /// </summary>
        protected class UpdateCarDetailsGraphType : ObjectGraphType<UpdateCarDetailsResponse>
        {
            public UpdateCarDetailsGraphType()
            {
                Name = "CarDetailsMutation";
                Field<NonNullGraphType<BooleanGraphType>>("result", resolve: context => context.Source.Result);
                Field<NonNullGraphType<StringGraphType>>("version", resolve: context => context.Source.CarVersion);
            }
        }

        /// <summary>
        /// Some mix up of objects happened with the one Graph Type
        /// </summary>
        protected class CarDetailsGraphType : ObjectGraphType<CarDetailsResponse>
        {
            public CarDetailsGraphType()
            {
                Name = "MyAccountContactInformation";
                Field<NonNullGraphType<StringGraphType>>("address", resolve: context => context.Source.Address);
                Field<NonNullGraphType<BooleanGraphType>>("result", resolve: context => context.Source.Result);
            }
        }
    }
}
