using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class groupof
    {
        public int groupofId { get; set; }
     
        public string name { get; set; }

        // 1 to 1 - Steven Sandersons
        public string userAppId { get; set; }
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual userApp user { get; set; }

    }
}
