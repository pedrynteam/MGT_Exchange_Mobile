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
    public class MutationCreateChatTxn_Input
    {        
        public chat chat { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class MutationCreateChatTxn_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public chat chat { get; set; }
    }

    public class MutationCreateChatTxn
    {

        public MutationCreateChatTxn()
        {
        }

        public async Task<MutationCreateChatTxn_Output> Execute(MutationCreateChatTxn_Input input)//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            MutationCreateChatTxn_Output output = new MutationCreateChatTxn_Output();

            String participantsIn = "";
            foreach (var userin in input.chat.participants)
            {
                string admin = (userin.isAdmin == true) ? "true" : "false";

                participantsIn += @"
                {
                    participantId: 0
                    chatId: 0
                    isAdmin: " + admin + @"
                    userAppId: " + SAHBResource.SetArgumentRaw(userin.userAppId) + @"
                }
                ";
            }

            string queryRaw = @"

mutation
{
  createChatTxn(input:
  {
    chat:
    {
      chatId:0
      name:" + SAHBResource.SetArgumentRaw(input.chat.name) + @"
      companyId: " + SAHBResource.SetArgumentRaw(input.chat.companyId) + @"
      participants:
      [
      "+ participantsIn + @"    
      ]
    }
    }
  )
  {
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
    chat
    {
      chatId
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
                output = stuff.data.createChatTxn.ToObject<MutationCreateChatTxn_Output>();
            }
            else
            {
                output.chat = null;
                output.ResultConfirmation = new resultConfirmation { resultPassed = false, resultMessage = "ErrorMessage", resultDetail = "resultDetail" };
            }

            return output;

        }
    }
}
