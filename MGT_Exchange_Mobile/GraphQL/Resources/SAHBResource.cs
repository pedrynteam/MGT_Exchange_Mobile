using System;
using System.Collections.Generic;
using System.Text;

namespace MGT_Exchange_Client.GraphQL.Resources
{
    static class SAHBResource
    {
        public static string SetArgumentRaw(string argumentValue)
        {
            return "\\\"" + argumentValue + "\\\"";
        }
    }
}
