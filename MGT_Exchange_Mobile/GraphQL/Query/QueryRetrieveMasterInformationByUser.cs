using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Resources;
using Newtonsoft.Json;
using SAHB.GraphQLClient.Executor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Query
{

    // 1. Create Model: Input type is used for Mutation, it should be included if needed
    public class QueryRetrieveMasterInformationByUser_Input
    {
        public userApp UserApp { get; set; }
        public int chatsRecentTake { get; set; }
        public int commentsSeenTake { get; set; }
        public int commentsUnseenTake { get; set; }
        public int commentsBeforeUnseenTake { get; set; }
        public int commentsNewestTake { get; set; }
        public int findSpecificChatId { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class QueryRetrieveMasterInformationByUser_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public List<comment> commentsUnseen { get; set; }
        public List<comment> commentsSeen { get; set; }
        public List<comment> commentsNewest { get; set; }
    }

    public class QueryRetrieveMasterInformationByUser
    {

        public QueryRetrieveMasterInformationByUser()
        {
        }

        public async Task<QueryRetrieveMasterInformationByUser_Output> Execute(QueryRetrieveMasterInformationByUser_Input input)//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            QueryRetrieveMasterInformationByUser_Output output = new QueryRetrieveMasterInformationByUser_Output();
            
            string queryRaw = @"
mutation {
  retrieveMasterInformationByUser(
    input: {
      user: { userAppId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @" }
      chatsRecentTake: "+ input.chatsRecentTake + @"
      commentsSeenTake: "+ input.commentsSeenTake + @"
      commentsUnseenTake: "+ input.commentsUnseenTake + @"
      commentsNewestTake: "+ input.commentsNewestTake + @"
      commentsBeforeUnseenTake: "+ input.commentsBeforeUnseenTake + @"
      findSpecificChatId: "+ input.findSpecificChatId + @"
    }
  ) {
    commentsUnseen {
      chatId
      commentId
      userAppId
      message
      commentsInfo(infoForUserId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @") 
        {
            userAppId
            seen
        delivered
      }
    }
    commentsSeen {
      chatId
      commentId
      userAppId
      message
      commentsInfo(infoForUserId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @") 
    {
        userAppId
        seen
        delivered
      }
}
commentsNewest {
      chatId
      commentId
      userAppId
      message
      commentsInfo(infoForUserId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @") 
{
    userAppId
    seen
        delivered
      }
      
    }
    resultConfirmation
    {
      resultPassed
    }
  }
}

";

            string queryToExecute = " {\"query\":\" " + queryRaw + " \"} ";

            IGraphQLHttpExecutor executor = new GraphQLHttpExecutor();
            var result = await executor.ExecuteQuery(query: queryToExecute, url: input.url, method: HttpMethod.Post, authorizationMethod: "Bearer", authorizationToken: input.token);

            dynamic stuff = JsonConvert.DeserializeObject(result.Response);

            // Find a way to see errors
            bool errors = false;
            //foreach (var error in stuff.errors)
            {
                //System.Diagnostics.Debug.WriteLine("Error: " + error["message"]);
            }

            if (!errors)
            {
                /*
                output.commentsUnseen = stuff.data.retrieveMasterInformationByUser.commentsUnseen.ToObject<comment>();
                output.commentsSeen = stuff.data.retrieveMasterInformationByUser.commentsSeen.ToObject<comment>();
                output.commentsNewest = stuff.data.retrieveMasterInformationByUser.commentsNewest.ToObject<comment>();
                output.ResultConfirmation = new resultConfirmation { resultPassed = true, resultMessage = "OK", resultDetail = "" };
                */
                // output = stuff.data.createCompanyAndXUsersTxn.ToObject<MutationCreateCompanyAndXUsersTxn_Output>();
                output = stuff.data.retrieveMasterInformationByUser.ToObject<QueryRetrieveMasterInformationByUser_Output>();
            }
            else
            {
                //output.company = null;
                output.ResultConfirmation = new resultConfirmation { resultPassed = false, resultMessage = "ErrorMessage", resultDetail = "resultDetail" };
            }

            return output;

        }
    }
}
