using MGT_Exchange_Client.GraphQL.Interface;
using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Testing
{
    public static class MGTQueryTest
    {        
        public static async Task<QueryAllUsersByCompany_Output> TestQueryAllUsersByCompany()
        {

            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            QueryAllUsersByCompany_Input input = new QueryAllUsersByCompany_Input
            {
                company = new company { companyId = "04c6b67c-6f4d-455f-a472-d6206dd769df" },
                url = _url,
                token = _token
            };

            QueryAllUsersByCompany_Output output = await clientMGT.QueryAllUsersByCompany(input: input);
            return output;
        }
        
        public static async Task<QueryRetrieveMasterInformationByUser_Output> TestQueryRetrieveRecentChats()
        {

            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            // This is to be used as master List, to show recent chats, and recent message of each chat (seen or unseen). 
            // After that the user can click in an specific chat to check more information
            QueryRetrieveMasterInformationByUser_Input input = new QueryRetrieveMasterInformationByUser_Input
            {
                UserApp = new userApp { userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b" },
                chatsRecentTake = 5,
                commentsSeenTake = 0,
                commentsUnseenTake = 0,
                commentsNewestTake = 1,
                commentsBeforeUnseenTake = 0,
                findSpecificChatId = 0, // 0 means no, any number means ChatId
                url = _url,
                token = _token
            };

            QueryRetrieveMasterInformationByUser_Output output = await clientMGT.QueryRetrieveRecentChats(input: input);

            return output;
        }

        public static async Task<QueryRetrieveMasterInformationByUser_Output> TestQueryRetrieveChatComments()
        {

            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            // This is to be used when a user clicks any Chat from the master chats list
            // The app must show unseen comments if any, if none, wold show newest comments
            // Logic in FrontEnd must be
            // IF commentsUnseen is null THEN show only commentsNewest
            // ELSE show commentsUnseen   (Note: In the transaction the beforeUnsee and Unseen are joined)
            // Pending mark as seen all the commentsUnseen Retrieved, create a variable to decidi if mark as seen or not
            QueryRetrieveMasterInformationByUser_Input input = new QueryRetrieveMasterInformationByUser_Input
            {
                UserApp = new userApp { userAppId = "127b0df2-d732-478e-bf84-0b673c48d145" }, //10d3ed1e-ae7d-4d0c-80e0-aa360d76025b" },
                chatsRecentTake = 1,
                commentsSeenTake = 0,
                commentsBeforeUnseenTake = 5,
                commentsUnseenTake = 10,
                commentsNewestTake = 10,
                findSpecificChatId = 2, // 0 means no, any number means ChatId
                url = _url,
                token = _token
            };

            QueryRetrieveMasterInformationByUser_Output output = await clientMGT.QueryRetrieveRecentChats(input: input);
            return output;

        }
    }
}
