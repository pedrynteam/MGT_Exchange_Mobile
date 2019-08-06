using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Resources;
using Newtonsoft.Json;
using SAHB.GraphQLClient.Executor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Mutation
{
    // 1. Create Model: Input type is used for Mutation, it should be included if needed
    public class MutationAddCommentToChatTxn_Input
    {
        public userApp userApp { get; set; }
        public comment comment { get; set; }        
        public string url { get; set; }
        public string token { get; set; }
    }

    public class MutationAddCommentToChatTxn_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public comment comment { get; set; }
    }

    public class MutationAddCommentToChatTxn
    {

        public MutationAddCommentToChatTxn()
        {
        }

        public async Task<MutationAddCommentToChatTxn_Output> Execute(MutationAddCommentToChatTxn_Input input)//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            MutationAddCommentToChatTxn_Output output = new MutationAddCommentToChatTxn_Output();

            string queryRaw = @"
mutation {
  addCommentToChatTxn(
    input: {
      comment: {
        commentId: 0
        chatId: " + input.comment.chatId + @"
        message: " + SAHBResource.SetArgumentRaw(input.comment.message) + @"
        userAppId: " + SAHBResource.SetArgumentRaw(input.comment.userAppId) + @"
      }
    }
  ) {
    resultConfirmation {
      resultCode
      resultPassed
      resultMessage
      resultDetail
      resultDictionary {
        tag
        value
      }
    }
    comment {
     commentId
     message
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
                // If no high level errors return the result as it is from server. It may contain errors
                output = stuff.data.addCommentToChatTxn.ToObject<MutationAddCommentToChatTxn_Output>();
                //List<chat> chats = stuff.data.chatsByUser.ToObject<List<chat>>();
                //output.comment = stuff.data.addCommentToChatTxn.ToObject<comment>();
                //output.ResultConfirmation = new resultConfirmation { resultPassed = true };
            }
            else
            {
                output.comment = null;
                output.ResultConfirmation = new resultConfirmation { resultPassed = false, resultMessage = "ErrorMessage", resultDetail = "resultDetail" };
            }

            return output;

        }
    }
}
