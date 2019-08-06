using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class commentInfo
    {
        public int commentInfoId { get; set; }

        public DateTime createdAt { get; set; }

        public bool delivered { get; set; }

        public DateTime? deliveredAt { get; set; }

        public bool seen { get; set; }

        public DateTime? seenAt { get; set; }

        // 1 to 1 - Steven Sandersons
        public string userAppId { get; set; }
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual userApp user { get; set; }

        // 1 to Many - Steven Sandersons
        public int commentId { get; set; }        
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual comment comment { get; set; }

    }
}
