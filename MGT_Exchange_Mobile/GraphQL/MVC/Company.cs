using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace MGT_Exchange_Client.GraphQL.MVC
{

    public class company
    {
        
        public string companyId { get; set; }
        public int id { get; set; }        
        public string loginTokenId { get; set; } // Use it to Identify 
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tokenAuth { get; set; }

        [GraphQLFieldIgnore]
        // 1 to Many - Steven Sandersons
        public virtual List<userApp> users { get; set; }
    }
}
