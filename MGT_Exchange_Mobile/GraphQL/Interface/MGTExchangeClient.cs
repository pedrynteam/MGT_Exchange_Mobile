using MGT_Exchange_Client.GraphQL.Mutation;
using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Interface
{
    /// <summary>
    /// It's preferable to use an interface because all the events are listed here.
    /// </summary>
    public interface IMGTExchangeClient
    {
        // <Query>

        /// <summary>
        /// Get all users for the company, then users are used to create Chats
        /// </summary>
        /// <param name="input"> 
        ///  QueryAllUsersByCompany_Input input = new QueryAllUsersByCompany_Input {                
        ///  company = new company { companyId = "04c6b67c-6f4d-455f-a472-d6206dd769df" },
        ///  url = "GraphQL URL",
        ///  token = "UserAppAuthToken"
        ///  };        
        /// </param>
        /// <returns>A company object with all the users</returns>

        Task<QueryAllUsersByCompany_Output> QueryAllUsersByCompany(QueryAllUsersByCompany_Input input);
        
        /// <summary>
        /// Get a master List, to show recent chats, and recent message of each chat (seen or unseen).
        /// </summary>
        /// <param name="input"> 
        ///  QueryRetrieveMasterInformationByUser_Input input = new QueryRetrieveMasterInformationByUser_Input {   
        ///  UserApp = new userApp { userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b" },
        ///  chatsRecentTake = 5, 
        ///  commentsSeenTake = 0, // Fixed
        ///  commentsUnseenTake = 0, // Fixed
        ///  commentsNewestTake = 1,
        ///  commentsBeforeUnseenTake = 0, // Fixed
        ///  findSpecificChatId = 0, // 0 means no, any number means ChatId
        ///  url = "GraphQL URL",
        ///  token = "UserAppAuthToken"
        ///  };        
        /// </param>
        /// <returns>A object where commentsNewest is the list to use</returns>
        Task<QueryRetrieveMasterInformationByUser_Output> QueryRetrieveRecentChats(QueryRetrieveMasterInformationByUser_Input input);

        /// <summary>
        /// Get a chat detail (findSpecificChatId), This is to be used when a user clicks any Chat from the master chats list
        /// The app must show unseen comments if any, if none, wold show newest comments
        /// Logic in FrontEnd must be
        /// IF commentsUnseen is null THEN show only commentsNewest
        /// ELSE show commentsUnseen   (Note: In the transaction the beforeUnsee and Unseen are joined)
        /// Pending mark as seen all the commentsUnseen Retrieved, create a variable to decidi if mark as seen or not
        /// </summary>
        /// <param name="input"> 
        /// QueryRetrieveMasterInformationByUser_Input input = new QueryRetrieveMasterInformationByUser_Input            {
        /// UserApp = new userApp { userAppId = "127b0df2-d732-478e-bf84-0b673c48d145" }, //10d3ed1e-ae7d-4d0c-80e0-aa360d76025b" },
        /// chatsRecentTake = 1,
        /// commentsSeenTake = 0,
        /// commentsBeforeUnseenTake = 5,
        /// commentsUnseenTake = 10,
        /// commentsNewestTake = 10,                
        /// findSpecificChatId = 2, // 0 means no, any number means ChatId
        ///  url = "GraphQL URL",
        ///  token = "UserAppAuthToken"
        ///  };        
        /// </param>
        /// <returns>A object where commentsUnseen and commentsNewest are the lists to use</returns>
        Task<QueryRetrieveMasterInformationByUser_Output> QueryRetrieveChatComments(QueryRetrieveMasterInformationByUser_Input input);

        // </Query>

        // <Mutation>

        /// <summary>
        /// To create a new company and X users
        /// </summary>
        /// <param name="input"> 
        /// MutationCreateCompanyAndXUsersTxn_Input input = new MutationCreateCompanyAndXUsersTxn_Input {
        /// company = new company
        /// {
        ///  name = "company",
        ///  description= "one",
        ///  email = "one@one.com",
        ///  password = "12345"
        /// },
        /// userstocreate = 10,
        /// url = "GraphQL URL",
        /// token = "UserAppAuthToken"
        /// }; 
        /// </param>
        /// <returns>A company object with a list of new users</returns>

        Task<MutationCreateCompanyAndXUsersTxn_Output> MutationCreateCompanyAndXUsersTxn(MutationCreateCompanyAndXUsersTxn_Input input);

        /// <summary>
        /// To create a Chat with N participants
        /// </summary>
        /// <param name="input"> 
        /// MutationCreateChatTxn_Input input = new MutationCreateChatTxn_Input
        /// {
        /// chat = new chat
        /// {
        /// chatId = 0,
        /// name = "Chat 1",
        /// companyId = "04c6b67c-6f4d-455f-a472-d6206dd769df",
        /// participants = new List ^participant^ {
        /// { new participant { participantId = 0, chatId = 0, isAdmin = true, userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b" },
        /// { new participant { participantId = 0, chatId = 0, isAdmin = true, userAppId = "127b0df2-d732-478e-bf84-0b673c48d145" },
        /// { new participant { participantId = 0, chatId = 0, isAdmin = true, userAppId = "3982349d-289d-4f3a-ab29-f3f6bb3893f4" }
        /// },
        /// url = "GraphQL URL",
        /// token = "UserAppAuthToken"
        /// }; 
        /// </param>
        /// <returns>A chat object</returns>
        Task<MutationCreateChatTxn_Output> MutationCreateChatTxn(MutationCreateChatTxn_Input input);

        /// <summary>
        /// To add comment to a Chat
        /// </summary>
        /// <param name="input"> 
        /// MutationAddCommentToChatTxn_Input input = new MutationAddCommentToChatTxn_Input {
        /// comment = new comment 
        /// {
        /// chatId = 3,
        /// message = "Hello There",
        /// userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b"
        /// },
        /// url = "GraphQL URL",
        /// token = "UserAppAuthToken"
        /// }; 
        /// </param>
        /// <returns>A comment object</returns>

        Task <MutationAddCommentToChatTxn_Output> MutationAddCommentToChatTxn(MutationAddCommentToChatTxn_Input input);

        // </Mutation>

        // <Subscription>
        // </Subscription>
    }

    public class MGTExchangeClient : IMGTExchangeClient
    {
        // <Query> 
        public async Task<QueryAllUsersByCompany_Output> QueryAllUsersByCompany(QueryAllUsersByCompany_Input input)
        {
            QueryAllUsersByCompany_Output output = await new QueryAllUsersByCompany().Execute(input: input);
            return output;
        }

        public async Task<QueryRetrieveMasterInformationByUser_Output> QueryRetrieveRecentChats(QueryRetrieveMasterInformationByUser_Input input)
        {
            QueryRetrieveMasterInformationByUser_Output output = await new QueryRetrieveMasterInformationByUser().Execute(input: input);
            return output;
        }

        public async Task<QueryRetrieveMasterInformationByUser_Output> QueryRetrieveChatComments(QueryRetrieveMasterInformationByUser_Input input)
        {
            QueryRetrieveMasterInformationByUser_Output output = await new QueryRetrieveMasterInformationByUser().Execute(input: input);
            return output;            
        }

        // </Query>
        // <Mutation>
        public async Task<MutationCreateCompanyAndXUsersTxn_Output> MutationCreateCompanyAndXUsersTxn(MutationCreateCompanyAndXUsersTxn_Input input)
        {
            MutationCreateCompanyAndXUsersTxn_Output output = await new MutationCreateCompanyAndXUsersTxn().Execute(input: input);
            return output;
        }
        public async Task<MutationCreateChatTxn_Output> MutationCreateChatTxn(MutationCreateChatTxn_Input input)
        {
            MutationCreateChatTxn_Output output = await new MutationCreateChatTxn().Execute(input: input);
            return output;
        }

        public async Task<MutationAddCommentToChatTxn_Output> MutationAddCommentToChatTxn(MutationAddCommentToChatTxn_Input input)
        {
            MutationAddCommentToChatTxn_Output output = await new MutationAddCommentToChatTxn().Execute(input: input);
            return output;            
        }




        // </Mutation>

    }
}
