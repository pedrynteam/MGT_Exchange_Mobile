using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGT_Exchange_Client.GraphQL.Resources
{
    // Class to handle confirmations to client, PASS / FAIL, Message
    public class resultConfirmation
    {
        public string resultCode { get; set; }
        public bool resultPassed { get; set; }
        public string resultMessage { get; set; } // Message to translate: ORDER_NOT_FOUND
        public string resultDetail { get; set; } // Detail to transaction: 1   (Order number)

        //[GraphQLFieldIgnore]
        public List<itemKey> resultDictionary { get; set; }

    }
}
