using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    // One to One or One to Many Chat. Just chat.
    public class chat
    {        
        public int chatId { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime? updatedAt { get; set; }

        public bool closed { get; set; }
        public DateTime? closedAt { get; set; }

        // 1 to 1 - Steven Sandersons
        public string companyId { get; set; }
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual company company { get; set; }

        // 1 to Many - Steven Sandersons
        [GraphQLFieldIgnore]
        public virtual List<participant> participants { get; set; }

        // 1 to Many - Steven Sandersons
        [GraphQLFieldIgnore]
        public virtual List<comment> comments { get; set; }

    }

}
