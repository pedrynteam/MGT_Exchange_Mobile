using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class comment
    {        
        public int commentId { get; set; }

        public string message { get; set; }

        public DateTime? createdAt { get; set; }

        public bool seenByAll { get; set; } // Save it on Database, just to know if comment was seen by all participants

        // 1 to Many - Steven Sandersons
        public int chatId { get; set; }        
        [GraphQLFieldIgnore]
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        public virtual chat chat { get; set; }

        // 1 to 1 - Steven Sandersons
        public string userAppId { get; set; }        
        [GraphQLFieldIgnore]
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        public virtual userApp user { get; set; }

        // 1 to Many - Steven Sandersons
        [GraphQLFieldIgnore]
        public virtual List<commentInfo> commentsInfo { get; set; }

        
    }
}
