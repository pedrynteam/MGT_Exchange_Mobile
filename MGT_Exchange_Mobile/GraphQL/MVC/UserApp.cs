using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class userApp
    {
        public string userAppId { get; set; }
        public int id { get; set; }
        public string loginTokenId { get; set; } // Use it to Identify 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string nickname { get; set; }
        public string alias { get; set; } // htr003 - For name duplications
        public string email { get; set; }
        public string password { get; set; }
        public string tokenAuth { get; set; }
        public DateTime lastSeen { get; set; }
        public bool active { get; set; } // To know that the user is Active or not. So dont send him notifications, messages, etc, etc.
        // 1 to 1 - Steven Sandersons
        public string companyId { get; set; }
        [GraphQLFieldIgnore]
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        public virtual company company { get; set; }

        // 1 to 1 - Steven Sandersons
        public int? departmentId { get; set; }
        [GraphQLFieldIgnore]
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        public virtual department department { get; set; }

        // 1 to Many - Steven Sandersons
        [GraphQLFieldIgnore]
        public virtual List<groupof> groups { get; set; }

    }
}