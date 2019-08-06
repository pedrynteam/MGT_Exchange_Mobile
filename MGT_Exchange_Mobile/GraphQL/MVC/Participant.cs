using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class participant
    {        
        public int participantId { get; set; }
        
        public bool isAdmin { get; set; }
        
        // 1 to Many - Steven Sandersons
        public int chatId { get; set; }        
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual chat chat { get; set; }

        // 1 to 1 - Steven Sandersons
        public string userAppId { get; set; }        
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual userApp user { get; set; }
        
    }
}
