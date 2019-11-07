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
    /// Car Schema provider. This allows the retrieval of car details via a GraphQL Query.
    /// Example Mutation: 
    ///
    public class CarSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new CarInfoQuery();
        }

        protected class CarInfoQuery : RootFieldType<CarDetailsGraphType, CarDetailsResponse>
        {
            private const string CarDetailsQueryName = "carinformation";
            private const string CarQueryDescription = "Retrieves a type of car";

            public CarInfoQuery() : base(name: CarDetailsQueryName, description: CarQueryDescription)
            {
                Arguments = CommonQueryArguments.GetItemLookupArguments();
            }

            protected override CarDetailsResponse Resolve(ResolveFieldContext context)
            {
                CarDetailsResponse response = null;

                if (context.HasArgument(ApiConstants.GraphqlArgumentNames.PATH))
                {
                    var value = context.Arguments[ApiConstants.GraphqlArgumentNames.PATH];
                    if (value != null && value == "Blue")
                    {
                        response = new CarDetailsResponse(true){ HousedAddress = new Address()
                        {
                            AddressLineOne = "Blue HousedAddress",
                            Postcode = "4000",
                            State = "QLD",
                            Suburb = "Brisbane"
                        }};
                    }
                }
                response = new CarDetailsResponse(true)
                {
                    HousedAddress = new Address()
                    {
                        AddressLineOne = "Unknown HousedAddress",
                        Postcode = "2000",
                        State = "NSW",
                        Suburb = "Sydney"
                    }
                };

                response.Make = "Toyota";
                response.Model = "Tarago";
                response.HasAlarm = true;

                return response;
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
            private const string CarDetailsMutationName = "cardetailsmutation";
            private const string CarDetailsMutationDescription = "Car details mutation";

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
        /// Creates the GraphQL fields for car queries
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

        protected class CarDetailsGraphType : ObjectGraphType<CarDetailsResponse>
        {
            public CarDetailsGraphType()
            {
                Name = "CarInformation";
                Field<NonNullGraphType<AddressType>>("address", resolve: context => context.Source.HousedAddress);
                Field<NonNullGraphType<StringGraphType>>("model", resolve: context => context.Source.Model);
                Field<NonNullGraphType<StringGraphType>>("make", resolve: context => context.Source.Make);
                Field<NonNullGraphType<BooleanGraphType>>("hasalarm", resolve: context => context.Source.HasAlarm);
                Field<NonNullGraphType<BooleanGraphType>>("result", resolve: context => context.Source.Result);
            }
        }
    }
}
