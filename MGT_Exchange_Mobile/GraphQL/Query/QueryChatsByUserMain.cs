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
    public class QueryChatsByUserMain_Input
    {
        public userApp UserApp { get; set; }
        public int takeChats { get; set; }
        public int unseenForUserIdTake { get; set; }
        public int newestWhenNoUnseenTake { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class QueryChatsByUserMain_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public List<chat> Chats { get; set; }
    }

    public class QueryChatsByUserMain
    {

        public QueryChatsByUserMain()
        {
        }

        public async Task<QueryChatsByUserMain_Output> Execute(QueryChatsByUserMain_Input input )//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            QueryChatsByUserMain_Output output = new QueryChatsByUserMain_Output();


            string queryRaw = @"
query {
  chatsByUser(userAppId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @", take: " + input.takeChats + @") {
    chatId
    name
    comments(
      unseenForUserId: " + SAHBResource.SetArgumentRaw(input.UserApp.userAppId) + @"
      unseenForUserIdTake: "+ input.unseenForUserIdTake + @"
      newestWhenNoUnseenTake: " + input.newestWhenNoUnseenTake + @"
    ) {
      commentId
      message
      user {
        userAppId
        nickname
      }
    }
    participants {
      participantId
      userAppId
      user {
        userAppId
        nickname
      }
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
                //List<chat> chats = stuff.data.chatsByUser.ToObject<List<chat>>();
                output.Chats = stuff.data.chatsByUser.ToObject<List<chat>>();
                output.ResultConfirmation = new resultConfirmation { resultPassed = true };
            }
            else
            {
                output.Chats = null;
                output.ResultConfirmation = new resultConfirmation { resultPassed = false, resultMessage = "ErrorMessage", resultDetail = "resultDetail" };
            }

            return output;

        }
    }
}
