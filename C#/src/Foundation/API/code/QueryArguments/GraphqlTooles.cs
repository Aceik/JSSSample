using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraphQL.Types;

namespace Foundation.API.QueryArguments
{
    public class CommonQueryArguments
    {
        public static GraphQL.Types.QueryArguments GetItemLookupArguments()
        {
            GraphQL.Types.QueryArguments arguments = new GraphQL.Types.QueryArguments(new QueryArgument[]
            {
                new QueryArgument<StringGraphType>
                {
                    Name = ApiConstants.GraphqlArgumentNames.ITEMID,
                    Description = "The sitecore item ID"
                }, new QueryArgument<StringGraphType>
                {
                    Name = ApiConstants.GraphqlArgumentNames.PATH,
                    Description = "The Sitecore route, that matches the item name"
                }
            });
            return arguments;
        }

        public static GraphQL.Types.QueryArguments SwitchCommandArgument()
        {
            GraphQL.Types.QueryArguments arguments = new GraphQL.Types.QueryArguments(new QueryArgument[]
            {
                new QueryArgument<StringGraphType>
                {
                    Name = ApiConstants.GraphqlArgumentNames.GET,
                    Description = "The get command is sent when we want this query to lookup a value relating to the action."
                }, new QueryArgument<StringGraphType>
                {
                    Name = ApiConstants.GraphqlArgumentNames.PUT,
                    Description = "The put command is sent to make the query perform an action."
                }
            });
            return arguments;
        }

        public static GraphQL.Types.QueryArguments GetIdLookupArguments()
        {
            GraphQL.Types.QueryArguments arguments = new GraphQL.Types.QueryArguments(new QueryArgument[]
            {
                new QueryArgument<StringGraphType>
                {
                    Name = ApiConstants.GraphqlArgumentNames.ITEMID,
                    Description = "The item ID"
                }
            });
            return arguments;
        }
    }

    public struct ApiConstants
    {
        public struct GraphqlArgumentNames
        {
            public const string ITEMID = "iD";
            public const string PATH = "path";

            public const string GET = "get";
            public const string PUT = "put";
        }
    }
}