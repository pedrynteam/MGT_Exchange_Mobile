using Newtonsoft.Json;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using System;

namespace MGT_Exchange_Client.GraphQL.MVC
{
    public class notification
    {        
        public int notificationId { get; set; }

        public string type { get; set; } // NewMessage, NewChat * Enum
        public string title { get; set; } // Game Request * Translate
        public string subtitle { get; set; } // Five Cards Draw * Translate
        public string body { get; set; } // Bob wants to play poker
        public string message { get; set; } // Hello!, New Photo
        
        // 1 to 1 - Steven Sandersons
        public string toUserAppId { get; set; }        
        [JsonIgnore] // To avoid circular calls. Customer -> Order -> Customer -> Order
        [GraphQLFieldIgnore]
        public virtual userApp toUserApp { get; set; }

        // To go to the correct screen or detailed page
        public string route { get; set; }
        public string routeAction { get; set; }
        public string routeId { get; set; }        
        public string routeObject { get; set; } // The json object

        // To continue showing or not the notification. Change Seen when the user sees the event. i.e. Go into Chat
        public DateTime createdAt { get; set; }
        public bool seen { get; set; }
        public DateTime? seenAt { get; set; }

    }







}
