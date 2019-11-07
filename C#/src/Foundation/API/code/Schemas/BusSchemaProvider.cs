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
    /// Bus Schema provider. This allows the retrieval of bus information via a GraphQL Query.
    /// Example Mutation: 
    ///
    public class BusSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new BusQuery();
        }

        protected class BusQuery : RootFieldType<BusDetailsGraphType, BusDetailsResponse>
        {
            private const string BusQueryName = "businformation";
            private const string BusQueryDescription = "Retrieves a type of bus";

            public BusQuery() : base(name: BusQueryName, description: BusQueryDescription)
            {
                Arguments = CommonQueryArguments.GetItemLookupArguments();
            }

            protected override BusDetailsResponse Resolve(ResolveFieldContext context)
            {
                if (context.HasArgument(ApiConstants.GraphqlArgumentNames.PATH))
                {
                    var value = context.Arguments[ApiConstants.GraphqlArgumentNames.PATH];
                    if (value != null && value == "Scrap")
                    {
                        return new BusDetailsResponse(true)
                        {
                            Result = false
                        };
                    }
                }
                return new BusDetailsResponse(true)
                {
                    Model = "Suzuki",
                    Passengers = 50,
                    Result = true
                };
            }
        }

        public override IEnumerable<FieldType> CreateRootMutations()
        {
            yield return new BusDetailsMutation();
        }

        protected class BusDetailsMutation : RootFieldType<UpdateBusDetailsGraphType, UpdateBusDetailsResponse>
        {
            private const string DetailsParameterName = "details";
            private const string DetailsParameterDescription = "details";
            private const string BusDetailsMutationName = "busdetailsmationmutation";
            private const string BusDetailsMutationDescription = "";

            public BusDetailsMutation() : base(name: BusDetailsMutationName, description: BusDetailsMutationDescription)
            {
                var detailsArgument = new QueryArgument<NonNullGraphType<BusDetailsInput>>
                {
                    Name = DetailsParameterName,
                    Description = DetailsParameterDescription
                };

                Arguments = new GraphQL.Types.QueryArguments(new QueryArgument[] { detailsArgument });
            }

            protected override UpdateBusDetailsResponse Resolve(ResolveFieldContext context)
            {
                UpdateBusDetailsResponse response = new UpdateBusDetailsResponse(true);

                var details = context.GetArgument<MyBusDetails>(DetailsParameterName);

                // Database update goes here.

                response.BusVersion = details.Version + 100;

                return response;
            }
        }

        /// <summary>
        /// Creates the GraphQL fields for bus update mutation
        /// </summary>
        protected class UpdateBusDetailsGraphType : ObjectGraphType<UpdateBusDetailsResponse>
        {
            public UpdateBusDetailsGraphType()
            {
                Name = "BusDetailsMutation";
                Field<NonNullGraphType<BooleanGraphType>>("result", resolve: context => context.Source.Result);
                Field<NonNullGraphType<StringGraphType>>("version", resolve: context => context.Source.BusVersion);
            }
        }

        /// <summary>
        /// Some mix up of objects happened with the one Graph Type
        /// </summary>
        protected class BusDetailsGraphType : ObjectGraphType<BusDetailsResponse>
        {
            public BusDetailsGraphType()
            {
                Name = "BusInformation";
                Field<NonNullGraphType<StringGraphType>>("passengers", resolve: context => context.Source.Passengers);
                Field<NonNullGraphType<StringGraphType>>("model", resolve: context => context.Source.Model);
                Field<NonNullGraphType<BooleanGraphType>>("result", resolve: context => context.Source.Result);
            }
        }
    }
}
